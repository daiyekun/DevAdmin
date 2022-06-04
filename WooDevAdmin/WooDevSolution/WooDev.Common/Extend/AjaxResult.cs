﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Common.Models
{
    /// <summary>
    /// 列表返回承载对象
    /// </summary>
    /// <typeparam name="T">泛型：表对象</typeparam>
   public class AjaxResult // <T>where T :class, new()
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code=0;
        /// <summary>
        /// 消息
        /// </summary>
        public string msg = "";
        /// <summary>
        /// 条数
        /// </summary>
        public int count=0;
        /// <summary>
        /// 结果
        /// </summary>
        public bool Result { get; set; } = true;
        /// <summary>
        /// 其他值
        /// </summary>
        public object OtherValue { get; set; }
        /// <summary>
        /// 标签值
        /// </summary>
        public int Tag { get; set; } = 0;

    }
    public class AjaxResult<T> : AjaxResult
    {
        /// <summary>
        /// 数据集合
        /// </summary>
        public T data { get;set; }
    }
    public class AjaxListResult<T> : AjaxResult
    {
        /// <summary>
        /// 数据集合
        /// </summary>
        public IList<T> data { get; set; }
    }

}
