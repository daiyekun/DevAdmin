using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 签约主体
    /// </summary>
    [SugarTable("dev_depart_main")]
    public partial class DEV_DEPART_MAIN:IDevEntitiy
    {
        /// <summary>
        /// DEV_DEPART_MAIN构造函数
        /// </summary>
        public DEV_DEPART_MAIN()
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
        /// 描    述:部门ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int DEP_ID { get; set; }

        /// <summary>
        /// 描    述:法人代表
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string LEGAL_PERSON { get; set; }

        /// <summary>
        /// 描    述:税务登记号
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string REG_TAX_NUM { get; set; }

        /// <summary>
        /// 描    述:银行
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string BANK_NAME { get; set; }

        /// <summary>
        /// 描    述:银行账号
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string BANK_CODE { get; set; }

        /// <summary>
        /// 描    述:地址
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string ADDRESS { get; set; }

        /// <summary>
        /// 描    述:邮编
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string ZIP_CODE { get; set; }

        /// <summary>
        /// 描    述:传真
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string FAX { get; set; }

        /// <summary>
        /// 描    述:发票名称
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string INVOICE_NAME { get; set; }

        /// <summary>
        /// 描    述:电话
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string TEL { get; set; }

        /// <summary>
        /// 描    述:注册编号
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string REG_NUMBER { get; set; }

        /// <summary>
        /// 描    述:组织机构编号
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string ORG_NUMBER { get; set; }

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
