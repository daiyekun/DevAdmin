using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;

namespace WooDev.ViewModel
{

    /// <summary>
    /// 保存对象
    /// </summary>
    public class DevFlowTempSaveInfo
    {
        public int ID { get; set; }
        /// <summary>
        ///名称
        /// </summary>
        public string? NAME { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string? CODE { get; set; }
       /// <summary>
       /// 状态
       /// </summary>
        public int F_STATE { get; set; }
        /// <summary>
        /// 审批对象
        /// </summary>
        public int OBJ_TYPE { get; set; }
        /// <summary>
        /// 类别ID
        /// </summary>
        public List<int> CATE_IDS_LIST { get; set; }
        /// <summary>
        /// 审批事项
        /// </summary>
        public List<int> FLOW_ITEMS_LIST { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public List<int> DEPART_IDS_LIST { get; set; }
    }
    /// <summary>
    /// 保存图对象
    /// </summary>
    public class FlowChatData
    {
        /// <summary>
        /// 流程模板ID
        /// </summary>
        public int TempId{ get; set; }
        /// <summary>
        ///流程图数据
        /// </summary>
        public string? FlowData { get; set; }
    }

    /// <summary>
    /// 流程模板图数据
    /// </summary>
    public class FlowTempChartData
    {
        /// <summary>
        /// 流程节点
        /// </summary>
        public List<DEV_FLOWTEMP_NODE> FlowNodes { get; set; }
        /// <summary>
        /// 节点连线--边
        /// </summary>
        public List<DEV_FLOWTEMP_EDGE> FlowEdges { get; set; }
    }

}
