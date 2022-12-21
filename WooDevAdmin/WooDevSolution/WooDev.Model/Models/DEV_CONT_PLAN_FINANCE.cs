using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 计划资金
    /// </summary>
    [SugarTable("dev_cont_plan_finance")]
    public partial class DEV_CONT_PLAN_FINANCE:IDevEntitiy
    {
        /// <summary>
        /// DEV_CONT_PLAN_FINANCE构造函数
        /// </summary>
        public DEV_CONT_PLAN_FINANCE()
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
        /// 描    述:合同ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int CONT_ID { get; set; }

        /// <summary>
        /// 描    述:名称
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 描    述:0:收款 1：付款
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int F_TYPE { get; set; }

        /// <summary>
        /// 描    述:币种ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int CURRENCY_ID { get; set; }

        /// <summary>
        /// 描    述:汇率
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public decimal? CURR_RATE { get; set; }

        /// <summary>
        /// 描    述:金额
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public decimal AMOUNT { get; set; }

        /// <summary>
        /// 描    述:结算方式
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? SETT_YPE { get; set; }

        /// <summary>
        /// 描    述:计划完成时间
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public DateTime? PLAN_DATE { get; set; }

        /// <summary>
        /// 描    述:状态
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int F_STATE { get; set; }

        /// <summary>
        /// 描    述:排序
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int ORDER_NUM { get; set; }

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
        /// 描    述:说明
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string REMARK { get; set; }

    }
}
