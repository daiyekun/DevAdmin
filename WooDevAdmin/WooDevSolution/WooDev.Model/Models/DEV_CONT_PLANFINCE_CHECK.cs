using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 计划资金确认中间表
    /// </summary>
    [SugarTable("dev_cont_planfince_check")]
    public partial class DEV_CONT_PLANFINCE_CHECK:IDevEntitiy
    {
        /// <summary>
        /// DEV_CONT_PLANFINCE_CHECK构造函数
        /// </summary>
        public DEV_CONT_PLANFINCE_CHECK()
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
        /// 描    述:计划资金ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int PLAN_ID { get; set; }

        /// <summary>
        /// 描    述:实际资金ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int ACT_ID { get; set; }

        /// <summary>
        /// 描    述:确认人
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? CONFIRM_USERID { get; set; }

        /// <summary>
        /// 描    述:确认时间
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public DateTime CONFIRM_DATE { get; set; }

        /// <summary>
        /// 描    述:状态 0默认1已确认
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? CHK_STATE { get; set; }

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
