using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;
using WooDev.ViewModel;

namespace WooDev.IServices
{

    /// <summary>
    /// 菜单
    /// </summary>
    public partial interface IDevFunctionMenuService
    {
        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <param name="whereLambda">where 条件</param>
        /// <returns></returns>
        IList<DEV_FUNCTION_MENU> GetListByWhere(Expression<Func<DEV_FUNCTION_MENU, bool>> whereLambda);

        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <param name="whereLambda">条件</param>
        /// <returns></returns>
        List<MenuItem> GetMenus(Expression<Func<DEV_FUNCTION_MENU, bool>> whereLambda);

        /// <summary>
        /// 查询菜单大列表
        /// </summary>
        /// <param name="whereLambda">where 条件</param>
        /// <returns></returns>
        List<MenuList> GetList(Expression<Func<DEV_FUNCTION_MENU, bool>> whereLambda);
        /// <summary>
        /// 查询菜单大列表权限
        /// </summary>
        /// <param name="whereLambda">where 条件</param>
        /// <returns></returns>
        List<RoleMenuList> GetRolePermissionList(Expression<Func<DEV_FUNCTION_MENU, bool>> whereLambda);
        /// <summary>
        /// 查询菜单大列表权限
        /// </summary>
        /// <param name="whereLambda">where 条件</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        List<PermissionList> GetPermissionList(Expression<Func<DEV_FUNCTION_MENU, bool>> whereLambda, int roleId);
    }
}
