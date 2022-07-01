using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel
{

    /// <summary>
    /// 保存提交表单
    /// </summary>
    public class MenuSaveDTO
    {
        /// <summary>
        /// 菜单类型
        /// 0目录
        /// 1菜单
        /// 2按钮
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string? menuName { get; set; }
        /// <summary>
        /// 父类菜单
        /// </summary>
        public int parentMenu { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int orderNo { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string? icon { get; set; }
        /// <summary>
        /// 路由地址 
        /// path
        /// </summary>
        public string? routePath { get; set; }
        /// <summary>
        /// 组件路径
        /// </summary>
        public string? component { get; set; }
        /// <summary>
        /// 权限标识
        /// </summary>
        public string? permission { get; set; }
        /// <summary>
        /// 状态
        /// isshow
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 是否外链
        /// </summary>
        public int isExt { get; set; }
        /// <summary>
        /// 是否缓存
        /// </summary>
        public int keepalive { get; set; }
        /// <summary>
        ///是否显示
        /// </summary>
        public int show { get; set; } = 0;
        /// <summary>
        /// Id 修改时有用
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string name { get; set; }

    }
}
