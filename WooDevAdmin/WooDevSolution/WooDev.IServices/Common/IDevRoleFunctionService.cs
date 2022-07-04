using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.ViewModel;

namespace WooDev.IServices
{

    /// <summary>
    /// 角色菜单权限
    /// </summary>
    public partial interface IDevRoleFunctionService
    {
        /// <summary>
        /// 新增角色菜单权限
        /// </summary>
        /// <param name="roleMenuDTO">角色菜单权限对象</param>
        void SaveRoleMenus(RoleMenuDTO roleMenuDTO);
    }
}
