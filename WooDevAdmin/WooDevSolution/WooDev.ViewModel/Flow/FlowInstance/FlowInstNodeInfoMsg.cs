using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel.Flow.FlowInstance
{
    /// <summary>
    /// 审批实例节点信息及意见
    /// </summary>
    public class FlowInstNodeInfoMsg
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 操作对象ID
        /// </summary>
        public int OPT_ID { get; set; }
        /// <summary>
        /// 操作对象名称
        /// </summary>
        public string OPT_NAME { get; set; }
        /// <summary>
        /// 节点字符串ID
        /// </summary>
        public string NODE_STRID { get; set; }
        /// <summary>
        /// 审批对象类型
        /// 1、人力资源
        /// 2、审批组
        /// 3....
        /// </summary>

        public int O_TYPE { get; set; }
        
        /// <summary>
        /// 状态
        /// </summary>
        public int INFO_STATE { get; set; }
        /// <summary>
        /// 操作对象类型描述
        /// </summary>
        public string OtypeDsc { get; set; }
        /// <summary>
        /// 对象名称
        /// </summary>
        public string ObjName { get; set; }

        /// <summary>
        /// 节点消息
        /// </summary>
        public List<NodeMsg> NodeMsg { get; set; }
       
    }

    /// <summary>
    /// 节点消息
    /// </summary>
    public class NodeMsg
    {
        /// <summary>
        /// 审批意见
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 审批人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 收到时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 审批完成时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
