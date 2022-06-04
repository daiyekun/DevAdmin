using System;
using System.Collections.Generic;
using System.Text;

namespace WooDev.ViewModel.ExtendModel
{
    /// <summary>
    /// 字段信息
    /// </summary>
    public  class FieldInfo
    {
        /// <summary>
        /// 字段类型
        /// </summary>
        public Type FileType
        {
            get;set;
        }
        /// <summary>
        /// 字段值
        /// </summary>
        public object FileValue { get; set; }

    }
}
