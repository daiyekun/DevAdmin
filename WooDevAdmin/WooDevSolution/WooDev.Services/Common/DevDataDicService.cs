using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Models;
using WooDev.IServices.Common;
using WooDev.Model.Models;
using WooDev.ViewModel.Common;

namespace WooDev.Services.Common
{
    public class DevDataDicService:BaseService<DEV_DATADIC>, IDevDataDicService
    {
        public DevDataDicService(ISqlSugarClient DbClient) : base(DbClient)
        {

        }

        /// <summary>
        /// 字典列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where 条件</param>
        /// <param name="orderbyLambda">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public AjaxListResult<DevDataDicListView> GetList<s>(PageInfo<DEV_DATADIC> pageInfo, Expression<Func<DEV_DATADIC, bool>> whereLambda,
             Expression<Func<DEV_DATADIC, object>> orderbyLambda, bool isAsc)
        {

            var tempquery = DbClient.Queryable<DEV_DATADIC>().Where(whereLambda);
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
                            ORDER_NUM=a.ORDER_NUM,//排序
                            REMARK=a.REMARK,
                            CODE=a.CODE,
                            PID=a.PID,
                            APP_TYPE=a.APP_TYPE,
                            SORT_NAME=a.SORT_NAME,//简称
                            
                        };
            int totalCount = 0;
            var list = query.ToPageList(pageInfo.PageIndex, pageInfo.PageSize, ref totalCount);
            pageInfo.TotalCount = totalCount;
            var local = from a in list
                        select new DevDataDicListView
                        {
                            ID = a.ID,
                            NAME = a.NAME,//显示名称
                            ORDER_NUM = a.ORDER_NUM,//排序
                            REMARK = a.REMARK,
                            CODE = a.CODE,
                            PID = a.PID,
                            APP_TYPE = a.APP_TYPE,
                            SORT_NAME = a.SORT_NAME,//简称

                        };
            return new AjaxListResult<DevDataDicListView>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
    }
}
