using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Models;
using WooDev.Model.Models;
using WooDev.ViewModel;
using WooDev.ViewModel.ExtendModel;

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
        ResultPageData<DevUserList> GetList(PageInfo<DEV_USER> pageInfo, Expression<Func<DEV_USER, bool>> whereLambda,
            Expression<Func<DEV_USER, object>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="LoginName">登录名称</param>
        /// <param name="Pwd">密码</param>
        /// <returns></returns>
        LoginResult Login(string LoginName, string Pwd);
        /// <summary>
        /// 根据用户ID获取用户部分信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>CurrLoginUser 登录以后返回的部分信息</returns>
        CurrLoginUser GetUserInfoById(int userId);
        /// <summary>
        /// 根据where条件判断用户是否存在
        /// </summary>
        /// <param name="whereLambda">where条件</param>
        /// <returns>是否存在</returns>
        bool IsAccountExist(Expression<Func<DEV_USER, bool>> whereLambda);
        /// <summary>
        /// 设置Redis
        /// </summary>
        /// <returns></returns>
        void SetRedisHash();
        /// <summary>
        /// 根据ID获取用户详细信息
        /// </summary>
        /// <returns>用户详细信息</returns>
        DevUserViewInfo GetViewInfoById(int id);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="Ids">选择用户</param>
        /// <returns></returns>
        int RestPwd(string Ids);

    }
}
