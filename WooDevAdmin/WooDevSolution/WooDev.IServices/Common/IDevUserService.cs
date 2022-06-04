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
    /// 系统用户
    /// </summary>
     public partial  interface IDevUserService
     {
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        AjaxListResult<DevUserList> GetList<s>(PageInfo<DEV_USER> pageInfo, Expression<Func<DEV_USER, bool>> whereLambda,
            Expression<Func<DEV_USER, object>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="LoginName">登录名称</param>
        /// <param name="Pwd">密码</param>
        /// <returns></returns>
         LoginResult Login(string LoginName, string Pwd);
     }
}
