using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.ViewModel.Common;

namespace WooDev.ViewModel
{

    /// <summary>
    /// 合同对方搜索
    /// </summary>
    public class DevCompanySearch: DevSearData
    {  
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// 类别ID
        /// </summary>
        public int CateId { get; set; }

    }
}
