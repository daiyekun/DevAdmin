using Dev.WooNet.AutoMapper.Extend;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Tsp;
using SqlSugar;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WooDev.Common.Models;
using WooDev.Common.Utility;
using WooDev.Model.Models;
using WooDev.ViewModel;
using WooDev.ViewModel.Contract.Enums;
using WooDev.ViewModel.Enums;
using WooDev.ViewModel.Flow;
using WooDev.ViewModel.Flow.FlowInstance;

namespace WooDev.Services
{
    /// <summary>
    /// 审批实例
    /// </summary>
    public partial class DevFlowInstanceService
    {
        /// <summary>
        /// 根据条件查询模板
        /// </summary>
        /// <param name="appConditions">流程模板条件</param>
        /// <returns></returns>
        public DEV_FLOW_TEMP GetTemp(AppConditionsInfo appConditions)
        {
            DEV_FLOW_TEMP flowTemp = null;
            var tempquery = DbClient.Queryable<DEV_FLOW_TEMP>()
                .Where(a => a.OBJ_TYPE == appConditions.FlowObj);
            switch (appConditions.FlowObj)
            {
                case (int)FlowObjEnums.Contract:
                case (int)FlowObjEnums.InvoiceIn:
                case (int)FlowObjEnums.InvoiceOut:
                case (int)FlowObjEnums.payment:
                    tempquery= tempquery.Where(a => a.MIN_MONERY <= appConditions.Monery && appConditions.Monery <= a.MAX_MONERY);
                    break;


            }
            var listtmp = tempquery.ToList();
            foreach (var item in listtmp)
            {
                var arrflowitem = StringHelper.String2ArrayInt(item.FLOW_ITEMS);
                var arrcateIds = StringHelper.String2ArrayInt(item.CATE_IDS);
                var deptIdsIds = StringHelper.String2ArrayInt(item.DEPART_IDS);
                if(arrflowitem.Contains(appConditions.FlowItem)
                    && arrcateIds.Contains(appConditions.CateId)
                    && deptIdsIds.Contains(appConditions.DeptId))
                {
                    flowTemp = item;
                    break;
                }

            }

            return flowTemp;




        }

        #region 创建审批实例


        /// <summary>
        /// 创建审批实例
        /// 目前审批都是考虑单线条的，多线条太复杂没时间去完成。后续有时间再考虑
        /// 如果遇到多选条，采用多建立几条流程去完成。比如一个分支建立一条流程
        /// 
        /// </summary>
        /// <returns></returns>
        public DEV_FLOW_INSTANCE CreateFlowInst(FlowInstDTO flowInstDTO, int userId)
        {
            var tempInfo = DbClient.Queryable<DEV_FLOW_TEMP>().Where(a => a.ID == flowInstDTO.TempId).First();
            var listNodes = DbClient.Queryable<DEV_FLOWTEMP_NODE>().Where(a => a.TEMP_ID == flowInstDTO.TempId).ToList();
            var listNodeInfos = DbClient.Queryable<DEV_FLOWTEMP_NODE_INFO>().Where(a => a.TEMP_ID == flowInstDTO.TempId).ToList();
            var listEdges = DbClient.Queryable<DEV_FLOWTEMP_EDGE>().Where(a => a.TEMP_ID == flowInstDTO.TempId).ToList();
            var dicdata = GetNodes(listNodes, listEdges);

            //审批实例对象创建
            var InstInfo = new DEV_FLOW_INSTANCE();
            InstInfo.TEMP_ID = flowInstDTO.TempId;
            InstInfo.FLOW_ITEM_ID = flowInstDTO.FlowItem;
            InstInfo.FLOW_STATE = 1;//审批中
            InstInfo.ORDER_NUM = 0;
            InstInfo.IS_DELETE = 0;
            InstInfo.CREATE_USERID = userId;
            InstInfo.CREATE_TIME = DateTime.Now;
            InstInfo.UPDATE_USERID = userId;
            InstInfo.UPDATE_TIME = DateTime.Now;
            InstInfo.CURR_NODE_ID = dicdata["firstNode"];//当前节点ID
            InstInfo.CURR_NODE_NAME = dicdata["firstNodeName"];//当前节点名称
            InstInfo.FLOW_TYPE = flowInstDTO.FlowType;//审批类型
            #region 审批对象信息获取以及修改  其他审批添加基本修改这里
            var objInfo = UpdateAppObject(flowInstDTO, userId, InstInfo, dicdata);
            #endregion
            //审批实例对象新增到数据库
            var inceInfo = DbClient.Insertable(InstInfo).ExecuteReturnEntity();

            #region 开始组装审批节点，链接线，审批信息等


            List<DEV_FLOW_INST_NODE> tlistNodes = CreateInstNodes(userId, listNodes, dicdata, inceInfo);
            List<DEV_FLOW_INST_NODE_INFO> tlistNodeInfos = CreateInstNodeInfos(userId, listNodeInfos, inceInfo, dicdata);
            List<DEV_FLOW_INST_EDGE> tlistEdges = CreateEdges(userId, listEdges, dicdata, inceInfo);
            //审批实例组
            List<DEV_FLOW_INST_GROUPUSER> tlistInstGroup = GetFlowInstGroupUser(listNodeInfos, dicdata, InstInfo);
            //当前审批人员，方便后续查询列表而建立-实战中如果不设计这个，审批数据到一定量联合查询很老火
            List<DEV_FLOW_INST_WAIT_USER> tlistWaitUser = CreateWaitUser(listNodeInfos, dicdata, InstInfo, userId);

            DbClient.Insertable(tlistNodes).AddQueue();
            DbClient.Insertable(tlistNodeInfos).AddQueue();
            DbClient.Insertable(tlistEdges).AddQueue();
            DbClient.Insertable(tlistWaitUser).AddQueue();
            DbClient.Insertable(tlistInstGroup).AddQueue();


            var ar = DbClient.SaveQueues();

            #endregion


            return inceInfo;


        }
        /// <summary>
        /// 创建当前实例连接线
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="listEdges">模板连接线</param>
        /// <param name="dicdata">当前需要更改状态</param>
        /// <param name="inceInfo">审批实例</param>
        /// <returns>审批实例连接线</returns>

