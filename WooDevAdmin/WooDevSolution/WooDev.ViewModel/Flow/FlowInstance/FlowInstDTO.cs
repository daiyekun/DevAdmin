using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel.Flow.FlowInstance
{
    /// <summary>
    /// 审批实例添加参数
    /// </summary>
    public class FlowInstDTO
    {
        /// <summary>
        /// 客户，供应商 合同
        /// </summary>
        public int FlowType { get; set; }
        /// <summary>
        /// 审批对象ID
        /// </summary>
        public int ObjId { get; set; }
        /// <summary>
        /// 审批实例
        /// </summary>
        public int FlowItem { get; set; }
        /// <summary>
        /// 流程模板ID
        /// </summary>
        public int TempId { get; set; }
        

    }
}
