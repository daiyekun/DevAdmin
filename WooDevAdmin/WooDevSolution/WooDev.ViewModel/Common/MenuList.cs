﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel
{

    /// <summary>
    /// 菜单列表
    /// </summary>
    public class MenuList
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 组件名称
        /// </summary>
        public string? Name { get; set; }
        
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string? MenuName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string? Icon { get; set; }
        /// <summary>
        /// 权限标识
        /// </summary>
        public string? Permission { get; set; }
        /// <summary>
        /// 组件
        /// </summary>
        public string? Component { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public int OrderNo { get; set; } = 0;
       
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 状态是否显示
        /// 1：显示
        /// 0 不显示
        /// </summary>
        public int Status { get; set; }
        
        
        /// <summary>
        /// 父类ID
        /// </summary>

        public int Pid { get; set; }
        /// <summary>
        /// 父类菜单
        /// </summary>
        public int ParentMenu { get; set; }
        /// <summary>
        /// 路由地址
        /// </summary>
        public string? RoutePath { get; set; }
        /// <summary>
        ///是否外连接
        /// </summary>
        public int IsExt { get; set; } = 0;
        /// <summary>
        /// 是否缓存
        /// </summary>
        public int Keepalive { get; set; }
        /// <summary>
        /// 是否显示
        /// 
        /// </summary>
        public int Show { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<MenuList> Children { get; set; }




    }

    /// <summary>
    /// 查询
    /// </summary>
    public class SearMenu
    {
        /// <summary>
        /// 名称
        /// 标题
        /// </summary>
        public string? menuName{get;set;}
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; } = -1;

      
    }
}
