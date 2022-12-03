using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel.ExtendModel
{

    /// <summary>
    /// 下拉多选
    /// </summary>
    public class SelectMultiple
    {
        /// <summary>
        /// 显示值
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 值 数据库是int 由于前端必须使用string
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 默认
        /// </summary>
        public string Selected { get; set; } = "";
        /// <summary>
        /// 是否禁止选择
        /// </summary>
        public string Disabled { get; set; } = "";
        /// <summary>
        /// 开始状态
        /// </summary>
        public int StartSta { get; set; }
        /// <summary>
        /// 变更状态
        /// </summary>

        public int EndSta { get; set; }

    }
}
