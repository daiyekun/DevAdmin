using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel.Flow.FlowInstance
{

    /// <summary>
    /// 审批对象条件
    /// </summary>
    public class AppConditionsInfo
    {
        /// <summary>
        /// 审批对象
        /// 客户，合同....
        /// 来之枚举值 FlowObjEnums
        /// </summary>
        public int FlowObj { get; set; }
        /// <summary>
        /// 审批对象类别
        /// </summary>
        public int CateId { get; set; }
        /// <summary>
        /// 经办部门
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 审批事项
        /// </summary>
        public int FlowItem { get; set; }
        /// <summary>
        /// 审批金额
        /// </summary>

        public decimal Monery { get; set; } = -1;
    }
}
