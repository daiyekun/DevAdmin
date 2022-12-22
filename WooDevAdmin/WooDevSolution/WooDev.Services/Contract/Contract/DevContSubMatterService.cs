using SqlSugar;
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
    /// 标的
    /// </summary>
    public partial class DevContSubMatterService
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
        public ResultPageData<DevContSubMatterList> GetList(PageInfo<DEV_CONT_SUB_MATTER> pageInfo, Expression<Func<DEV_CONT_SUB_MATTER, bool>> whereLambda,
             Expression<Func<DEV_CONT_SUB_MATTER, object>> orderbyLambda, bool isAsc)
        {

            var tempquery = DbClient.Queryable<DEV_CONT_SUB_MATTER>().Where(whereLambda);
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
                            S_NAME = a.S_NAME,//名称
                            SPEC = a.SPEC,//规格
                            S_TYPE = a.S_TYPE,//型号
                            UNIT = a.UNIT,//单位
                            AMOUNT = a.AMOUNT,//数量
                            PRICE = a.PRICE,//单价
                            SUBTOTAL = a.SUBTOTAL,//小计
                            CREATE_TIME = a.CREATE_TIME,
                            PLAN_DATE = a.PLAN_DATE,//计划交付时间
                            SALE_PRICE = a.SALE_PRICE,//销售报价
                            REMARK = a.REMARK,//备注
                            CREATE_USERID = a.CREATE_USERID,//创建人



                        };
            int totalCount = 0;
            if ((pageInfo is NoPageInfo<DEV_CONT_SUB_MATTER>))
            { //分页
                pageInfo.PageSize = 2000000;
                pageInfo.PageIndex = 0;
            }
            var list = query.ToPageList(pageInfo.PageIndex, pageInfo.PageSize, ref totalCount);
            pageInfo.TotalCount = totalCount;
            var local = from a in list
                        select new DevContSubMatterList
                        {
                            ID = a.ID,
                            S_NAME = a.S_NAME,//名称
                            SPEC = a.SPEC,//规格
                            S_TYPE = a.S_TYPE,//型号
                            UNIT = a.UNIT,//单位
                            AMOUNT = a.AMOUNT,//数量
                            PRICE = a.PRICE,//单价
                            SUBTOTAL = a.SUBTOTAL,//小计
                            CREATE_TIME = a.CREATE_TIME,
                            PLAN_DATE = a.PLAN_DATE,//计划交付时间
                            SALE_PRICE = a.SALE_PRICE,//销售报价
                            REMARK = a.REMARK,//备注
                            CREATE_USERID = a.CREATE_USERID,//创建人
                            SUBTOTAL_Thod=a.SUBTOTAL.ThousandsSeparator(),
                            CreateUserName = DevRedisUtility.GetUserField(a.CREATE_USERID),

                        };
            return new ResultPageData<DevContSubMatterList>()
            {
                items = local.ToList(),
                total = pageInfo.TotalCount,
                page = pageInfo.PageIndex,
                pageSize = pageInfo.PageSize


            };
        }
    }
}
