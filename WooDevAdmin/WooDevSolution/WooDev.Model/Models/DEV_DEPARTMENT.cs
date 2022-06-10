using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 部门
    /// </summary>
    [SugarTable("dev_department")]
    public partial class DEV_DEPARTMENT
    {
        /// <summary>
        /// DEV_DEPARTMENT构造函数
        /// </summary>
        public DEV_DEPARTMENT()
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
        /// 描    述:上级ID
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
        /// 是否空值:False
        /// </summary>
        public string CODE { get; set; }

        /// <summary>
        /// 描    述:简称
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string SORT_NAME { get; set; }

        /// <summary>
        /// 描    述:机构类型
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int DEP_TYPE { get; set; }

        /// <summary>
        /// 描    述:是否签约主体
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? IS_MAIN { get; set; }

        /// <summary>
        /// 描    述:部门负责人
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? DEP_HEAD { get; set; }

        /// <summary>
        /// 描    述:机构状态
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int DSTATE { get; set; }

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

    }
}
