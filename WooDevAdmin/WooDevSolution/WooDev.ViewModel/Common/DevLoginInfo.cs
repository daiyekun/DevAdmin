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
        /// 登录ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 登录token
        /// </summary>
        public string? Token { get; set; }
        /// <summary>
        ///角色信息
        /// </summary>
        public RoleInfo? Role { get; set; }
        /// <summary>
        /// 登录结果
        /// </summary>
        public int Reult { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public LoginUser? LoginUser { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public string? RoleIds { get; set; }

    }

    /// <summary>
    /// vben 登录
    /// </summary>
    public class DevLogin 
    {
        /// <summary>
        /// 登录ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 登录token
        /// </summary>
        public string? Token { get; set; }
        /// <summary>
        ///角色信息
        /// </summary>
        public RoleInfo? Role { get; set; }
        /// <summary>
        /// 登录结果
        /// </summary>
        public int Reult { get; set; }
    }


    /// <summary>
    /// 角色信息
    /// </summary>
    public class RoleInfo
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string? RoleName { get; set; }
        /// <summary>
        /// 角色值
        /// </summary>
        public string? Value { get; set; }
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

    /// <summary>
    /// 登录以后获取用户信息
    /// </summary>
    public class CurrLoginUser
    {
        /// <summary>
        /// 角色集合
        /// </summary>
        public List<RoleInfo> roles = new List<RoleInfo>();
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// 真实名称
        /// </summary>
        public string? RealName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string? Avatar { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Desc { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartName { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

    }


}
