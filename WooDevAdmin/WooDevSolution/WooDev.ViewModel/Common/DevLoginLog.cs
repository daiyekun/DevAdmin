using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel.Common
{
    public class DevLoginLogSearch
    {
        /// <summary>
        /// 登录名称
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 开始
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 结束
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
