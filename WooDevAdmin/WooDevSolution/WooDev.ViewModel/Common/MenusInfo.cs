using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel
{

   

    /// <summary>
    /// 菜单
    /// </summary>
    public class MenuItem 
    {

        public int Id { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string? Path { get; set; }
        /// <summary>
        /// 组件名称
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 组件
        /// </summary>
        public string? Component { get; set; }

        /// <summary>
        /// 重定向
        /// </summary>
        public string? Redirect { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public RouteMenuMeta? Meta { get; set; }
        /// <summary>
        /// 子类
        /// </summary>

        public List<MenuItem> Children { get; set; }


    }
  
   

    /// <summary>
    /// 路由mate
    /// </summary>
    public class RouteMenuMeta
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Icon
        /// </summary>
        public string? Icon { get; set; }
        /// <summary>
        /// 是否关闭缓存
        /// </summary>
        public bool IgnoreKeepAlive { get; set; } = false;
        /// <summary>
        /// 是否隐藏菜单
        /// </summary>
        public bool HideMenu { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        public bool HideBreadcrumb { get; set; } = false;
        /// <summary>
        /// 是否隐藏子菜单
        /// </summary>
        public bool HideChildrenInMenu { get; set; } = false;
    }

}
