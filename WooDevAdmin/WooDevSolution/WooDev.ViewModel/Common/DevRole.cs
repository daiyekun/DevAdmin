using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel
{

    /// <summary>
    /// 修改角色状态对象
    /// </summary>
    public class DevRoleStatus
    {
        /// <summary>
        /// id 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }
    }

    /// <summary>
    /// 角色
    /// </summary>
    public partial class DevRoleDTO
    {
        /// <summary>
        /// 菜单权限
        /// </summary>
        public List<int> menu { get; set; }

    }
}
