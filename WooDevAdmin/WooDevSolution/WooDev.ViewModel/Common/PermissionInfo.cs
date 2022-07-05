using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel
{

    /// <summary>
    /// 权限
    /// </summary>
    public class PermissionInfo
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 权限标识
        /// </summary>
        public string? Permission { get; set; }
        /// <summary>
        /// 权限分配
        /// </summary>
        public int Pssionlb { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public int RoleId { get; set; }
    }
}
