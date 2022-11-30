using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel.Flow
{

    /// <summary>
    /// 流程节点信息
    /// </summary>
    public class DevFlowTempNodeInfoList
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
        /// 节点ID
        /// </summary>
        public int NODE_ID { get; set; }
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
        /// 文本操作
        /// </summary>
        public int RE_TEXT { get; set;}
        /// <summary>
        /// 状态
        /// </summary>
        public int INFO_STATE { get; set; }
        /// <summary>
        /// 审批规则
        /// </summary>
        public int NRULE { get; set; }
    }

    /// <summary>
    /// 流程节点信息查询
    /// </summary>
    public class DevFlowInfoSearch
    {

        /// <summary>
        /// 流程节点ID
        /// </summary>
        public string NodeStr { get; set; } = "";

    }
    /// <summary>
    /// 节点信息修改实体
    /// </summary>

    public class DevFlowInfoDTO
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public int TEMP_ID { get; set; }
        /// <summary>
        /// 节点ID字符串
        /// </summary>
        public string NODE_STRID { get; set; }
        /// <summary>
        /// 操作类型
        /// 1：人力资源
        /// 2：审批组
        /// </summary>
        public int O_TYPE { get; set; }
        /// <summary>
        /// 审批对象ID集合
        /// </summary>

        public List<int> SpObjIds { get; set; }

    }
         
}
