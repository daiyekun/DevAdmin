﻿using System;
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
        /// 值
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 默认
        /// </summary>
        public string Selected { get; set; } = "";
        /// <summary>
        /// 是否禁止选择
        /// </summary>
        public string Disabled { get; set; } = "";

    }
}
