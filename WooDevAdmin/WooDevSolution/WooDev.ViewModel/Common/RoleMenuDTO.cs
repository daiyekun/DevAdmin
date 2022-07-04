using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel
{

    /// <summary>
    /// 角色菜单对象
    /// </summary>
    public class RoleMenuDTO
    {

        /// <summary>
        /// 角色ID
        /// </summary>
       public int RoleId { get; set; }
        /// <summary>
        /// 分配的菜单ID
        /// </summary>
       public List<int> MenuIds { get; set; }
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public int UserId { get; set; }
    }
}