        private List<DEV_FLOW_INST_EDGE> CreateEdges(int userId, List<DEV_FLOWTEMP_EDGE> listEdges, Dictionary<string, string> dicdata, DEV_FLOW_INSTANCE inceInfo)
        {
            List<DEV_FLOW_INST_EDGE> tlistEdges = new List<DEV_FLOW_INST_EDGE>();
            //遍历节点联线
            foreach (var item in listEdges)
            {
                var edgeInfo = new DEV_FLOW_INST_EDGE();
                edgeInfo.INST_ID = inceInfo.ID;
                if (dicdata["curredge"] == item.EDGE_STRID)
                {
                    edgeInfo.EDGE_STATE = 2;

                }
                else
                {
                    edgeInfo.EDGE_STATE = 0;
                }
                edgeInfo.EDGE_STRID = item.EDGE_STRID;
                edgeInfo.EDGE_TYPE = item.EDGE_TYPE;
                edgeInfo.SOURCENODEID = item.SOURCENODEID;
                edgeInfo.TARGETNODEID = item.TARGETNODEID;
                edgeInfo.STARTPORT_X = item.STARTPORT_X;
                edgeInfo.STARTPORT_Y = item.STARTPORT_Y;
                edgeInfo.ENDPORT_X = item.ENDPORT_X;
                edgeInfo.ENDPORT_Y = item.ENDPORT_Y;
                edgeInfo.TEXT_X = item.TEXT_X;
                edgeInfo.TEXT_Y = item.TEXT_Y;
                edgeInfo.TEXT_VALUE = item.TEXT_VALUE;
                edgeInfo.PONTSLIST = item.PONTSLIST;
                edgeInfo.IS_DELETE = 0;
                edgeInfo.CREATE_USERID = userId;
                edgeInfo.CREATE_TIME = DateTime.Now;
                edgeInfo.UPDATE_TIME = DateTime.Now;
                edgeInfo.UPDATE_USERID = userId;
                tlistEdges.Add(edgeInfo);

            }

            return tlistEdges;
        }

        /// <summary>
        /// 创建节点信息
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="listNodeInfos">模板节点信息</param>
        /// <param name="inceInfo">审批实例对象</param>
        /// <returns>审批实例节点信息</returns>
        private List<DEV_FLOW_INST_NODE_INFO> CreateInstNodeInfos(int userId, List<DEV_FLOWTEMP_NODE_INFO> listNodeInfos, DEV_FLOW_INSTANCE inceInfo, Dictionary<string, string> dicdata)
        {
            List<DEV_FLOW_INST_NODE_INFO> tlistNodeInfos = new List<DEV_FLOW_INST_NODE_INFO>();
            //遍历节点信息
            foreach (var item in listNodeInfos)
            {

                var instNode = new DEV_FLOW_INST_NODE_INFO();
                instNode.INST_ID = inceInfo.ID;
                instNode.NODE_ID = 0;
                instNode.NODE_STRID = item.NODE_STRID;
                instNode.O_TYPE = item.O_TYPE;
                instNode.OPT_ID = item.OPT_ID;
                instNode.OPT_NAME = "";
                // instNode.INFO_STATE = 0;
                if (dicdata["stratNode"] == item.NODE_STRID)
                {//开始节点
                    instNode.INFO_STATE = (int)WorkFlowStateEnums.SPTG;
                }
                if (dicdata["firstNode"] == item.NODE_STRID)
                {//审批中
                    instNode.INFO_STATE = (int)WorkFlowStateEnums.SPZ;
                }
                else
                {
                    instNode.INFO_STATE = (int)WorkFlowStateEnums.State0;
                }
                instNode.RE_TEXT = item.RE_TEXT;
                instNode.NRULE = item.NRULE;
                instNode.IS_DELETE = 0;
                instNode.CREATE_TIME = DateTime.Now;
                instNode.CREATE_USERID = userId;
                instNode.UPDATE_TIME = DateTime.Now;
                instNode.UPDATE_USERID = userId;
                tlistNodeInfos.Add(instNode);


            }

            return tlistNodeInfos;
        }

        /// <summary>
        /// 创建节点信息
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="listNodes">模板节点集合</param>
        /// <param name="dicdata">当前审批状态需要改变数据</param>
        /// <param name="inceInfo">审批实例</param>
        /// <returns>审批实例节点</returns>

        private List<DEV_FLOW_INST_NODE> CreateInstNodes(int userId, List<DEV_FLOWTEMP_NODE> listNodes, Dictionary<string, string> dicdata, DEV_FLOW_INSTANCE inceInfo)
        {
            List<DEV_FLOW_INST_NODE> tlistNodes = new List<DEV_FLOW_INST_NODE>();
            //遍历模板节点
            foreach (var item in listNodes)
            {
                DEV_FLOW_INST_NODE node = new DEV_FLOW_INST_NODE();
                node.INST_ID = inceInfo.ID;
                node.N_TYPE = item.N_TYPE;
                node.NODE_STRID = item.NODE_STRID;
                node.X = item.X;
                node.Y = item.Y;
                node.TEXT_X = item.X;
                node.TEXT_Y = item.Y;
                node.TEXT_VALUE = item.TEXT_VALUE;
                //if (dicdata["stratNode"] == item.NODE_STRID)
                if (item.N_TYPE== "StartNode")
                {//开始节点
                    node.NODE_STATE = (int)WorkFlowStateEnums.SPTG;
                }
                else
                {
                    if (dicdata["firstNode"] == item.NODE_STRID)
                    {//审批中
                        node.NODE_STATE = (int)WorkFlowStateEnums.SPZ;
                    }
                    else
                    {
                        node.NODE_STATE = (int)WorkFlowStateEnums.State0;
                    }

                }
               

                node.ORDER_NUM = 0;
                node.IS_DELETE = 0;
                node.CREATE_USERID = userId;
                node.UPDATE_USERID = userId;
                node.CREATE_TIME = DateTime.Now;
                node.UPDATE_TIME = DateTime.Now;
                node.NRULE = item.NRULE;
                node.RE_TEXT = item.RE_TEXT;
                tlistNodes.Add(node);
            }

            return tlistNodes;
        }

