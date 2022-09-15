using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;

namespace WooDev.ViewModel
{

    /// <summary>
    /// 字典
    /// </summary>
    public partial class DevDatadicList
    {
        

    }

    /// <summary>
    /// 搜索对象
    /// </summary>
    public class SerachParam
    {
        /// <summary>
        /// 类别ID
        /// </summary>
        public int LbId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 流程对象
        /// </summary>
        public int? FlowObj { get; set; }

    }
    /// <summary>
    /// 组织机构表
    /// </summary>
    public class DataDicTree : DevDatadicList
    {
        /// <summary>
        /// 子类
        /// </summary>
        public List<DataDicTree>? children { get; set; }

    }

}
