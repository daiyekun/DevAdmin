using SqlSugar;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Extend;
using WooDev.Common.Models;
using WooDev.Common.Utility;
using WooDev.Model.Models;
using WooDev.ViewModel;

namespace WooDev.Services
{

    /// <summary>
    /// 计划资金
    /// </summary>
    public partial class DevContPlanFinanceService
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
        public ResultPageData<DevContPlanFinanceList> GetList(PageInfo<DEV_CONT_PLAN_FINANCE> pageInfo, Expression<Func<DEV_CONT_PLAN_FINANCE, bool>> whereLambda,
             Expression<Func<DEV_CONT_PLAN_FINANCE, object>> orderbyLambda, bool isAsc)
        {

            var tempquery = DbClient.Queryable<DEV_CONT_PLAN_FINANCE>().Where(whereLambda);
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
                            AMOUNT=a.AMOUNT,//金额
                            SETT_YPE = a.SETT_YPE,//结算方式
                            CONT_ID = a.CONT_ID,//合同ID
                            PLAN_DATE = a.PLAN_DATE,//计划完成日期
                            REMARK = a.REMARK,//说明
                            CREATE_USERID = a.CREATE_USERID,//创建人
                            F_STATE=a.F_STATE,//状态



                        };
            int totalCount = 0;
            if ((pageInfo is NoPageInfo<DEV_CONT_PLAN_FINANCE>))
            { //分页
                pageInfo.PageSize = 200000;
                pageInfo.PageIndex = 0;
            }
            var list = query.ToPageList(pageInfo.PageIndex, pageInfo.PageSize, ref totalCount);
            pageInfo.TotalCount = totalCount;
            var local = from a in list
                        select new DevContPlanFinanceList
                        {
                            ID = a.ID,
                            NAME = a.NAME,//显示名称
                            AMOUNT = a.AMOUNT,//金额
                            SETT_YPE = a.SETT_YPE,//结算方式
                            CONT_ID = a.CONT_ID,//合同ID
                            PLAN_DATE = a.PLAN_DATE,//计划完成日期
                            REMARK = a.REMARK,//说明
                            CREATE_USERID = a.CREATE_USERID,//创建人
                            F_STATE = a.F_STATE,//状态
                            CreateUserName = DevRedisUtility.GetUserField(a.CREATE_USERID),
                            AMOUNT_Thod = a.AMOUNT.ThousandsSeparator(),
                            SETT_YPE_DSC = DevRedisUtility.GetDataField(a.SETT_YPE ?? 0),//结算方式
                            SettType = a.SETT_YPE.ToString()
                        };
            return new ResultPageData<DevContPlanFinanceList>()
            {
                items = local.ToList(),
                total = pageInfo.TotalCount,
                page = pageInfo.PageIndex,
                pageSize = pageInfo.PageSize


            };
        }
    }
}
