using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel
{

    /// <summary>
    /// 合同对方列表
    /// </summary>
    public partial class DevCompanyList
    {
        /// <summary>
        /// 状态
        /// </summary>
        public string? StateDic { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string? CreateUserName { get; set; }
        /// <summary>
        /// 流程状态
        /// </summary>
        public string? WfState { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string? LeadUserName { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string? CateName { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public string? LevelName { get; set; }
        /// <summary>
        /// 信用等级
        /// </summary>
        public string? CrateName { get; set; }

    }
}
