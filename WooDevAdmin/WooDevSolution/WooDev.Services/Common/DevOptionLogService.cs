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

namespace WooDev.Services
{

    /// <summary>
    /// 操作日志
    /// </summary>
    public partial class DevOptionLogService
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
        public ResultPageData<DevOptionLogList> GetList(PageInfo<DEV_OPTION_LOG> pageInfo, Expression<Func<DEV_OPTION_LOG, bool>> whereLambda,
             Expression<Func<DEV_OPTION_LOG, object>> orderbyLambda, bool isAsc)
        {

            var tempquery = DbClient.Queryable<DEV_OPTION_LOG>().Where(whereLambda);
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
                            NAME = a.NAME,//操作名称
                            REQ_URL = a.REQ_URL,//操作路径
                            REQ_PARAMETER = a.REQ_PARAMETER,//操作参数
                            REQ_RESULT = a.REQ_RESULT,//操作结果
                            CREATE_TIME = a.CREATE_TIME,
                            IP = a.IP,//Ip
                            CREATE_USERID = a.CREATE_USERID,
                            ACTION_NAME = a.ACTION_NAME,
                            REQ_METHOD = a.REQ_METHOD,//request 方法 get post ..
                            OPTION_TYPE = a.OPTION_TYPE,//操作类型 增加 删 改
                            
                            REMARK = a.REMARK,//描述


                        };
            int totalCount = 0;
            var list = query.ToPageList(pageInfo.PageIndex, pageInfo.PageSize, ref totalCount);
            pageInfo.TotalCount = totalCount;
            var local = from a in list
                        select new DevOptionLogList
                        {
                            ID = a.ID,
                            NAME = a.NAME,//操作名称
                            REQ_URL=a.REQ_URL,//操作路径
                            REQ_PARAMETER=a.REQ_PARAMETER,//操作参数
                            REQ_RESULT=a.REQ_RESULT,//操作结果
                            CREATE_TIME = a.CREATE_TIME,
                            CREATE_USERID = a.CREATE_USERID,
                            ACTION_NAME = a.ACTION_NAME,
                            REQ_METHOD=a.REQ_METHOD,//request 方法 get post ..
                            OPTION_TYPE=a.OPTION_TYPE,//操作类型 增加 删 改
                            IP = a.IP,
                            REMARK=a.REMARK,//描述
                            LoginName= DevRedisUtility.GetUserField(a.CREATE_USERID,"LOGIN_NAME"),
                            ShowName = DevRedisUtility.GetUserField(a.CREATE_USERID)

                        };
            return new ResultPageData<DevOptionLogList>()
            {
                items = local.ToList(),
                total = pageInfo.TotalCount,
                page = pageInfo.PageIndex,
                pageSize = pageInfo.PageSize


            };
        }
    }
}
