using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel.Common
{

    /// <summary>
    /// 审批实例查询
    /// </summary>
    public class FlowInstSearch: BaseSearch
    {
        /// <summary>
        /// 当前对象ID
        /// </summary>
        public int? CustId { get; set; }
        /// <summary>
        /// 审批对象类型ID
        /// 客户，，供应商。。合同..
        /// </summary>
        public int? FlowType { get; set; }
      
    }
}
