using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Models;
using WooDev.Model.Models;
using WooDev.ViewModel;

namespace WooDev.IServices
{

    /// <summary>
    /// 登录日志
    /// </summary>
    public partial interface IDevLoginLogService
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
        ResultPageData<DevLoginLogList> GetList(PageInfo<DEV_LOGIN_LOG> pageInfo, Expression<Func<DEV_LOGIN_LOG, bool>> whereLambda,
            Expression<Func<DEV_LOGIN_LOG, object>> orderbyLambda, bool isAsc);
    }
}
