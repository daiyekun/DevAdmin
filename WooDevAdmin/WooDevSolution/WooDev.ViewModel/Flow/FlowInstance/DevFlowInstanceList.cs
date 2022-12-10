using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel
{

    /// <summary>
    /// 审批实例列表
    /// </summary>
    public partial class DevFlowInstanceList
    {
        /// <summary>
        /// 审批对象描述
        /// 客户 供应商 合同
        /// </summary>
        public string FlowTypeDic { get; set; }
        /// <summary>
        /// 审批事项
        /// </summary>
        public string FlowItemDic { get; set; }
        /// <summary>
        /// 状态
        /// </summary>

        public string StateDic { get; set; }
        /// <summary>
        /// 审批发起人
        /// </summary>
        public string StartUserName { get; set; }


        
    }

    /// <summary>
    /// 流程节点信息查询
    /// </summary>
    public class DevFlowInstInfoSearch
    {

        /// <summary>
        /// 流程节点ID
        /// </summary>
        public string NodeStr { get; set; } = "";
        /// <summary>
        /// 审批实例ID
        /// </summary>
        public int? InstId { get; set; }

    }
}
