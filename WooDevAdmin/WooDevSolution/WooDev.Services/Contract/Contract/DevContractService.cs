using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Models;
using WooDev.Common.Utility;
using WooDev.Model.Models;
using WooDev.ViewModel.Contract.Enums;
using WooDev.ViewModel.Enums;
using WooDev.ViewModel;
using WooDev.Common.Extend;
using Dev.WooNet.AutoMapper.Extend;
using WooDev.ViewModel.Contract.ExcelModel;

namespace WooDev.Services
{

    /// <summary>
    /// 合同
    /// </summary>
    public partial class DevContractService
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public ResultPageData<DevContractList> GetList(PageInfo<DEV_CONTRACT> pageInfo, Expression<Func<DEV_CONTRACT, bool>> whereLambda,
             Expression<Func<DEV_CONTRACT, object>> orderbyLambda, bool isAsc)
        {

            var tempquery = DbClient.Queryable<DEV_CONTRACT>()
                .LeftJoin<DEV_COMPANY>((a, cus) => a.COMP_ID == cus.ID).Where(whereLambda);
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Asc);
            }
            else
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Desc);
            }

            var query = tempquery.Select((a, cus) => new
            {
                ID = a.ID,
                C_NAME = a.C_NAME,//显示名称
                C_CODE = a.C_CODE,//编号
                CATE_ID = a.CATE_ID,//类别
                ANT_MONERY = a.ANT_MONERY,//金额
                CONT_STATE = a.CONT_STATE,//合同状态
                CREATE_USERID = a.CREATE_USERID,//创建人
                CREATE_TIME = a.CREATE_TIME,//创建时间
                HEAD_USER_ID = a.HEAD_USER_ID,//负责人
                WF_ITEM = a.WF_ITEM,//审批事项
                WF_STATE = a.WF_STATE,//流程状态
                WF_NODE = a.WF_NODE,//流程节点
                DEPART_ID = a.DEPART_ID,//经办机构
                COMP_ID = a.COMP_ID,//合同对方ID
                SIGNING_DATE = a.SIGNING_DATE,//签订日期
                EFFECTIVE_DATE = a.EFFECTIVE_DATE,//生效日期
                PLAN_DATE = a.PLAN_DATE,//计划完成日期
                ACT_DATE = a.ACT_DATE,//实际完成日期
                IS_FRAMEWORK = a.IS_FRAMEWORK,//框架合同标准合同
                IS_SUBCONT = a.IS_SUBCONT,//是否总包
                ComName = cus.NAME,//合同对方名称
                MAIN_DEPART_ID = a.MAIN_DEPART_ID,//签约主体
            });
                     
            int totalCount = 0;
            if ((pageInfo is NoPageInfo<DEV_CONTRACT>))
            { //分页
                pageInfo.PageSize = 2000000;
                pageInfo.PageIndex = 0;
            }
            var list = query.ToPageList(pageInfo.PageIndex, pageInfo.PageSize, ref totalCount);
            pageInfo.TotalCount = totalCount;
            var local = from a in list
                        select new DevContractList
                        {
                            ID = a.ID,
                            C_NAME = a.C_NAME,//显示名称
                            C_CODE = a.C_CODE,//编号
                            CATE_ID = a.CATE_ID,//类别
                            ANT_MONERY = a.ANT_MONERY,//金额
                            CONT_STATE = a.CONT_STATE,//合同状态
                            CREATE_USERID = a.CREATE_USERID,//创建人
                            CREATE_TIME = a.CREATE_TIME,//创建时间
                            HEAD_USER_ID = a.HEAD_USER_ID,//负责人
                            WF_ITEM = a.WF_ITEM,//审批事项
                            WF_STATE = a.WF_STATE,//流程状态
                            WF_NODE = a.WF_NODE,//流程节点
                            DEPART_ID = a.DEPART_ID,//经办机构
                            COMP_ID = a.COMP_ID,//合同对方ID
                            SIGNING_DATE = a.SIGNING_DATE,//签订日期
                            EFFECTIVE_DATE = a.EFFECTIVE_DATE,//生效日期
                            PLAN_DATE = a.PLAN_DATE,//计划完成日期
                            ACT_DATE = a.ACT_DATE,//实际完成日期
                            IS_FRAMEWORK = a.IS_FRAMEWORK,//框架合同标准合同
                            IS_SUBCONT = a.IS_SUBCONT,//是否总包
                            WfFlowDic = EmunUtility.GetDesc(typeof(ContFlowItemsEnum), a.WF_ITEM ?? 0),//审批事项
                            StateDic = EmunUtility.GetDesc(typeof(ContractStateEnums), a.CONT_STATE),
                            WfState = EmunUtility.GetDesc(typeof(WorkFlowStateEnums), a.WF_STATE ?? -1),
                            CreateUserName = DevRedisUtility.GetUserField(a.CREATE_USERID),
                            LeadUserName = DevRedisUtility.GetUserField(a.HEAD_USER_ID ?? -1),
                            CateName = DevRedisUtility.GetDataField(a.CATE_ID),//合同类别
                            ComName = a.ComName,//合同对方名称
                            ANT_MONERYThod=a.ANT_MONERY.ThousandsSeparator(),
                            DeptName= DevRedisUtility.GetDeptName(a.DEPART_ID),
                            MainDeptName = DevRedisUtility.GetDeptName(a.MAIN_DEPART_ID),
                            ContPro = EmunUtility.GetDesc(typeof(ContractProperty), (a.IS_FRAMEWORK)),//合同属性


                        };
            return new ResultPageData<DevContractList>()
            {
                items = local.ToList(),
                total = pageInfo.TotalCount,
                page = pageInfo.PageIndex,
                pageSize = pageInfo.PageSize


            };
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="devCompanyDTO"></param>
        /// <param name="userId">当前登录人ID </param>
        public void ConstractSave(DevContractDTO devCompanyDTO, int userId)
        {


            if (devCompanyDTO.ID > 0)
            {
                var saveinfo = DbClient.Queryable<DEV_CONTRACT>().Where(a => a.ID == devCompanyDTO.ID).Single();
                AutoMapperHelper.Map<DevContractDTO, DEV_CONTRACT>(devCompanyDTO, saveinfo);
                //saveinfo.C_TYPE = 0;
                saveinfo.UPDATE_USERID = userId;
                saveinfo.UPDATE_TIME = DateTime.Now;
                Update(saveinfo);
                UpdateItemData(saveinfo.ID, userId);
                CreateHistory(saveinfo.ID, userId);

            }
            else
            {
                var info = AutoMapperHelper.Map<DevContractDTO, DEV_CONTRACT>(devCompanyDTO);
                info.CREATE_TIME = DateTime.Now;
                info.UPDATE_TIME = DateTime.Now;
               // info.C_TYPE = 0;
                info.CREATE_USERID = userId;
                info.UPDATE_USERID = userId;
                var currinfo = Add(info);
                UpdateItemData(currinfo.ID, userId);
                CreateHistory(currinfo.ID, userId);
            }

          

        }

        /// <summary>
        /// 清理垃圾个人数据
        /// </summary>
        public void ClearData(int userId)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append($"DELETE FROM DEV_CONT_CONTTEXT WHERE CONT_ID={-userId};");
            sqlstr.Append($"DELETE FROM DEV_CONT_ATTACHMENT WHERE CONT_ID={-userId};");
            sqlstr.Append($"DELETE FROM DEV_CONT_DESC WHERE CONT_ID={-userId};");
            sqlstr.Append($"DELETE FROM DEV_CONT_SUB_MATTER WHERE CONT_ID={-userId};");
            sqlstr.Append($"DELETE FROM DEV_CONT_PLAN_FINANCE WHERE CONT_ID={-userId};");
            
            ExecuteCommand(sqlstr.ToString());
        }

        /// <summary>
        /// 清洗数据
        /// </summary>
        /// <param name="compId">对方ID</param>
        /// <param name="userId">当前用户ID</param>
        public void UpdateItemData(int compId, int userId)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append($"UPDATE DEV_CONT_CONTTEXT SET CONT_ID={compId}  WHERE CONT_ID={-userId};");
            sqlstr.Append($"UPDATE DEV_CONT_ATTACHMENT SET CONT_ID={compId} WHERE CONT_ID={-userId};");
            sqlstr.Append($"UPDATE DEV_CONT_DESC SET CONT_ID={compId} WHERE CONT_ID={-userId};");
            sqlstr.Append($"UPDATE DEV_CONT_SUB_MATTER SET CONT_ID={compId} WHERE CONT_ID={-userId};");
            sqlstr.Append($"UPDATE DEV_CONT_PLAN_FINANCE SET CONT_ID={compId} WHERE CONT_ID={-userId};");

            ExecuteCommand(sqlstr.ToString());

        }

        /// <summary>
        /// 查看详情
        /// </summary>
        /// <param name="Id">客户ID</param>
        /// <returns></returns>
        public DevContractView ShowDetail(int Id)
        {
            var tempquery = DbClient.Queryable<DEV_CONTRACT>()
                
                .LeftJoin<DEV_COMPANY>((a, cus) => a.COMP_ID == cus.ID).Where(a => a.ID == Id);
                
            var query = tempquery.Select((a, cus) => new
                        
                        {
                            ID = a.ID,
                            C_NAME = a.C_NAME,//显示名称
                            C_CODE = a.C_CODE,//编号
                            CATE_ID = a.CATE_ID,//类别
                            ANT_MONERY = a.ANT_MONERY,//金额
                            CONT_STATE = a.CONT_STATE,//合同状态
                            CREATE_USERID = a.CREATE_USERID,//创建人
                            CREATE_TIME = a.CREATE_TIME,//创建时间
                            HEAD_USER_ID = a.HEAD_USER_ID,//负责人
                            WF_ITEM = a.WF_ITEM,//审批事项
                            WF_STATE = a.WF_STATE,//流程状态
                            WF_NODE = a.WF_NODE,//流程节点
                            DEPART_ID = a.DEPART_ID,//经办机构
                            COMP_ID = a.COMP_ID,//合同对方ID
                            SIGNING_DATE = a.SIGNING_DATE,//签订日期
                            EFFECTIVE_DATE = a.EFFECTIVE_DATE,//生效日期
                            PLAN_DATE = a.PLAN_DATE,//计划完成日期
                            ACT_DATE = a.ACT_DATE,//实际完成日期
                            IS_FRAMEWORK = a.IS_FRAMEWORK,//框架合同标准合同
                            IS_SUBCONT = a.IS_SUBCONT,//是否总包
                            ComName = cus.NAME,//合同对方名称
                            MAIN_DEPART_ID = a.MAIN_DEPART_ID,//签约主体
                            SOURCE_ID=a.SOURCE_ID,
                            OTHER_CODE=a.OTHER_CODE,//对方编号
                            EST_MONERY= a.EST_MONERY,//预估金额
                            ADVANCE_MONERY=a.ADVANCE_MONERY,//预收预付

                        });

            var list = query.ToList();
            var local = from a in list
                        select new DevContractView
                        {
                            ID = a.ID,
                            C_NAME = a.C_NAME,//显示名称
                            C_CODE = a.C_CODE,//编号
                            CATE_ID = a.CATE_ID,//类别
                            ANT_MONERY = a.ANT_MONERY,//金额
                            CONT_STATE = a.CONT_STATE,//合同状态
                            CREATE_USERID = a.CREATE_USERID,//创建人
                            CREATE_TIME = a.CREATE_TIME,//创建时间
                            HEAD_USER_ID = a.HEAD_USER_ID,//负责人
                            WF_ITEM = a.WF_ITEM,//审批事项
                            WF_STATE = a.WF_STATE,//流程状态
                            WF_NODE = a.WF_NODE,//流程节点
                            DEPART_ID = a.DEPART_ID,//经办机构
                            COMP_ID = a.COMP_ID,//合同对方ID
                            SIGNING_DATE = a.SIGNING_DATE,//签订日期
                            EFFECTIVE_DATE = a.EFFECTIVE_DATE,//生效日期
                            PLAN_DATE = a.PLAN_DATE,//计划完成日期
                            ACT_DATE = a.ACT_DATE,//实际完成日期
                            IS_FRAMEWORK = a.IS_FRAMEWORK,//框架合同标准合同
                            IS_SUBCONT = a.IS_SUBCONT,//是否总包
                            WfFlowDic = EmunUtility.GetDesc(typeof(ContFlowItemsEnum), a.WF_ITEM ?? 0),//审批事项
                            StateDic = EmunUtility.GetDesc(typeof(ContractStateEnums), a.CONT_STATE),
                            WfState = EmunUtility.GetDesc(typeof(WorkFlowStateEnums), a.WF_STATE ?? -1),
                            CreateUserName = DevRedisUtility.GetUserField(a.CREATE_USERID),
                            HEAD_USER_NAME = DevRedisUtility.GetUserField(a.HEAD_USER_ID ?? -1),
                            CateName = DevRedisUtility.GetDataField(a.CATE_ID),//合同类别
                            ComName = a.ComName,//合同对方名称
                            ANT_MONERYThod = a.ANT_MONERY.ThousandsSeparator(),
                            DeptName = DevRedisUtility.GetDeptName(a.DEPART_ID),
                            MainDeptName = DevRedisUtility.GetDeptName(a.MAIN_DEPART_ID),
                            SOURCE_ID = a.SOURCE_ID,
                            ContPro = EmunUtility.GetDesc(typeof(ContractProperty), (a.IS_FRAMEWORK)),//合同属性
                            SourceName = DevRedisUtility.GetDataField(a.SOURCE_ID??0),//合同来源
                            OTHER_CODE = a.OTHER_CODE,//对方编号
                            IS_SUBCONT_DSC= a.IS_SUBCONT==1?"是":"否",
                            EST_MONERY_Thod=a.EST_MONERY.ThousandsSeparator(),
                            ADVANCE_MONERY_Thod=a.ADVANCE_MONERY.ThousandsSeparator(),

                        };
            return local.FirstOrDefault();

        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="updateState">修改数据对象</param>
        /// <param name="userId">当前用户</param>
        /// <returns></returns>
        public int UpdateState(UpdateStateDTO updateState, int userId)
        {
            var saveinfo = DbClient.Queryable<DEV_CONTRACT>().Where(a => a.ID == updateState.Id).Single();
            if (saveinfo != null)
            {
                saveinfo.CONT_STATE = updateState.State;
                saveinfo.UPDATE_TIME = DateTime.Now;
                saveinfo.UPDATE_USERID = userId;
                return Update(saveinfo!);
            }
            return -1;


        }
        /// <summary>
        /// 创建历史表
        /// </summary>
        public void CreateHistory(int contId, int userId)
        { 
            //当前需要copy的合同ID
            var saveinfo = DbClient.Queryable<DEV_CONTRACT>().Where(a => a.ID == contId).Single();
            var infohist = AutoMapperHelper.Map<DEV_CONTRACT, DEV_CONTRACT_HIST>(saveinfo);
            infohist.CREATE_TIME = DateTime.Now;
            infohist.CREATE_USERID = userId;
            infohist.UPDATE_TIME = DateTime.Now;
            infohist.UPDATE_USERID = userId;
            infohist.IS_DELETE = 0;
            infohist.CONT_ID = saveinfo.ID;
            var histinfo= DbClient.Insertable<DEV_CONTRACT_HIST>(infohist).ExecuteReturnEntity();
           
            
            #region 计划资金历史
            
            var list = DbClient.Queryable<DEV_CONT_PLAN_FINANCE>().Where(a => a.CONT_ID == contId).ToList();
            List<DEV_CONT_PLAN_FINANCE_HIST> listPlanFincehist = new List<DEV_CONT_PLAN_FINANCE_HIST>();
            foreach (var item in list)
            {
                var findedata = AutoMapperHelper.Map<DEV_CONT_PLAN_FINANCE, DEV_CONT_PLAN_FINANCE_HIST>(item);
                findedata.CREATE_TIME = DateTime.Now;
                findedata.CREATE_USERID = userId;
                findedata.UPDATE_TIME = DateTime.Now;
                findedata.UPDATE_USERID = userId;
                findedata.IS_DELETE = 0;
                findedata.CONT_ID = saveinfo.ID;
                findedata.CONT_HIST_ID = histinfo.ID;
                listPlanFincehist.Add(findedata);

            }
            DbClient.Insertable(listPlanFincehist).AddQueue();
            #endregion

            #region 合同文本历史

           
            List<DEV_CONT_CONTTEXT_HIST> listtexthist = new List<DEV_CONT_CONTTEXT_HIST>();
            var listtxts = DbClient.Queryable<DEV_CONT_CONTTEXT>().Where(a => a.CONT_ID == contId).ToList();
            foreach (var item in listtxts)
            {
                var textdata = AutoMapperHelper.Map<DEV_CONT_CONTTEXT, DEV_CONT_CONTTEXT_HIST>(item);
                textdata.CREATE_TIME = DateTime.Now;
                textdata.CREATE_USERID = userId;
                textdata.UPDATE_TIME = DateTime.Now;
                textdata.UPDATE_USERID = userId;
                textdata.IS_DELETE = 0;
                textdata.CONT_ID = saveinfo.ID;
                textdata.CONT_HIST_ID = histinfo.ID;
                listtexthist.Add(textdata);

            }

            DbClient.Insertable(listtexthist).AddQueue();

            #endregion

            #region 标的历史


            List<DEV_CONT_SUB_MATTER_HIST> listsubmatterhist = new List<DEV_CONT_SUB_MATTER_HIST>();
            var listsubmatters = DbClient.Queryable<DEV_CONT_SUB_MATTER>().Where(a => a.CONT_ID == contId).ToList();
            foreach (var item in listsubmatters)
            {
                var textdata = AutoMapperHelper.Map<DEV_CONT_SUB_MATTER, DEV_CONT_SUB_MATTER_HIST>(item);
                textdata.CREATE_TIME = DateTime.Now;
                textdata.CREATE_USERID = userId;
                textdata.UPDATE_TIME = DateTime.Now;
                textdata.UPDATE_USERID = userId;
                textdata.IS_DELETE = 0;
                textdata.CONT_ID = saveinfo.ID;
                textdata.CONT_HIST_ID = histinfo.ID;
                listsubmatterhist.Add(textdata);

            }

            DbClient.Insertable(listsubmatterhist).AddQueue();

            #endregion

            var ar = DbClient.SaveQueues();



        }

    }
}