        /// <summary>
        /// 修改审批对象以及获取部分审批实例信息
        /// 如果有其他对象审批请修改这里
        /// </summary>
        /// <param name="flowInstDTO">审批实例提交对象</param>
        /// <param name="userId">当前用户</param>
        /// <param name="InstInfo">审批实例</param>
        /// <param name="dicdata">当前审批节点相关信息</param>
        private IDevEntitiy UpdateAppObject(FlowInstDTO flowInstDTO, int userId, DEV_FLOW_INSTANCE InstInfo, Dictionary<string, string> dicdata)
        {
            switch (flowInstDTO.FlowType)
            {
                case (int)FlowObjEnums.Customer:
                case (int)FlowObjEnums.Supplier:
                case (int)FlowObjEnums.Other:
                    {
                        var info = DbClient.Queryable<DEV_COMPANY>().Where(a => a.ID == flowInstDTO.ObjId).Single();
                        InstInfo.NAME = info.NAME;
                        InstInfo.CODE = info.CODE;
                        InstInfo.APP_ID = info.ID;
                        InstInfo.APP_MONERY = 0;
                        InstInfo.START_USER_ID = userId;
                        //修改当前对象
                        info.WF_STATE = 1;//审批中
                        info.WF_ITEM = flowInstDTO.FlowItem;
                        info.WF_NODE_STRID = dicdata["firstNode"];//节点ID
                        info.WF_NODE = dicdata["firstNodeName"];//节点名称
                        //修改当前审批对象 最后和审批节点信息一起提交
                        DbClient.Updateable<DEV_COMPANY>(info).AddQueue();
                        return info;

                    }

                case (int)FlowObjEnums.Contract:
                    {
                        var info = DbClient.Queryable<DEV_CONTRACT>().Where(a => a.ID == flowInstDTO.ObjId).Single();
                        InstInfo.NAME = info.C_NAME;
                        InstInfo.CODE = info.C_CODE;
                        InstInfo.APP_ID = info.ID;
                        InstInfo.APP_MONERY = 0;
                        InstInfo.START_USER_ID = userId;
                        InstInfo.APP_MONERY = info.ANT_MONERY;
                        //修改当前对象
                        info.WF_STATE = 1;//审批中
                        info.WF_ITEM = flowInstDTO.FlowItem;
                        info.WF_NODE_STRID= dicdata["firstNode"];//节点ID
                        info.WF_NODE = dicdata["firstNodeName"];//节点名称
                        //修改当前审批对象 最后和审批节点信息一起提交
                        DbClient.Updateable<DEV_CONTRACT>(info).AddQueue();
                        return info;
                    }
                default:
                    return null;


            }
        }



        /// <summary>
        /// 获取开始节点
        /// 第一个审批节点
        /// 以及链接线
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="edges"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetNodes(List<DEV_FLOWTEMP_NODE> nodes, List<DEV_FLOWTEMP_EDGE> edges)
        {
            var dicnode = new Dictionary<string, string>();
            var stratNode = nodes.Where(a =>a.N_TYPE == "StartNode").FirstOrDefault();
            var edgeInfo = edges.Where(a => a.SOURCENODEID == stratNode!.NODE_STRID).FirstOrDefault();
            var firstInfo = nodes.Where(a => a.NODE_STRID == edgeInfo!.TARGETNODEID).FirstOrDefault();
            dicnode.Add("stratNode", stratNode!.NODE_STRID);
            dicnode.Add("firstNode", edgeInfo!.TARGETNODEID);
            dicnode.Add("firstNodeName", firstInfo!.TEXT_VALUE);
            dicnode.Add("curredge", edgeInfo!.EDGE_STRID);
            return dicnode;
        }

        /// <summary>
        /// 获取节点信息表
        /// </summary>
        /// <param name="nodeInfos">节点信息集合</param>
        /// <returns>审批实例组</returns>
        public List<DEV_FLOW_INST_GROUPUSER> GetFlowInstGroupUser(List<DEV_FLOWTEMP_NODE_INFO> nodeInfos, Dictionary<string, string> dicdata, DEV_FLOW_INSTANCE InstInfo)
        {
            var firstnode = dicdata["firstNode"];
            var listGroupIds = nodeInfos.Where(a => a.NODE_STRID == firstnode && a.O_TYPE == (int)OptTypeEnum.FlowGroup)
                .Select(a => a.OPT_ID).ToList();
            var listgoupusers = DbClient.Queryable<DEV_FLOW_GROUPUSER>().Where(a => listGroupIds.Contains(a.GROUP_ID)).ToList();
            List<DEV_FLOW_INST_GROUPUSER> listInstGroups = new List<DEV_FLOW_INST_GROUPUSER>();
            foreach (var item in listgoupusers)
            {

                var info = new DEV_FLOW_INST_GROUPUSER();
                info.INST_ID = InstInfo.ID;
                info.GROUP_ID = item.GROUP_ID;
                info.USER_ID = item.USER_ID;
                info.GROUP_NAME = DevRedisUtility.GetGroupField(item.GROUP_ID);
                listInstGroups.Add(info);



            }

            return listInstGroups;
        }

        /// <summary>
        /// 创建待审批用户列表
        /// </summary>
        /// <param name="dicdata">当前审批节点信息</param>
        /// <param name="InstInfo">审批实例信息</param>
        /// <param name="nodeInfos">模板节点信息</param>
        /// <param name="userId">当前用户</param>
        /// <returns></returns>
        public List<DEV_FLOW_INST_WAIT_USER> CreateWaitUser(List<DEV_FLOWTEMP_NODE_INFO> nodeInfos,
            Dictionary<string, string> dicdata, DEV_FLOW_INSTANCE InstInfo, int userId)
        {

            var listwaitusers = new List<DEV_FLOW_INST_WAIT_USER>();
            var firstnode = dicdata["firstNode"];//第一个审批节点
                                                 //查询组用户
            var listGroupIds = nodeInfos.Where(a => a.NODE_STRID == firstnode && a.O_TYPE == (int)OptTypeEnum.FlowGroup)
                .Select(a => a.OPT_ID).ToList();
            var listspusers = nodeInfos.Where(a => a.NODE_STRID == firstnode && a.O_TYPE == (int)OptTypeEnum.UserRes)
                .Select(a => a.OPT_ID).ToList();
            var listgoupusers = DbClient.Queryable<DEV_FLOW_GROUPUSER>().Where(a => listGroupIds.Contains(a.GROUP_ID)).ToList();
            //遍历组用户
            foreach (var item in listgoupusers)
            {

                var info = new DEV_FLOW_INST_WAIT_USER();
                info.INST_ID = InstInfo.ID;
                info.NODE_STR = firstnode;
                info.USER_ID = item.USER_ID;
                info.FLOW_TYPE = InstInfo.FLOW_TYPE;
                info.FLOW_ITEM = InstInfo.FLOW_ITEM_ID;
                info.OBJ_NAME = InstInfo.NAME;
                info.OBJ_MONERY = InstInfo.APP_MONERY ?? 0;
                info.OBJ_ID = InstInfo.APP_ID;
                info.OBJ_CODE = InstInfo.CODE;
                info.ORDER_NUM = 0;
                info.FLOW_STATE = 1;//审批中
                info.IS_DELETE = 0;
                info.SP_TYPE = (int)OptTypeEnum.FlowGroup;
                info.SP_TYPE_OBJID = item.GROUP_ID;
                info.IS_AUTH = 0;
                info.CREATE_USERID = userId;
                info.CREATE_TIME = DateTime.Now;
                info.UPDATE_TIME = DateTime.Now;
                info.UPDATE_USERID = userId;
                listwaitusers.Add(info);




            }
            //遍历用户
            foreach (var item in listspusers)
            {

                var info = new DEV_FLOW_INST_WAIT_USER();
                info.INST_ID = InstInfo.ID;
                info.NODE_STR = firstnode;
                info.USER_ID = item;
                info.FLOW_TYPE = InstInfo.FLOW_TYPE;
                info.FLOW_ITEM = InstInfo.FLOW_ITEM_ID;
                info.OBJ_NAME = InstInfo.NAME;
                info.OBJ_MONERY = InstInfo.APP_MONERY ?? 0;
                info.OBJ_ID = InstInfo.APP_ID;
                info.OBJ_CODE = InstInfo.CODE;
                info.ORDER_NUM = 0;
                info.FLOW_STATE = 1;//审批中
                info.IS_DELETE = 0;
                info.SP_TYPE = (int)OptTypeEnum.UserRes;
                info.SP_TYPE_OBJID = item;
                info.IS_AUTH = 0;
                info.CREATE_USERID = userId;
                info.CREATE_TIME = DateTime.Now;
                info.UPDATE_TIME = DateTime.Now;
                info.UPDATE_USERID = userId;

                listwaitusers.Add(info);




            }
            return listwaitusers;


        }

