using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel
{
   

    /// <summary>
    /// 系统登录返回
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// 登录标识
        /// </summary>
        public int Reult { get; set; } = 0;
        /// <summary>
        /// 登录用户信息
        /// </summary>
        public LoginUser LoginUser
        {
            get; set;
        }
        /// <summary>
        /// 登录token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 登录key 当作sessionkey使用
        /// </summary>
        public string LoginKey { get; set; }
    }
    /// <summary>
    /// 登录用户
    /// </summary>
    public class LoginUser
    {
        /// <summary>
        /// 当前用户Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        ///角色ID集合
        /// </summary>

        public string RoleIds { get; set; }
        
    }

}
