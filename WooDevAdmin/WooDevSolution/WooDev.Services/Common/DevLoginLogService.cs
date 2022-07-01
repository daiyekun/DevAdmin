using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Models;
using WooDev.Model.Models;
using WooDev.ViewModel;

namespace WooDev.Services
{

    /// <summary>
    /// 登录日志
    /// </summary>
    public partial class DevLoginLogService
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
        public ResultPageData<DevLoginLogList> GetList(PageInfo<DEV_LOGIN_LOG> pageInfo, Expression<Func<DEV_LOGIN_LOG, bool>> whereLambda,
             Expression<Func<DEV_LOGIN_LOG, object>> orderbyLambda, bool isAsc)
        {

            var tempquery = DbClient.Queryable<DEV_LOGIN_LOG>().Where(whereLambda);
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
                            CREATE_TIME = a.CREATE_TIME,
                            CREATE_USERID = a.CREATE_USERID,
                            LOGIN_RES=a.LOGIN_RES,
                            IP=a.IP,
                            
                           
                        };
            int totalCount = 0;
            var list = query.ToPageList(pageInfo.PageIndex, pageInfo.PageSize, ref totalCount);
            pageInfo.TotalCount = totalCount;
            var local = from a in list
                        select new DevLoginLogList
                        {
                            ID = a.ID,
                            NAME = a.NAME,//显示名称
                            CREATE_TIME = a.CREATE_TIME,
                            CREATE_USERID = a.CREATE_USERID,
                            LOGIN_RES = a.LOGIN_RES,
                            IP = a.IP,

                        };
            return new ResultPageData<DevLoginLogList>()
            {
                items = local.ToList(),
                total = pageInfo.TotalCount,
                page = pageInfo.PageIndex,
                pageSize = pageInfo.PageSize


            };
        }
    }
}
