using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel
{

    #region 不要

    /// <summary>
    /// 路由项信息
    /// </summary>
    public class RouteItem
    {
        /// <summary>
        /// Id
        /// </summary>
        //public int Id { get; set; }
        /// <summary>
        /// 名称-少它不能tab
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string? Path { get; set; }
        /// <summary>
        /// 组件
        /// </summary>
        public string? Component { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
       // public string? Alias { get; set; }
        /// <summary>
        /// 重定向路径
        /// </summary>
         public string? Redirect { get; set; }
        /// <summary>
        /// 区分大小写
        /// </summary>
        //public bool CaseSensitive { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<LeafRouteItem> Children { get; set; }
        /// <summary>
        /// 元数据
        /// </summary>
        public PrentMeta? Meta { get; set; }
    }

    /// <summary>
    /// 子路由项信息
    /// </summary>
    public class LeafRouteItem
    {
        /// <summary>
        /// Id
        /// </summary>
       //public int Id { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string? Path { get; set; }
        /// <summary>
        /// 组件
        /// </summary>
        public string? Component { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
       // public string? Alias { get; set; }
        /// <summary>
        /// 重定向路径
        /// </summary>
       // public string? Redirect { get; set; }
        /// <summary>
        /// 区分大小写
        /// </summary>
        //public bool CaseSensitive { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
       // public List<RouteItem> Children { get; set; }
        /// <summary>
        /// 元数据
        /// </summary>
        public RouteMeta? Meta { get; set; }
    }


    /// <summary>
    /// 路由Meta（元数据）
    /// </summary>
    public class RouteMeta
    {
        /// <summary>
        /// 排序编号
        /// </summary>
        //public int OrderNo { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 动态路由级别 以下两个参数开启就关闭了tab 多标签
        /// </summary>
        //public int? DynamicLevel{get;set;}
        /// <summary>
        /// 动态路由路径
        /// </summary>
        //public string? RealPath { get; set; }
        /// <summary>
        /// 是否忽略权限
        /// </summary>
       // public bool IgnoreAuth { get; set; }
        /// <summary>
        /// 角色列表
        /// </summary>
        //public string? Roles { get; set; }
        /// <summary>
        ///是否关闭缓存
        /// </summary>
       // public bool IgnoreKeepAlive { get; set; } = false;
        /// <summary>
        /// 是否固定标签上
        /// </summary>
        //public bool Affix { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string? Icon { get; set; }
        /// <summary>
        /// 框架路径
        /// </summary>
       // public string? FrameSrc { get; set; }
        /// <summary>
        /// 当前页面转换
        /// </summary>
        //public string? TransitionName { get; set; }
        /// <summary>
        /// 是否启用动态添加路由
        /// </summary>
        public bool HideBreadcrumb { get; set; }
      
        /// <summary>
        /// 是否有参数
        /// </summary>
        //public bool CarryParam { get; set; }
        /// <summary>
        /// 内部用于标记单层菜单
        /// </summary>
        //public bool Single { get; set; }
        /// <summary>
        /// 激活当前菜单
        /// </summary>
        public string? CurrentActiveMenu { get; set; }
        /// <summary>
        /// 是否显示标签
        /// </summary>
        //public bool HideTab { get; set; } = false;
        /// <summary>
        /// 是否显示菜单
        /// </summary>
        public bool HideMenu { get; set; }
        /// <summary>
        /// 是否是a连接
        /// </summary>
        //public bool IsLink { get; set; }
        /// <summary>
        /// 仅针对菜单生成
        /// </summary>
        //public bool IgnoreRoute { get; set; }
        /// <summary>
        /// 隐藏子级路由
        /// </summary>
        //public bool HidePathForChildren { get; set; }




    }


    /// <summary>
    /// 父类mate
    /// </summary>
    public class PrentMeta
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 是否隐藏子菜单
        /// </summary>
        public bool HideChildrenInMenu { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string? Icon { get; set; }
    }


    #endregion


}