        #endregion

        /// <summary>
        /// 审批历史记录列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where 条件</param>
        /// <param name="orderbyLambda">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public ResultPageData<DevFlowInstanceList> GetList(PageInfo<DEV_FLOW_INSTANCE> pageInfo, Expression<Func<DEV_FLOW_INSTANCE, bool>>? whereLambda,
            Expression<Func<DEV_FLOW_INSTANCE, object>> orderbyLambda, bool isAsc)
        {

            var tempquery = DbClient.Queryable<DEV_FLOW_INSTANCE>().Where(whereLambda);
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Asc);
            }
            else
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Desc);
            }

            int totalCount = 0;
            if ((pageInfo is NoPageInfo<DEV_FLOW_INSTANCE>))
            { //分页
                pageInfo.PageSize = 2000;
                pageInfo.PageIndex = 0;
            }
            var list = tempquery.ToPageList(pageInfo.PageIndex, pageInfo.PageSize, ref totalCount, a => new
            {
                ID = a.ID,
                NAME = a.NAME,//审批对象名称
                CODE = a.CODE,//审批对象编号
                FLOW_TYPE = a.FLOW_TYPE,//对象类别，客户，合同..
                APP_ID = a.APP_ID,//对象ID
                APP_MONERY = a.APP_MONERY,//对象金额
                START_USER_ID = a.START_USER_ID,//提交人
                CREATE_TIME = a.CREATE_TIME,//创建时间
                CREATE_USERID = a.CREATE_USERID,//创建人
                CURR_NODE_ID = a.CURR_NODE_ID,//当前节点ID
                CURR_NODE_NAME = a.CURR_NODE_NAME,//节点名称
                FLOW_ITEM_ID = a.FLOW_ITEM_ID,//审批事项
                FLOW_END_TIME = a.FLOW_END_TIME,//审批结束时间
                FLOW_STATE = a.FLOW_STATE,//

            });
            pageInfo.TotalCount = totalCount;
            var local = from a in list
                        select new DevFlowInstanceList
                        {
                            ID = a.ID,
                            NAME = a.NAME,//审批对象名称
                            CODE = a.CODE,//审批对象编号
                            FLOW_TYPE = a.FLOW_TYPE,//对象类别，客户，合同..
                            APP_ID = a.APP_ID,//对象ID
                            APP_MONERY = a.APP_MONERY,//对象金额
                            START_USER_ID = a.START_USER_ID,//提交人
                            CREATE_TIME = a.CREATE_TIME,//创建时间
                            CREATE_USERID = a.CREATE_USERID,//创建人
                            CURR_NODE_ID = a.CURR_NODE_ID,//当前节点ID
                            CURR_NODE_NAME = a.CURR_NODE_NAME,//节点名称
                            FLOW_ITEM_ID = a.FLOW_ITEM_ID,//审批事项
                            FLOW_END_TIME = a.FLOW_END_TIME,//审批结束时间
                            FlowTypeDic = EmunUtility.GetDesc(typeof(FlowObjEnums), a.FLOW_TYPE),
                            FlowItemDic = GetFlowItemDic(a.FLOW_TYPE, a.FLOW_ITEM_ID),
                            FLOW_STATE = a.FLOW_STATE,//
                            StateDic = EmunUtility.GetDesc(typeof(WorkFlowStateEnums), a.FLOW_STATE),
                            StartUserName = DevRedisUtility.GetUserField(a.START_USER_ID),


                        };
            return new ResultPageData<DevFlowInstanceList>()
            {
                items = local.ToList(),
                total = pageInfo.TotalCount,
                page = pageInfo.PageIndex,
                pageSize = pageInfo.PageSize


            };
        }

        /// <summary>
        /// 获取审批事项
        /// </summary>
        /// <param name="enumval">审批事项值</param>
        /// <param name="objEnum">审批对象枚举值，客户 供货商...</param>
        /// <returns></returns>
        private string GetFlowItemDic(int objEnum, int enumval)
        {
            var itemObjType = EmunUtility.GetEnumItemExAttribute(typeof(FlowObjEnums), objEnum);
            var list = EmunUtility.GetExtAttr(itemObjType.TypeValue);
            var enuminfo = list.Where(a => a.Value == enumval).FirstOrDefault();
            return enuminfo == null ? "" : enuminfo.Desc;
        }

        ///// <summary>
        ///// 根据当前人员获取当前审批对象是否可以审批
        ///// 让其在查看页面是否能看到审批按钮
        ///// </summary>
        ///<param name="approval">判断条件实例</param>
        ///<param name="userid">当前用户</param>
        ///// <returns></returns>
        public PersionApprovalInfo IsAppExistInfo(ApprovalActionDTO approval, int userid)
        {
            var currwaituser = DbClient.Queryable<DEV_FLOW_INST_WAIT_USER>()
                 .Where(a => a.OBJ_ID == approval.AppObjId && a.FLOW_TYPE == approval.AppObjType
                 && a.USER_ID == userid && a.FLOW_STATE == 1)
                 .Select(a => new PersionApprovalInfo
                 {
                     WaitId = a.ID,
                     InstId = a.INST_ID,
                     NodeId = a.NODE_STR,

                 }).First();
            if (currwaituser != null)
            {
                var nodeinfo = DbClient.Queryable<DEV_FLOW_INST_NODE>().Where(a => a.INST_ID == currwaituser.InstId && a.NODE_STRID == currwaituser.NodeId).First();
                currwaituser.ReText = nodeinfo != null ? (nodeinfo.RE_TEXT ?? 0) : 0;
            }
            else
            {
                currwaituser = new PersionApprovalInfo();
            }

            return currwaituser;



        }

        #region 提交审批意见



        /// <summary>
        /// 提交审批意见
        /// </summary>
        /// <param name="flowOption">审批意见相关信息</param>
        /// <param name="userId">审批人员</param>
        /// <returns></returns>
        public int SubmitOption(FlowOptionDTO flowOption, int userId)
        {
            //当前实例节点的待处理审批人员
            var waitusers = DbClient.Queryable<DEV_FLOW_INST_WAIT_USER>()
                .Where(a => a.INST_ID == flowOption.InstId && a.NODE_STR == flowOption.NodeId).ToList();
            //当前审批人员对象
            // var waitInfo = waitInfos.Where(a => a.ID == flowOption.WaitId).First();
            //审批实例
            var instInfo = DbClient.Queryable<DEV_FLOW_INSTANCE>().Where(a => a.ID == flowOption.InstId).First();
            //审批节点
            var listNodes = DbClient.Queryable<DEV_FLOW_INST_NODE>().Where(a => a.INST_ID == flowOption.InstId).ToList();
            //链接线
            var listEdges = DbClient.Queryable<DEV_FLOW_INST_EDGE>().Where(a => a.INST_ID == flowOption.InstId).ToList();
            //查询实例节点信息
            var listNodeInfo = DbClient.Queryable<DEV_FLOW_INST_NODE_INFO>()
                .Where(a => a.INST_ID == flowOption.InstId).ToList();

            if (flowOption.Sta==(int)OptionStateEnum.TongYi)
            {
                FlowTongYi(flowOption,userId,listNodes,listNodeInfo,listEdges, waitusers, instInfo);

            }
            else if (flowOption.Sta == (int)OptionStateEnum.BuTongYi)
            {
                FlowBuTongYi(flowOption, userId, listNodes, listNodeInfo, listEdges, waitusers, instInfo);
            }

            


            
            var ar = DbClient.SaveQueues();

            return ar;


        }

        /// <summary>
        /// 不同意操作
        /// </summary>
        /// <param name="flowOption">审批意见对象</param>
        /// <param name="userId">当前用户</param>
        /// <param name="listNodes">当前实例节点集合</param>
        /// <param name="listnodeInfos">当前实例节点信息对象集合</param>
        /// <param name="lsitEdngs">当前实例节点连接线</param>
        /// <param name="waitusers">当前节点等待审批用户列表</param>
        /// <param name="instInfo">当前实例</param>
        private void FlowBuTongYi(FlowOptionDTO flowOption, int userId, List<DEV_FLOW_INST_NODE> listNodes,
            List<DEV_FLOW_INST_NODE_INFO> listnodeInfos, List<DEV_FLOW_INST_EDGE> lsitEdngs,
            List<DEV_FLOW_INST_WAIT_USER> waitusers, DEV_FLOW_INSTANCE instInfo)
        {
            //1、修改当前节点
            //2、新增审批意见
            //3、修改审批对象
            //4、修改当前用户待处理意见
            //5、新增当前处理通过审批意见
            var currnode = listNodes.Where(a => a.NODE_STRID == flowOption.NodeId).FirstOrDefault();
            currnode.NODE_STATE = (int)WorkFlowStateEnums.BDH;
            currnode.UPDATE_TIME = DateTime.Now;
            currnode.UPDATE_USERID = userId;
            //1、修当前改节点
            DbClient.Updateable(currnode).AddQueue();
            //2、新增审批意见
            SubmitUserMsg(flowOption, userId);
            //3、修改审批对象
            UpdateObjState(flowOption, instInfo, currnode);
            //4、修改当前用户待处理意见
            var waituser = waitusers.Where(a => a.ID == flowOption.WaitId).FirstOrDefault();
            waituser.FLOW_STATE = (int)WorkFlowStateEnums.BDH;
            waituser.UPDATE_USERID=userId;
            waituser.UPDATE_TIME = DateTime.Now;
            //5、构建当前用户审批通过

            CreateEndUser(flowOption, userId, waituser);

        }




        /// <summary>
        /// 同意审批操作
        /// </summary>
        /// <param name="flowOption">审批意见</param>
        /// <param name="userId">当前用户</param>
        /// <param name="listNodes">当前实例节点信息集合</param>
        /// <param name="listnodeInfos">当前接待你信息集合</param>
        /// <param name="lsitEdngs">当前实例连接线</param>
        /// <param name="waitusers">当前节点等待，用来判断全部通过规则 是否满足跳转下一个节点</param>
        /// <param name="instInfo">实例信息</param>
        private void FlowTongYi(FlowOptionDTO flowOption, int userId,List<DEV_FLOW_INST_NODE> listNodes,
            List<DEV_FLOW_INST_NODE_INFO> listnodeInfos,List<DEV_FLOW_INST_EDGE> lsitEdngs,
            List<DEV_FLOW_INST_WAIT_USER> waitusers, DEV_FLOW_INSTANCE instInfo)
        {
            //1、修当前改节点
            //2、修改审批实例（需要判断是否需要修改）
            //3、修改审批对象（需要判断是否需要修改）
            //4、修改当前待处理审批
            //5、新增审批意见
            //6、新增处理完成用户列表
            //7、构建审批通过表
            //8、修改链接线

            var currnode = listNodes.Where(a => a.NODE_STRID == flowOption.NodeId).FirstOrDefault();
            if (currnode.NRULE==(int)NruleEnum.Qbtg)
            {
                var spcount = waitusers.Where(a => a.FLOW_STATE != 0).Count();
                if (spcount+1>= waitusers.Count())
                {
                    NextNodeToMsg(flowOption, userId, listNodes, listnodeInfos, lsitEdngs, waitusers, instInfo, currnode);

                }
                else
                {//当前节点还没有全部审批通过 还停留当前节点
                 //1、修改当前待处理审批
                 //2、新增审批意见
                 //3、构建当前用户审批通过
                    var waituser = waitusers.Where(a => a.ID == flowOption.WaitId).FirstOrDefault();
                    waituser.FLOW_STATE = (int)WorkFlowStateEnums.SPTG;//同意
                    waituser.UPDATE_TIME = DateTime.Now;
                    waituser.UPDATE_USERID = userId;
                    DbClient.Updateable(waituser).AddQueue();
                    //2、新增审批意见
                    SubmitUserMsg(flowOption, userId);
                    //3、构建当前用户审批通过
                    CreateEndUser(flowOption, userId, waituser);
                    

                }


            } 
            else if (currnode.NRULE == (int)NruleEnum.RyTg)
            {
                NextNodeToMsg(flowOption, userId, listNodes, listnodeInfos, lsitEdngs, waitusers, instInfo, currnode);
            }

        }

        /// <summary>
        /// 跳转到下一个节点
        /// 设置当前节点到下一个线条的状态
        /// </summary>
        /// <param name="lsitEdngs">审批实例连接线</param>
        /// <param name="currNode">当前节点</param>
        private void NextNodeSetEdge(List<DEV_FLOW_INST_EDGE> lsitEdngs, DEV_FLOW_INST_NODE currNode)
        {
            var edgeinfo = lsitEdngs.Where(a => a.SOURCENODEID == currNode.NODE_STRID).FirstOrDefault();
            edgeinfo.EDGE_STATE = 2;
            DbClient.Updateable(edgeinfo).AddQueue();



        }

        /// <summary>
        /// 可以流转到下一个节点的相关操作
        /// </summary>
        /// <param name="flowOption">审批意见</param>
        /// <param name="userId">当前用户</param>
        /// <param name="listNodes">当前实例节点</param>
        /// <param name="listnodeInfos">当前实例节点信息</param>
        /// <param name="lsitEdngs">当前实例节点连接线</param>
        /// <param name="waitusers">当前实例待处理用户</param>
        /// <param name="instInfo">当前实例信息</param>
        /// <param name="currnode">当前节点</param>
        private void NextNodeToMsg(FlowOptionDTO flowOption, int userId, List<DEV_FLOW_INST_NODE> listNodes, List<DEV_FLOW_INST_NODE_INFO> listnodeInfos, List<DEV_FLOW_INST_EDGE> lsitEdngs, List<DEV_FLOW_INST_WAIT_USER> waitusers, DEV_FLOW_INSTANCE instInfo, DEV_FLOW_INST_NODE? currnode)
        {
            currnode.NODE_STATE = (int)WorkFlowStateEnums.SPTG;
            currnode.UPDATE_TIME = DateTime.Now;
            currnode.UPDATE_USERID = userId;
            //1、修当前改节点
            DbClient.Updateable(currnode).AddQueue();

            var nextnode = GetNextNode(flowOption, listNodes, lsitEdngs);
            if (nextnode != null && nextnode.N_TYPE == "EndNode")
            {   //最后一个节点是 “结束”
                instInfo.UPDATE_TIME = DateTime.Now;
                instInfo.UPDATE_USERID = userId;
                instInfo.CURR_NODE_ID = nextnode.NODE_STRID;
                instInfo.CURR_NODE_NAME = nextnode.TEXT_VALUE;
                instInfo.FLOW_STATE = (int)WorkFlowStateEnums.SPTG;
                instInfo.FLOW_END_TIME = DateTime.Now;

                //最后一个节点状态设置通过
                nextnode.NODE_STATE= (int)WorkFlowStateEnums.SPTG;
                DbClient.Updateable(nextnode).AddQueue();


            }
            else
            {
                instInfo.UPDATE_TIME = DateTime.Now;
                instInfo.UPDATE_USERID = userId;
                instInfo.CURR_NODE_ID = nextnode.NODE_STRID;
                instInfo.CURR_NODE_NAME = nextnode.TEXT_VALUE;
                instInfo.FLOW_STATE = (int)WorkFlowStateEnums.SPZ;
                //6、新增下一个节点待处理审批
                CreateWaitUser(nextnode, listnodeInfos, instInfo, userId);
                //设置下一个节点为审批中
                nextnode.NODE_STATE = (int)WorkFlowStateEnums.SPZ;
                DbClient.Updateable(nextnode).AddQueue();

            }
            //2、修改审批实例（需要判断是否需要修改）
            DbClient.Updateable(instInfo).AddQueue();

            //3、修改审批对象（需要判断是否需要修改）
            UpdateObjState(flowOption, instInfo, nextnode);

            //4、修改当前待处理审批
            var waituser = waitusers.Where(a => a.ID == flowOption.WaitId).FirstOrDefault();
            waituser.FLOW_STATE = (int)WorkFlowStateEnums.SPTG;//同意
            waituser.UPDATE_TIME = DateTime.Now;
            waituser.UPDATE_USERID = userId;
            DbClient.Updateable(waituser).AddQueue();
            //5、修改审批意见
            SubmitUserMsg(flowOption, userId);
            //7、构建当前用户审批通过表
            CreateEndUser(flowOption, userId, waituser);
            //8、修改线条
            NextNodeSetEdge(lsitEdngs, currnode);
        }

        /// <summary>
        ///创建当前审批通过用户
        /// </summary>
        /// <param name="flowOption">审批意见用户</param>
        /// <param name="userId">当前用户</param>
        /// <param name="waitInfo">当前用户待处理信息</param>
        private void CreateEndUser(FlowOptionDTO flowOption, int userId, DEV_FLOW_INST_WAIT_USER waitInfo)
        {
            //审批通过实体
            var enduserinfo = AutoMapperHelper.Map<DEV_FLOW_INST_WAIT_USER, DEV_FLOW_INST_END_USER>(waitInfo);
            if (flowOption.Sta == (int)OptionStateEnum.TongYi)
            {
                enduserinfo.FLOW_STATE = (int)WorkFlowStateEnums.SPTG;
            }
            else if (flowOption.Sta == (int)OptionStateEnum.BuTongYi)
            {
                enduserinfo.FLOW_STATE = (int)WorkFlowStateEnums.BDH;
            }
            enduserinfo.START_TIME = waitInfo.CREATE_TIME;
            enduserinfo.WF_OPTION = flowOption.Msg;
            enduserinfo.END_TIME = DateTime.Now;
            enduserinfo.UPDATE_TIME = DateTime.Now;
            enduserinfo.UPDATE_USERID = userId;
            //审批通过列表
            DbClient.Insertable(enduserinfo).AddQueue();
        }

        /// <summary>
        /// 新增下一个节点的 待处理审批人员
        /// </summary>
        /// <param name="nextNode">下一个节点</param>
        /// <param name="nodeinfos">节点信息列表</param>
        /// <param name="InstInfo">实例信息</param>
        /// <param name="userId">当前用户</param>
        public void CreateWaitUser(DEV_FLOW_INST_NODE nextNode, List<DEV_FLOW_INST_NODE_INFO> nodeinfos, DEV_FLOW_INSTANCE InstInfo, int userId)
        {
            List<DEV_FLOW_INST_WAIT_USER> listwaitusers = new List<DEV_FLOW_INST_WAIT_USER>();
            var nextnodeInfos = nodeinfos.Where(a => a.NODE_STRID == nextNode.NODE_STRID).ToList();
            if (nextNode.N_TYPE != "EndNode")
            {//下一个节点不是结束
                var listGroupIds = nodeinfos.Where(a => a.NODE_STRID == nextNode.NODE_STRID && a.O_TYPE == (int)OptTypeEnum.FlowGroup)
                .Select(a => a.OPT_ID).ToList();
                var listspusers = nodeinfos.Where(a => a.NODE_STRID == nextNode.NODE_STRID && a.O_TYPE == (int)OptTypeEnum.UserRes)
                    .Select(a => a.OPT_ID).ToList();
                var listgoupusers = DbClient.Queryable<DEV_FLOW_INST_GROUPUSER>().Where(a => listGroupIds.Contains(a.GROUP_ID)).ToList();
                //遍历组用户
                foreach (var item in listgoupusers)
                {

                    var info = new DEV_FLOW_INST_WAIT_USER();
                    info.INST_ID = InstInfo.ID;
                    info.NODE_STR = nextNode.NODE_STRID;
                    info.USER_ID = item.USER_ID;
                    info.FLOW_TYPE = InstInfo.FLOW_TYPE;
                    info.FLOW_ITEM = InstInfo.FLOW_ITEM_ID;
                    info.OBJ_NAME = InstInfo.NAME;
                    info.OBJ_MONERY = InstInfo.APP_MONERY ?? 0;
                    info.OBJ_ID = InstInfo.APP_ID;
                    info.OBJ_CODE = InstInfo.CODE;
                    info.ORDER_NUM = 0;
                    info.FLOW_STATE = (int)WorkFlowStateEnums.SPZ;//审批中
                    info.IS_DELETE = 0;
                    info.SP_TYPE = (int)OptTypeEnum.FlowGroup;
                    info.SP_TYPE_OBJID = item.GROUP_ID;
                    info.IS_AUTH = 0;
                    info.CREATE_USERID = userId;
                    info.CREATE_TIME = DateTime.Now;
                    info.UPDATE_TIME = DateTime.Now;
                    info.UPDATE_USERID = userId;
                    listwaitusers.Add(info);




                }
                //遍历用户
                foreach (var item in listspusers)
                {

                    var info = new DEV_FLOW_INST_WAIT_USER();
                    info.INST_ID = InstInfo.ID;
                    info.NODE_STR = nextNode.NODE_STRID;
                    info.USER_ID = item;
                    info.FLOW_TYPE = InstInfo.FLOW_TYPE;
                    info.FLOW_ITEM = InstInfo.FLOW_ITEM_ID;
                    info.OBJ_NAME = InstInfo.NAME;
                    info.OBJ_MONERY = InstInfo.APP_MONERY ?? 0;
                    info.OBJ_ID = InstInfo.APP_ID;
                    info.OBJ_CODE = InstInfo.CODE;
                    info.ORDER_NUM = 0;
                    info.FLOW_STATE = (int)WorkFlowStateEnums.SPZ;//审批中
                    info.IS_DELETE = 0;
                    info.SP_TYPE = (int)OptTypeEnum.UserRes;
                    info.SP_TYPE_OBJID = item;
                    info.IS_AUTH = 0;
                    info.CREATE_USERID = userId;
                    info.CREATE_TIME = DateTime.Now;
                    info.UPDATE_TIME = DateTime.Now;
                    info.UPDATE_USERID = userId;

                    listwaitusers.Add(info);




                }

            }

            if (listwaitusers.Count > 0)
            {
                DbClient.Insertable(listwaitusers).AddQueue();
            }

        }


        /// <summary>
        /// 提交审批意见
        /// </summary>
        /// <param name="flowOption">意见对象</param>
        /// <param name="userId">当前用户</param>
        private void SubmitUserMsg(FlowOptionDTO flowOption, int userId)
        {
            var option = new DEV_FLOW_INST_OPTION();
            option.NODE_STR_ID = flowOption.NodeId;
            option.INST_ID = flowOption.InstId;
            option.INST_ID = flowOption.InstId;
            option.USER_ID = userId;
            option.APP_STATE = flowOption.Sta;
            option.IS_DELETE = 0;
            option.CREATE_USERID = userId;
            option.UPDATE_USERID = userId;
            option.CREATE_TIME = DateTime.Now;
            option.UPDATE_TIME = DateTime.Now;
            option.APP_OPTION = flowOption.Msg;
            DbClient.Insertable(option).AddQueue();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flowOption"></param>
        /// <param name="listNodes"></param>
        /// <param name="lsitEdngs"></param>
        /// <returns></returns>
        private DEV_FLOW_INST_NODE GetNextNode(FlowOptionDTO flowOption,List<DEV_FLOW_INST_NODE> listNodes, List<DEV_FLOW_INST_EDGE> lsitEdngs)
        {
            var edge = lsitEdngs.Where(a => a.SOURCENODEID == flowOption.NodeId).FirstOrDefault();
            var nextNode = listNodes.Where(a => a.NODE_STRID == edge!.TARGETNODEID).FirstOrDefault();
            return nextNode;


        }

        /// <summary>
        ///修改审批对象
        /// </summary>
        /// <param name="flowOption">审批意见对象</param>
        /// <param name="instInfo">实例对象</param>
        /// <param name="netxtNode">下一个节点或者当前节点</param>
        public void UpdateObjState(FlowOptionDTO flowOption, DEV_FLOW_INSTANCE instInfo,
            DEV_FLOW_INST_NODE netxtNode)
        {
            switch (instInfo.FLOW_TYPE)
            {
                case (int)FlowObjEnums.Customer:
                    {
                        var objinfo = DbClient.Queryable<DEV_COMPANY>().Where(a => a.ID == instInfo.APP_ID).First();
                        if (flowOption.Sta== (int)OptionStateEnum.TongYi)
                        {
                           
                            if (netxtNode.N_TYPE == "EndNode")
                            {
                                objinfo.WF_STATE = (int)WorkFlowStateEnums.SPTG;
                                objinfo.WF_NODE_STRID = "";
                                objinfo.WF_NODE = "";
                                objinfo.WF_ITEM = null;
                                objinfo.C_STATE = (int)CompanyStateEnums.SHTG;
                            }
                            else
                            {
                                objinfo.WF_STATE = (int)WorkFlowStateEnums.SPZ;
                                objinfo.WF_NODE_STRID = netxtNode.NODE_STRID;
                                objinfo.WF_NODE = netxtNode.TEXT_VALUE;
                            }



                        }
                        else
                        {
                            objinfo.WF_STATE = (int)WorkFlowStateEnums.BDH;
                        }

                        DbClient.Updateable(objinfo).AddQueue();

                    }
                    break;

                case (int)FlowObjEnums.Contract:
                    {
                        var objinfo = DbClient.Queryable<DEV_CONTRACT>().Where(a => a.ID == instInfo.APP_ID).First();
                        if (flowOption.Sta == (int)OptionStateEnum.TongYi)
                        {

                            if (netxtNode.N_TYPE == "EndNode")
                            {
                                objinfo.WF_STATE = (int)WorkFlowStateEnums.SPTG;
                                objinfo.WF_NODE_STRID = "";
                                objinfo.WF_NODE = "";
                                objinfo.WF_ITEM = null;
                                objinfo.CONT_STATE = (int)ContractStateEnums.Approve;
                            }
                            else
                            {
                                objinfo.WF_STATE = (int)WorkFlowStateEnums.SPZ;
                                objinfo.WF_NODE_STRID = netxtNode.NODE_STRID;
                                objinfo.WF_NODE = netxtNode.TEXT_VALUE;
                            }



                        }
                        else
                        {
                            objinfo.WF_STATE = (int)WorkFlowStateEnums.BDH;
                        }

                        DbClient.Updateable(objinfo).AddQueue();

                    }
                    break;
            }

        }



        #endregion

        #region 审批实例流程图

        /// <summary>
        /// 根据审批实例ID获取流出图
        /// </summary>
        /// <param name="instId">审批实例Id</param>
        /// <returns></returns>
        public FlowInstChartData GetFlowChart(int instId)
        {
            //List<DEV_FLOWTEMP_NODE> FlowNodes = new List<DEV_FLOWTEMP_NODE>();
            //List<DEV_FLOWTEMP_EDGE> FlowEdges = new List<DEV_FLOWTEMP_EDGE>();
            var listnodes = DbClient.Queryable<DEV_FLOW_INST_NODE>().Where(a => a.INST_ID == instId).ToList();
            var listedges = DbClient.Queryable<DEV_FLOW_INST_EDGE>().Where(a => a.INST_ID == instId).ToList();

            return new FlowInstChartData { FlowNodes = listnodes, FlowEdges = listedges };

        }
        #endregion

        #region 审批意见表

        /// <summary>
        /// 审批节点意见列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where 条件</param>
        /// <param name="orderbyLambda">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public ResultPageData<FlowInstNodeInfoMsg> GetNodeInfoList(PageInfo<DEV_FLOW_INST_NODE_INFO> pageInfo, Expression<Func<DEV_FLOW_INST_NODE_INFO, bool>>? whereLambda,
            Expression<Func<DEV_FLOW_INST_NODE_INFO, object>> orderbyLambda, bool isAsc,int instId,string nodestr)
        {
            var listdataMsg = DbClient.Queryable<DEV_FLOW_INST_END_USER>()
                .Where(a => a.INST_ID == instId && a.NODE_STR == nodestr).ToList();
            var tempquery = DbClient.Queryable<DEV_FLOW_INST_NODE_INFO>().Where(whereLambda);
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Asc);
            }
            else
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Desc);
            }

            int totalCount = 0;
            if ((pageInfo is NoPageInfo<DEV_FLOW_INST_NODE_INFO>))
            { //分页
                pageInfo.PageSize = 2000;
                pageInfo.PageIndex = 0;
            }
            var list = tempquery.ToPageList(pageInfo.PageIndex, pageInfo.PageSize, ref totalCount, a => new {
                ID = a.ID,
                OPT_ID = a.OPT_ID,
                OPT_NAME = a.OPT_NAME,
                NODE_STRID = a.NODE_STRID,
                O_TYPE = a.O_TYPE,
                INFO_STATE = a.INFO_STATE,
                INST_ID=a.INST_ID,

            });
            pageInfo.TotalCount = totalCount;
            var local = from a in list
                        select new FlowInstNodeInfoMsg
                        {
                            ID = a.ID,
                            OPT_ID = a.OPT_ID,
                            OPT_NAME = a.OPT_NAME,
                            NODE_STRID = a.NODE_STRID,
                            O_TYPE = a.O_TYPE,
                            OtypeDsc = EmunUtility.GetDesc(typeof(OptTypeEnum), a.O_TYPE),
                            INFO_STATE = a.INFO_STATE,
                            ObjName = FlowUtility.GetObjName(a.O_TYPE, a.OPT_ID),
                            NodeMsg= listdataMsg.Where(p => p.SP_TYPE == a.O_TYPE && p.SP_TYPE_OBJID == a.OPT_ID).Select(p => new
                            NodeMsg
                            {
                                Msg = p.WF_OPTION,
                                UserName = DevRedisUtility.GetUserField(p.USER_ID),
                                StartTime = p.START_TIME,
                                EndTime = p.END_TIME

                            }).ToList()
        };
            return new ResultPageData<FlowInstNodeInfoMsg>()
            {
                items = local.ToList(),
                total = pageInfo.TotalCount,
                page = pageInfo.PageIndex,
                pageSize = pageInfo.PageSize


            };


        }
       
        #endregion
    }
}
