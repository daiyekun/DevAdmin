using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel
{

    /// <summary>
    /// 修改字段对象
    /// </summary>
    public class UpdateField
    {
        /// <summary>
        /// ID 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 字段
        /// </summary>
        public string? Field { get; set; }
        /// <summary>
        /// 修改值
        /// </summary>
        public Object? Value { get; set; }
    }
}
