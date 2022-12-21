using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 实际资金表
    /// </summary>
    [SugarTable("dev_cont_actfince")]
    public partial class DEV_CONT_ACTFINCE:IDevEntitiy
    {
        /// <summary>
        /// DEV_CONT_ACTFINCE构造函数
        /// </summary>
        public DEV_CONT_ACTFINCE()
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
        /// 是否空值:True
        /// </summary>
        public int? CONT_ID { get; set; }

        /// <summary>
        /// 描    述:结算方式
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? SETT_TYPE { get; set; }

        /// <summary>
        /// 描    述:类别（0收款/1付款）
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int F_TYPE { get; set; }

        /// <summary>
        /// 描    述:金额
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public decimal AMOUNT { get; set; }

        /// <summary>
        /// 描    述:币种
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? CURRENCY_ID { get; set; }

        /// <summary>
        /// 描    述:汇率
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public decimal? CURRENCY_RATE { get; set; }

        /// <summary>
        /// 描    述:实际结算日期
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string ACT_DATE { get; set; }

        /// <summary>
        /// 描    述:凭证编号
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string A_CODE { get; set; }

        /// <summary>
        /// 描    述:银行
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string BANK { get; set; }

        /// <summary>
        /// 描    述:账号
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string ACCOUNT { get; set; }

        /// <summary>
        /// 描    述:备用1
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string FIELD1 { get; set; }

        /// <summary>
        /// 描    述:备用2
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string FIELD2 { get; set; }

        /// <summary>
        /// 描    述:确认人
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? CONFIRM_USERID { get; set; }

        /// <summary>
        /// 描    述:确认时间
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public DateTime? CONFIRM_DATE { get; set; }

        /// <summary>
        /// 描    述:说明
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string REMARK { get; set; }

        /// <summary>
        /// 描    述:状态
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string A_STATE { get; set; }

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

    }
}
