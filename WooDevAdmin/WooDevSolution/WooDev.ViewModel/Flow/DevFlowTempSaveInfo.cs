using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
