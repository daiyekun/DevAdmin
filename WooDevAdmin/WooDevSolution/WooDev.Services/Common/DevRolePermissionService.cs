using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;
using SqlSugar;

namespace WooDev.Services
{
    /// <summary>
    /// 权限
    /// </summary>
    public partial class DevRolePermissionService
    {
        /// <summary>
        /// 根据角色查询菜单ID
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public List<int> GetMenuIdByRoleId(int roleId)
        {

          var listmenuIds= DbClient.Queryable<DEV_ROLE_FUNCTION>()
                .Where(a => a.ROLE == roleId).Select(a=>a.FUNCTION_ID).ToList();
            return listmenuIds;
        }
    }
}
