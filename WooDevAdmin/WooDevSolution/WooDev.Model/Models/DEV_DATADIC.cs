using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 数据字典
    /// </summary>
    [SugarTable("dev_datadic")]
    public partial class DEV_DATADIC
    {
        /// <summary>
        /// DEV_DATADIC构造函数
        /// </summary>
        public DEV_DATADIC()
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
        /// 描    述:父类字段
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        /// 描    述:名称
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 描    述:编号
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string CODE { get; set; }

        /// <summary>
        /// 描    述:简称
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string SORT_NAME { get; set; }

        /// <summary>
        /// 描    述:是否应用
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string IS_APP { get; set; }

        /// <summary>
        /// 描    述:应用类型
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int APP_TYPE { get; set; }

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
        /// 描    述:排序
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int ORDER_NUM { get; set; }

        /// <summary>
        /// 描    述:描述
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string REMARK { get; set; }

        /// <summary>
        /// 描    述:字段1
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string FILED1 { get; set; }

        /// <summary>
        /// 描    述:字段2
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string FILED2 { get; set; }

    }
}
