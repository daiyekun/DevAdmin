using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel
{

    /// <summary>
    /// 角色列表
    /// </summary>
    public partial class DevRoleList
    {
        /// <summary>
        /// 角色对应的菜单权限
        /// </summary>
        public List<int>? menu { get; set; }
    }
}
