using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel.Common
{
    public class BasePermission
    {
        /// <summary>
        /// 权限标识
        /// </summary>
        public string? PerCode { get; set; }
    }
    /// <summary>
    /// 新建权限
    /// </summary>
    public class DevReqAddPermission: BasePermission
    {
        
    }

    /// <summary>
    /// 新建权限
    /// </summary>
    public class DevReqDelPermission: BasePermission
    {
       
        /// <summary>
        ///要删除的IDs
        /// </summary>
        public string? Ids { get; set; }
    }

    /// <summary>
    /// 修改权限
    /// </summary>
    public class DevReqUpdatePermission: BasePermission
    {
       
        /// <summary>
        ///修改ID
        /// </summary>
        public int? Id { get; set; }
    }
}
