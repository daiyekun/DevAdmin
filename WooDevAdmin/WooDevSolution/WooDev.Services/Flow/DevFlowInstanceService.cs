using Google.Protobuf.WellKnownTypes;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;
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
            var tempquery = DbClient.Queryable<DEV_FLOW_TEMP>()
                .Where(a => a.OBJ_TYPE == appConditions.FlowObj
                        && (',' + a.CATE_IDS + ',').Contains(',' + appConditions.CateId.ToString() + ',')
                        && (',' + a.FLOW_ITEMS + ',').Contains(',' + appConditions.FlowItem.ToString() + ',')
                        && (',' + a.DEPART_IDS + ',').Contains(',' + appConditions.DeptId.ToString() + ',')

                );
            switch (appConditions.FlowObj)
            {
                case (int)FlowObjEnums.Contract:
                case (int)FlowObjEnums.InvoiceIn:
                case (int)FlowObjEnums.InvoiceOut:
                case (int)FlowObjEnums.payment:
                    return tempquery.Where(a => a.MIN_MONERY >= appConditions.Monery && appConditions.Monery <= a.MAX_MONERY).Single();
                default:
                    return tempquery.Single();


            }
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

            #region 审批对象信息获取以及修改  其他审批添加基本修改这里
            UpdateAppObject(flowInstDTO, userId, InstInfo, dicdata);
            #endregion
            //审批实例对象新增到数据库
            var inceInfo = DbClient.Insertable<DEV_FLOW_INSTANCE>(InstInfo).ExecuteReturnEntity();

            #region 开始组装审批节点，链接线，审批信息等


            List<DEV_FLOW_INST_NODE> tlistNodes = CreateInstNodes(userId, listNodes, dicdata, inceInfo);
            List<DEV_FLOW_INST_NODE_INFO> tlistNodeInfos = CreateInstNodeInfos(userId, listNodeInfos, inceInfo);
            List<DEV_FLOW_INST_EDGE> tlistEdges = CreateEdges(userId, listEdges, dicdata, inceInfo);
            DbClient.Insertable<DEV_FLOW_INST_NODE>(tlistNodes).AddQueue();
            DbClient.Insertable<DEV_FLOW_INST_NODE_INFO>(tlistNodeInfos).AddQueue();
            DbClient.Insertable<DEV_FLOW_INST_EDGE>(tlistEdges).AddQueue();

            var ar = DbClient.SaveQueuesAsync();

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

        private  List<DEV_FLOW_INST_EDGE> CreateEdges(int userId, List<DEV_FLOWTEMP_EDGE> listEdges, Dictionary<string, string> dicdata, DEV_FLOW_INSTANCE inceInfo)
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
        private  List<DEV_FLOW_INST_NODE_INFO> CreateInstNodeInfos(int userId, List<DEV_FLOWTEMP_NODE_INFO> listNodeInfos, DEV_FLOW_INSTANCE inceInfo)
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
                instNode.INFO_STATE = 0;
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

        private  List<DEV_FLOW_INST_NODE> CreateInstNodes(int userId, List<DEV_FLOWTEMP_NODE> listNodes, Dictionary<string, string> dicdata, DEV_FLOW_INSTANCE inceInfo)
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
                if (dicdata["stratNode"] == item.NODE_STRID)
                    if (item.TEXT_VALUE == FlowStaticData.StratNodeText)
                    {//开始节点
                        node.NODE_STATE = (int)FlowStateEnum.State2;
                    }
                    else if (dicdata["firstNode"] == item.NODE_STRID)
                    {//审批中
                        node.NODE_STATE = (int)FlowStateEnum.State1;
                    }
                    else
                    {
                        node.NODE_STATE = (int)FlowStateEnum.State0;
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
        private void UpdateAppObject(FlowInstDTO flowInstDTO, int userId, DEV_FLOW_INSTANCE InstInfo, Dictionary<string, string> dicdata)
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


                    }
                    break;

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
            var stratNode = nodes.Where(a => a.TEXT_VALUE == FlowStaticData.StratNodeText && a.N_TYPE == "circle").FirstOrDefault();
            var edgeInfo = edges.Where(a => a.SOURCENODEID == stratNode!.NODE_STRID).FirstOrDefault();
            var firstInfo=nodes.Where(a => a.NODE_STRID == edgeInfo!.TARGETNODEID).FirstOrDefault();
            dicnode.Add("stratNode", stratNode!.NODE_STRID);
            dicnode.Add("firstNode", edgeInfo!.TARGETNODEID);
            dicnode.Add("firstNodeName", firstInfo!.TEXT_VALUE);
            dicnode.Add("curredge", edgeInfo!.EDGE_STRID);
            return dicnode;
        }

        #endregion 
    }
}
