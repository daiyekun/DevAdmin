using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 合同统计字段
    /// </summary>
    [SugarTable("dev_cont_static")]
    public partial class DEV_CONT_STATIC:IDevEntitiy
    {
        /// <summary>
        /// DEV_CONT_STATIC构造函数
        /// </summary>
        public DEV_CONT_STATIC()
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
        /// 描    述:发票建立金额
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public decimal INVOICE_AMOUNT { get; set; }

        /// <summary>
        /// 描    述:实际建立金额
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public decimal ACTUAL_AMOUNT { get; set; }

        /// <summary>
        /// 描    述:已确认发票金额
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public decimal COMP_INV_AMOUNT { get; set; }

        /// <summary>
        /// 描    述:已确认实际金额
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public decimal COMP_ACT_AMOUNT { get; set; }

        /// <summary>
        /// 描    述:确认发票比例
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public decimal INV_RATE { get; set; }

        /// <summary>
        /// 描    述:实际自己确认比率
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public decimal ACT_RATE { get; set; }

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
