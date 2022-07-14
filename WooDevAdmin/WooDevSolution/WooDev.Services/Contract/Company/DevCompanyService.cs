using Dev.WooNet.AutoMapper.Extend;
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
using WooDev.ViewModel;
using WooDev.ViewModel.Contract.Enums;
using WooDev.ViewModel.Enums;

namespace WooDev.Services
{

    /// <summary>
    /// 合同对方操作
    /// </summary>
    public partial class DevCompanyService
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
        public ResultPageData<DevCompanyList> GetList(PageInfo<DEV_COMPANY> pageInfo, Expression<Func<DEV_COMPANY, bool>> whereLambda,
             Expression<Func<DEV_COMPANY, object>> orderbyLambda, bool isAsc)
        {

            var tempquery = DbClient.Queryable<DEV_COMPANY>().Where(whereLambda);
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Asc);
            }
            else
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Desc);
            }

            var query = from a in tempquery
                        select new
                        {
                            ID = a.ID,
                            NAME = a.NAME,//显示名称
                            CODE=a.CODE,//编号
                            S_NAME=a.S_NAME,//简称
                            TRADE=a.TRADE,//行业
                            LEVEL_ID =a.LEVEL_ID,//级别
                            CATE_ID=a.CATE_ID,//类别
                            CREATE_ID=a.CREATE_ID,//信用等级
                            C_STATE=a.C_STATE,//状态
                            WF_STATE=a.WF_STATE,//流程状态
                            WF_NODE=a.WF_NODE,//流程节点
                            WF_ITEM=a.WF_ITEM,//审批事项
                            FIELD1=a.FIELD1,//备用1
                            FIELD2 = a.FIELD2,//备用2
                            CREATE_USERID=a.CREATE_USERID,//创建人
                            CREATE_TIME=a.CREATE_TIME,//创建时间
                            LEAD_USERID=a.LEAD_USERID,//负责人

                        };
            int totalCount = 0;
            var list = query.ToPageList(pageInfo.PageIndex, pageInfo.PageSize, ref totalCount);
            pageInfo.TotalCount = totalCount;
            var local = from a in list
                        select new DevCompanyList
                        {
                            ID = a.ID,
                            NAME = a.NAME,//显示名称
                            CODE = a.CODE,//编号
                            TRADE = a.TRADE,//行业
                            LEVEL_ID = a.LEVEL_ID,//级别
                            CATE_ID = a.CATE_ID,//类别
                            CREATE_ID = a.CREATE_ID,//信用等级
                            C_STATE = a.C_STATE,//状态
                            WF_STATE = a.WF_STATE,//流程状态
                            WF_NODE = a.WF_NODE,//流程节点
                            WF_ITEM = a.WF_ITEM,//审批事项
                            FIELD1 = a.FIELD1,//备用1
                            FIELD2 = a.FIELD2,//备用2
                            CREATE_USERID = a.CREATE_USERID,//创建人
                            CREATE_TIME = a.CREATE_TIME,//创建时间
                            LEAD_USERID = a.LEAD_USERID,//负责人
                            StateDic = EmunUtility.GetDesc(typeof(CompanyStateEnums), a.C_STATE),
                            CreateUserName = DevRedisUtility.GetUserField(a.CREATE_USERID),
                            LeadUserName= DevRedisUtility.GetUserField(a.LEAD_USERID??-1),
                            WfState = EmunUtility.GetDesc(typeof(WorkFlowStateEnums), a.WF_STATE??-1),
                            CrateName = DevRedisUtility.GetDataField(a.CREATE_ID??0),//信用等级
                            LevelName = DevRedisUtility.GetDataField(a.LEVEL_ID??0),//可以级别
                            CateName = DevRedisUtility.GetDataField(a.CATE_ID),//类别
                        };
            return new ResultPageData<DevCompanyList>()
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
        public void CompanySave(DevCompanyDTO devCompanyDTO,int userId)
        {
            
            
            if (devCompanyDTO.ID>0)
            {
                var saveinfo = DbClient.Queryable<DEV_COMPANY>().Where(a=>a.ID== devCompanyDTO.ID).Single();
                AutoMapperHelper.Map<DevCompanyDTO, DEV_COMPANY>(devCompanyDTO, saveinfo);
                saveinfo.C_TYPE = 0;
                saveinfo.UPDATE_USERID = userId;
                saveinfo.UPDATE_TIME = DateTime.Now;
               Update(saveinfo);
               UpdateItemData(saveinfo.ID, userId);

            }
            else
            {
                var info = AutoMapperHelper.Map<DevCompanyDTO, DEV_COMPANY>(devCompanyDTO);
                info.CREATE_TIME = DateTime.Now;
                info.UPDATE_TIME = DateTime.Now;
                info.C_TYPE = 0;
                info.CREATE_USERID = userId;
                info.UPDATE_USERID= userId;
                var currinfo=Add(info);
                UpdateItemData(currinfo.ID, userId);
            }

        }

        /// <summary>
        /// 清理垃圾个人数据
        /// </summary>
        public void ClearData(int userId)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append($"DELETE FROM DEV_COMP_FILE WHERE COMP_ID={-userId};");
            sqlstr.Append($"DELETE FROM DEV_COMP_CONTACTS WHERE COMP_ID={-userId};");
            sqlstr.Append($"DELETE FROM DEV_COMP_RECORD WHERE COMP_ID={-userId};");

            ExecuteCommand(sqlstr.ToString()) ;
        }
        /// <summary>
        /// 清洗数据
        /// </summary>
        /// <param name="compId">对方ID</param>
        /// <param name="userId">当前用户ID</param>
        public void UpdateItemData(int compId,int userId)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append($"UPDATE DEV_COMP_FILE SET COMP_ID={compId}  WHERE COMP_ID={-userId};");
            sqlstr.Append($"UPDATE DEV_COMP_CONTACTS SET COMP_ID={compId} WHERE COMP_ID={-userId};");
            sqlstr.Append($"UPDATE DEV_COMP_RECORD SET COMP_ID={compId} WHERE COMP_ID={-userId};");

            ExecuteCommand(sqlstr.ToString());

        }

        /// <summary>
        /// 查看详情
        /// </summary>
        /// <param name="Id">客户ID</param>
        /// <returns></returns>
        public DevCompanyView ShowDetail(int Id)
        {
            var tempquery = DbClient.Queryable<DEV_COMPANY>().Where(a=>a.ID== Id);
            var query = from a in tempquery
                        select new
                        {
                            ID = a.ID,
                            NAME = a.NAME,//显示名称
                            CODE = a.CODE,//编号
                            S_NAME = a.S_NAME,//简称
                            TRADE = a.TRADE,//行业
                            LEVEL_ID = a.LEVEL_ID,//级别
                            CATE_ID = a.CATE_ID,//类别
                            CREATE_ID = a.CREATE_ID,//信用等级
                            C_STATE = a.C_STATE,//状态
                            WF_STATE = a.WF_STATE,//流程状态
                            WF_NODE = a.WF_NODE,//流程节点
                            WF_ITEM = a.WF_ITEM,//审批事项
                            FIELD1 = a.FIELD1,//备用1
                            FIELD2 = a.FIELD2,//备用2
                            CREATE_USERID = a.CREATE_USERID,//创建人
                            CREATE_TIME = a.CREATE_TIME,//创建时间
                            LEAD_USERID = a.LEAD_USERID,//负责人

                        };
           
            var list = query.ToList();
            var local = from a in list
                        select new DevCompanyView
                        {
                            ID = a.ID,
                            NAME = a.NAME,//显示名称
                            CODE = a.CODE,//编号
                            TRADE = a.TRADE,//行业
                            LEVEL_ID = a.LEVEL_ID,//级别
                            CATE_ID = a.CATE_ID,//类别
                            CREATE_ID = a.CREATE_ID,//信用等级
                            C_STATE = a.C_STATE,//状态
                            WF_STATE = a.WF_STATE,//流程状态
                            WF_NODE = a.WF_NODE,//流程节点
                            WF_ITEM = a.WF_ITEM,//审批事项
                            FIELD1 = a.FIELD1,//备用1
                            FIELD2 = a.FIELD2,//备用2
                            CREATE_USERID = a.CREATE_USERID,//创建人
                            CREATE_TIME = a.CREATE_TIME,//创建时间
                            LEAD_USERID = a.LEAD_USERID,//负责人
                            StateDic = EmunUtility.GetDesc(typeof(CompanyStateEnums), a.C_STATE),
                            CreateUserName = DevRedisUtility.GetUserField(a.CREATE_USERID),
                            LeadUserName = DevRedisUtility.GetUserField(a.LEAD_USERID ?? -1),
                            WfState = EmunUtility.GetDesc(typeof(WorkFlowStateEnums), a.WF_STATE ?? -1),
                            CrateName = DevRedisUtility.GetDataField(a.CREATE_ID ?? 0),//信用等级
                            LevelName = DevRedisUtility.GetDataField(a.LEVEL_ID ?? 0),//可以级别
                            CateName = DevRedisUtility.GetDataField(a.CATE_ID),//类别
                        };
            return local.FirstOrDefault();

        }

    }
}
