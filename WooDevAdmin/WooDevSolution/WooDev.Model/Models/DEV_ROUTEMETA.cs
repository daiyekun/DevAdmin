using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 菜单路由Meta
    /// </summary>
    [SugarTable("dev_routemeta")]
    public partial class DEV_ROUTEMETA:IDevEntitiy
    {
        /// <summary>
        /// DEV_ROUTEMETA构造函数
        /// </summary>
        public DEV_ROUTEMETA()
        {

        }
        /// <summary>
        /// 描    述:ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
        public int ID { get; set; }

        /// <summary>
        /// 描    述:排序编号
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? ORDERNO { get; set; }

        /// <summary>
        /// 描    述:标题
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string TITLE { get; set; }

        /// <summary>
        /// 描    述:动态路由级别
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? DYNAMIC_LEVEL { get; set; }

        /// <summary>
        /// 描    述:动态路由路径
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string REAL_PATH { get; set; }

        /// <summary>
        /// 描    述:是否授权
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? IGNORE_AUTH { get; set; }

        /// <summary>
        /// 描    述:角色列表-可以不管
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string ROLES { get; set; }

        /// <summary>
        /// 描    述:是否是否缓存。默认否
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? IGNORE_KEEPALIVE { get; set; }

        /// <summary>
        /// 描    述:是否固定在标签上
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? AFFX { get; set; }

        /// <summary>
        /// 描    述:图标
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string ICON { get; set; }

        /// <summary>
        /// 描    述:路径
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string FRAME_SRC { get; set; }

        /// <summary>
        /// 描    述:是否已动态添加路由
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? HIDE_BREADCRUMB { get; set; }

        /// <summary>
        /// 描    述:是否隐藏子菜单
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? HIDE_CHILDRENINMENU { get; set; }

        /// <summary>
        /// 描    述:当前页面替换路径
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string TRANSITION_NAME { get; set; }

        /// <summary>
        /// 描    述:是否有参数
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? CARRY_PARAM { get; set; }

        /// <summary>
        /// 描    述:是否内部作为标记单层菜单
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? SINGLE { get; set; }

        /// <summary>
        /// 描    述:当前菜单激活
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string CURRENT_ACTIVEMENU { get; set; }

        /// <summary>
        /// 描    述:是否显示菜单
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? HIDETAB { get; set; }

        /// <summary>
        /// 描    述:是否显示菜单
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? HIDEMENU { get; set; }

        /// <summary>
        /// 描    述:是否是连接
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? ISLINK { get; set; }

        /// <summary>
        /// 描    述:是否隐藏子级别路由
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? HIDE_PATHCHILDER { get; set; }

        /// <summary>
        /// 描    述:仅针对菜单生成
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? IGNORE_ROUTE { get; set; }

        /// <summary>
        /// 描    述:是否删除
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int IS_DELETE { get; set; }

        /// <summary>
        /// 描    述:创建人
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int CREATE_USERID { get; set; }

        /// <summary>
        /// 描    述:创建时间
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public DateTime CREATE_TIME { get; set; }

        /// <summary>
        /// 描    述:更新人
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int UPDATE_USERID { get; set; }

        /// <summary>
        /// 描    述:更新时间
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public DateTime UPDATE_TIME { get; set; }

    }
}
