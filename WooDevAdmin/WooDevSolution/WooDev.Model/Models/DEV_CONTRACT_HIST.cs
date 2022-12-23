using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 合同管理历史
    /// </summary>
    [SugarTable("dev_contract_hist")]
    public partial class DEV_CONTRACT_HIST:IDevEntitiy
    {
        /// <summary>
        /// DEV_CONTRACT_HIST构造函数
        /// </summary>
        public DEV_CONTRACT_HIST()
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
        /// 描    述:名称
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string C_NAME { get; set; }

        /// <summary>
        /// 描    述:编号
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string C_CODE { get; set; }

        /// <summary>
        /// 描    述:对方编号
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string OTHER_CODE { get; set; }

        /// <summary>
        /// 描    述:合同类别ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int CATE_ID { get; set; }

        /// <summary>
        /// 描    述:自己类型 0：收款 1：付款
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int F_TYPE { get; set; }

        /// <summary>
        /// 描    述:合同金额
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public decimal ANT_MONERY { get; set; }

        /// <summary>
        /// 描    述:币种ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int CURRENCY_ID { get; set; }

        /// <summary>
        /// 描    述:汇率
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public decimal CURRENCY_RATE { get; set; }

        /// <summary>
        /// 描    述:状态
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int CONT_STATE { get; set; }

        /// <summary>
        /// 描    述:合同来源ID
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? SOURCE_ID { get; set; }

        /// <summary>
        /// 描    述:经办机构ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int DEPART_ID { get; set; }

        /// <summary>
        /// 描    述:合同对方ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int COMP_ID { get; set; }

        /// <summary>
        /// 描    述:项目ID
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? PROJECT_ID { get; set; }

        /// <summary>
        /// 描    述:签订日期
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public DateTime? SIGNING_DATE { get; set; }

        /// <summary>
        /// 描    述:生效日期
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public DateTime? EFFECTIVE_DATE { get; set; }

        /// <summary>
        /// 描    述:计划完成日期
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public DateTime? PLAN_DATE { get; set; }

        /// <summary>
        /// 描    述:实际完成日期
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public DateTime? ACT_DATE { get; set; }

        /// <summary>
        /// 描    述:负责人
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? HEAD_USER_ID { get; set; }

        /// <summary>
        /// 描    述:资金条款
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string CAPITAL_TERM { get; set; }

        /// <summary>
        /// 描    述:变更次数
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int CHANG_NUMBER { get; set; }

        /// <summary>
        /// 描    述:变更条款
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string CHANG_ITEM { get; set; }

        /// <summary>
        /// 描    述:签约主体ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int MAIN_DEPART_ID { get; set; }

        /// <summary>
        /// 描    述:是否总分包
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? IS_SUBCONT { get; set; }

        /// <summary>
        /// 描    述:总包合同ID
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? SUB_CONT_ID { get; set; }

        /// <summary>
        /// 描    述:第三方ID
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? COMPID3 { get; set; }

        /// <summary>
        /// 描    述:第四方ID
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? COMPID4 { get; set; }

        /// <summary>
        /// 描    述:是否框架合同 0：标准合同 1：框架合同
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int IS_FRAMEWORK { get; set; }

        /// <summary>
        /// 描    述:时间履行日期
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public DateTime? PERM_DATE { get; set; }

        /// <summary>
        /// 描    述:预收预付金额
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public decimal? ADVANCE_MONERY { get; set; }

        /// <summary>
        /// 描    述:预估金额
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public decimal? EST_MONERY { get; set; }

        /// <summary>
        /// 描    述:最后一个历史合同ID
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? CONT_HIST_ID { get; set; }

        /// <summary>
        /// 描    述:签订人身份证
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string CONT_SINGNO { get; set; }

        /// <summary>
        /// 描    述:合同统计表ID
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? CONT_STATIC_ID { get; set; }

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
        /// 描    述:审批事项
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? WF_ITEM { get; set; }

        /// <summary>
        /// 描    述:流程状态
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? WF_STATE { get; set; }

        /// <summary>
        /// 描    述:当前节点
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string WF_NODE { get; set; }

        /// <summary>
        /// 描    述:当前节点ID
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string WF_NODE_STRID { get; set; }

    }
}
