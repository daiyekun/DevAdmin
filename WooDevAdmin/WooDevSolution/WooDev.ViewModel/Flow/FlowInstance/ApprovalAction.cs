using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel.Flow.FlowInstance
{

    /// <summary>
    /// 用于判断当前用户是否对某个对象有审批权限
    /// 
    /// </summary>
    public class ApprovalActionDTO
    {
        /// <summary>
        ///审批对象ID
        /// </summary>
        public int AppObjId { get; set; }
        /// <summary>
        /// 审批对象类型
        /// 0：客户 .....等
        /// </summary>
        public int AppObjType { get; set; }
        /// <summary>
        /// 审批人员ID
        /// </summary>
        public int UserId { get; set; }

    }

    /// <summary>
    /// 审批权限信息
    /// 
    /// </summary>
    public class PersionApprovalInfo
    {
        /// <summary>
        /// 待处理审表ID
        /// DEV_FLOW_INST_WAIT_USER 表 ID
        /// </summary>
        public int WaitId { get; set; }
        /// <summary>
        /// 审批实例ID
        /// </summary>
        public int InstId { get; set; }
        /// <summary>
        /// 审批节点ID
        /// </summary>
        public string NodeId { get; set; }
        /// <summary>
        /// 文本操作权限
        /// 1;可以修改
        /// </summary>
        public int ReText { get; set; } = 0;
    }
    /// <summary>
    /// 提交审批意见对象
    /// </summary>
    public class FlowOptionDTO
    {
        /// <summary>
        /// 待处理审表ID
        /// DEV_FLOW_INST_WAIT_USER 表 ID
        /// </summary>
        public int WaitId { get; set; }
        /// <summary>
        /// 审批实例ID
        /// </summary>
        public int InstId { get; set; }
        /// <summary>
        /// 审批节点ID
        /// </summary>
        public string NodeId { get; set; }
        /// <summary>
        /// 审批状态
        /// 1：同意
        /// 2：不同意
        /// </summary>
        public int Sta { get; set; }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string Msg { get; set; }

    }

}
