﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;
using WooDev.ViewModel.ExtendModel;

namespace WooDev.ViewModel
{
    /// <summary>
    /// 部门列表
    /// </summary>
    public partial class DevDepartmentList 
    {
        
    }
    /// <summary>
    /// 搜索对象
    /// </summary>
    public class SerachDepartData
    {  
        /// <summary>
        /// 搜索名称
        /// </summary>
        public string depName { get; set; }
        /// <summary>
        /// 选择类型
        /// </summary>
        public int SelecType { get; set; } = 0;

    }

    /// <summary>
    /// 组织机构表
    /// </summary>
    public class DeptTreeTable : DevDepartmentList
    {
        /// <summary>
        /// 子类
        /// </summary>
        public List<DeptTreeTable>? children { get; set; }

    }




    /// <summary>
    /// 组织机构表
    /// </summary>
    public class DeptTree
    {

        public string icon { get; set; } = "home|svg";
        /// <summary>
        /// 标题
        /// </summary>
        public string? title { get; set; }
        /// <summary>
        /// key
        /// </summary>
        public string? key { get; set; }
        /// <summary>
        /// 子类
        /// </summary>
        public List<DeptTree>? children { get; set; }

    }
}
