using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;

namespace WooDev.ViewModel
{

    /// <summary>
    /// 用户列表list
    /// </summary>
    public partial class DevUserList
    {
        /// <summary>
        /// 性别描述
        /// </summary>
      public string SexDic { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
      public string UstateDic { get; set; }

    }
    /// <summary>
    /// 搜索对象
    /// </summary>
    public class DevUserSearch
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LOGIN_NAME { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
        public int deptId { get; set; } = 0;
        /// <summary>
        /// 组ID-用户组使用
        /// </summary>
        public int GroupId { get; set; }
    }
    /// <summary>
    /// 账号对象
    /// </summary>
    public class ExistAcccod
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
    }

    /// <summary>
    /// 用户保存实体
    /// </summary>
    public partial class DevUserDTO
    {
        /// <summary>
        /// 备注
        /// </summary>
        public string? REMARK { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string? ADDRESS { get; set; }
    }
    /// <summary>
    /// 用户查看信息
    /// </summary>
    public class DevUserViewInfo:DevUserDTO
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string? DepName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string? StateDic { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string? SexDic { get; set; }
       
    }

    /// <summary>
    /// 修改用户密码
    /// </summary>
    public class UpdatePwdInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int userid { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string newpwd { get; set; }
        /// <summary>
        /// 旧密码
        /// </summary>
        public string oldpwd { get; set; }
    }

         
}
