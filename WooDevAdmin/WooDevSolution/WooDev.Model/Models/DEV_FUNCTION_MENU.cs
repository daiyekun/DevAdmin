using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 功能菜单
    /// </summary>
    [SugarTable("dev_function_menu")]
    public partial class DEV_FUNCTION_MENU
    {
        /// <summary>
        /// DEV_FUNCTION_MENU构造函数
        /// </summary>
        public DEV_FUNCTION_MENU()
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
        /// 描    述:父类ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        /// 描    述:MetaID
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? META_ID { get; set; }

        /// <summary>
        /// 描    述:名称
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 描    述:组件
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string COMPONENT { get; set; }

        /// <summary>
        /// 描    述:编号
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string CODE { get; set; }

        /// <summary>
        /// 描    述:PATH
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string PATH { get; set; }

        /// <summary>
        /// 描    述:别名
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string ALIAS { get; set; }

        /// <summary>
        /// 描    述:重定向
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string REDIRECT { get; set; }

        /// <summary>
        /// 描    述:是否显示
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int IS_SHOW { get; set; }

        /// <summary>
        /// 描    述:未知
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? CASE_SENSITIVE { get; set; }

        /// <summary>
        /// 描    述:图标
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string ICO { get; set; }

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

        /// <summary>
        /// 描    述:0：目录 1：菜单 2：按钮
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? M_TYPE { get; set; }

        /// <summary>
        /// 描    述:权限标识字符串
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string PERMISSION { get; set; }

    }
}
