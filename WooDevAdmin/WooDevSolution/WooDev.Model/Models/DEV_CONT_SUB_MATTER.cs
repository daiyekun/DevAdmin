using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 合同标的
    /// </summary>
    [SugarTable("dev_cont_sub_matter")]
    public partial class DEV_CONT_SUB_MATTER:IDevEntitiy
    {
        /// <summary>
        /// DEV_CONT_SUB_MATTER构造函数
        /// </summary>
        public DEV_CONT_SUB_MATTER()
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
        public string S_NAME { get; set; }

        /// <summary>
        /// 描    述:规格
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string SPEC { get; set; }

        /// <summary>
        /// 描    述:型号
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string S_TYPE { get; set; }

        /// <summary>
        /// 描    述:单位
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string UNIT { get; set; }

        /// <summary>
        /// 描    述:数量
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public decimal AMOUNT { get; set; }

        /// <summary>
        /// 描    述:单价
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public decimal? PRICE { get; set; }

        /// <summary>
        /// 描    述:说明
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string REMARK { get; set; }

        /// <summary>
        /// 描    述:单品ID
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? BCINCE_ID { get; set; }

        /// <summary>
        /// 描    述:标的类型（是否关联品类等存值） 是否为品类标的 0：普通标的，非业务类 1：品类关联标的，业务类 2：导入的Excel
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int IS_FROM { get; set; }

        /// <summary>
        /// 描    述:折扣率
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public decimal? DIS_RATE { get; set; }

        /// <summary>
        /// 描    述:小计折扣率
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public decimal? SUB_RATE { get; set; }

        /// <summary>
        /// 描    述:小计
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public decimal? SUBTOTAL { get; set; }

        /// <summary>
        /// 描    述:完成量/交付量
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public decimal? COMP_AMOUNT { get; set; }

        /// <summary>
        /// 描    述:计划交付时间
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public DateTime? PLAN_DATE { get; set; }

        /// <summary>
        /// 描    述:销售报价
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public decimal? SALE_PRICE { get; set; }

        /// <summary>
        /// 描    述:总价
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public decimal? AMOUNT_MONERY { get; set; }

        /// <summary>
        /// 描    述:实际交付时间
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public DateTime? ACT_DATE { get; set; }

        /// <summary>
        /// 描    述:状态
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int SUB_STATE { get; set; }

        /// <summary>
        /// 描    述:备用1
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string FILED1 { get; set; }

        /// <summary>
        /// 描    述:编号
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string CODE { get; set; }

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
