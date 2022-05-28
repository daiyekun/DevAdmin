using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.Common.Models
{

    /// <summary>
    ///当前用户基本信息
    /// </summary>
    public class RequestBaseData
    {
        /// <summary>
        /// 登录名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 当前用户Id
        /// </summary>

        public int UserId { get; set; }
        /// <summary>
        /// 所属部门Id
        /// </summary>

        public int DeptId { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string ShowName { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>

        public string DeptName { get; set; }
        /// <summary>
        /// 角色Id：1,2,3......
        /// </summary>
        public string RoleIds { get; set; }


    }
}
