using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;

namespace WooDev.ViewModel.Flow.FlowInstance
{

   

    /// <summary>
    /// 审批实例流出图
    /// </summary>
    public class FlowInstChartData
    {
        /// <summary>
        /// 流程节点
        /// </summary>
        public List<DEV_FLOW_INST_NODE> FlowNodes { get; set; }
        /// <summary>
        /// 节点连线--边
        /// </summary>
        public List<DEV_FLOW_INST_EDGE> FlowEdges { get; set; }
    }

    
}
