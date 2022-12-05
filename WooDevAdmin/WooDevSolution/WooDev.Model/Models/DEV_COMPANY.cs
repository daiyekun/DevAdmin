using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 合同对方
    /// </summary>
    [SugarTable("dev_company")]
    public partial class DEV_COMPANY
    {
        /// <summary>
        /// DEV_COMPANY构造函数
        /// </summary>
        public DEV_COMPANY()
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
        /// 描    述:名称
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 描    述:类型 0客户 1：公司上 2：其他
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int C_TYPE { get; set; }

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
        public string S_NAME { get; set; }

        /// <summary>
        /// 描    述:行业
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string TRADE { get; set; }

        /// <summary>
        /// 描    述:信用等级
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? CREATE_ID { get; set; }

        /// <summary>
        /// 描    述:地址
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string ADDRESS { get; set; }

        /// <summary>
        /// 描    述:级别
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? LEVEL_ID { get; set; }

        /// <summary>
        /// 描    述:类别
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int CATE_ID { get; set; }

        /// <summary>
        /// 描    述:邮编
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string ZIPCODE { get; set; }

        /// <summary>
        /// 描    述:网址
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string WEBSITE { get; set; }

        /// <summary>
        /// 描    述:发定代表
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string DEPUTY { get; set; }

        /// <summary>
        /// 描    述:成立日期
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public DateTime? EST_DATE { get; set; }

        /// <summary>
        /// 描    述:经营范围
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string EXP_RANGE { get; set; }

        /// <summary>
        /// 描    述:注册资本
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string REG_CAP { get; set; }

        /// <summary>
        /// 描    述:公司类型
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string COMP_TYPE { get; set; }

        /// <summary>
        /// 描    述:负责人ID
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? LEAD_USERID { get; set; }

        /// <summary>
        /// 描    述:状态
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int C_STATE { get; set; }

        /// <summary>
        /// 描    述:流程状态
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? WF_STATE { get; set; }

        /// <summary>
        /// 描    述:流程节点
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string WF_NODE { get; set; }

        /// <summary>
        /// 描    述:流程事项
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? WF_ITEM { get; set; }

        /// <summary>
        /// 描    述:备注1
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string FIELD1 { get; set; }

        /// <summary>
        /// 描    述:备注2
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string FIELD2 { get; set; }

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
        /// 描    述:
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string WF_NODE_STRID { get; set; }

    }
}
