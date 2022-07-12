using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.IServices
{

    /// <summary>
    /// 权限
    /// </summary>
    public partial interface IDevRolePermissionService
    {
        /// <summary>
        /// 根据角色查询菜单ID
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        List<int> GetMenuIdByRoleId(int roleId);
    }
}
