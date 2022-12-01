using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel
{

    /// <summary>
    /// 审批模板列表
    /// </summary>
    public partial class DevFlowTempList
    {
        /// <summary>
        /// 审批对象描述
        /// </summary>
        public string? ObjTypeDic { get; set; }
        /// <summary>
        /// 部门
        /// </summary>

        public IList<int>? DEPART_IDS_LIST { get; set; }
        /// <summary>
        /// 类别集合
        /// </summary>
        public IList<string> CATE_IDS_LIST { get; set; }
        /// <summary>
        /// 审批事项
        /// </summary>
        public IList<string> FLOW_ITEMS_LIST { get; set; }
        /// <summary>
        /// 审批对象字符串ID
        /// vben必须string
        /// </summary>

        public string OBJ_TYPE_Str { get; set; }
    }
}
