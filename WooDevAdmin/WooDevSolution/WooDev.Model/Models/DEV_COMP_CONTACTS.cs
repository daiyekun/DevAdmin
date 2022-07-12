using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 公司联系人
    /// </summary>
    [SugarTable("dev_comp_contacts")]
    public partial class DEV_COMP_CONTACTS
    {
        /// <summary>
        /// DEV_COMP_CONTACTS构造函数
        /// </summary>
        public DEV_COMP_CONTACTS()
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
        /// 描    述:公司ID 来自于Company表
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int COMP_ID { get; set; }

        /// <summary>
        /// 描    述:名称
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 描    述:职位
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string POSITION { get; set; }

        /// <summary>
        /// 描    述:所属部门
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int DEPART { get; set; }

        /// <summary>
        /// 描    述:电话-座机
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string TEL { get; set; }

        /// <summary>
        /// 描    述:手机
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string PHONE { get; set; }

        /// <summary>
        /// 描    述:QQ
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 描    述:邮箱
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string EMAIL { get; set; }

        /// <summary>
        /// 描    述:备注
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string REMARK { get; set; }

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
