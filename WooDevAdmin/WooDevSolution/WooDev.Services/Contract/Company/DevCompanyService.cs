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
                            StateDic = EmunUtility.GetDesc(typeof(StateEnums), a.C_STATE),
                            CreateUserName = DevRedisUtility.GetUserField(a.CREATE_USERID),
                            LeadUserName= DevRedisUtility.GetUserField(a.LEAD_USERID??-1),
                            WfState = EmunUtility.GetDesc(typeof(WorkFlowStateEnums), a.WF_STATE??-1),
                            CrateName = "",//信用等级
                            LevelName = "",//可以级别
                            CateName = "",//类别
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
        public void CompanySave(DevCompanyDTO devCompanyDTO)
        {
            var info = AutoMapperHelper.Map<DevCompanyDTO, DEV_COMPANY>(devCompanyDTO);
            
            if (devCompanyDTO.ID>0)
            {
               Update(info);

            }
            else
            {
                info.CREATE_TIME = DateTime.Now;
                info.UPDATE_TIME = DateTime.Now;
                Add(info);
            }

        }

    }
}
