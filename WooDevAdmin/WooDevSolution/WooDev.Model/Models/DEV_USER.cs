using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [SugarTable("dev_user")]
    public partial class DEV_USER:IDevEntitiy
    {
        /// <summary>
        /// DEV_USER构造函数
        /// </summary>
        public DEV_USER()
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
        /// 描    述:登录名称
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string LOGIN_NAME { get; set; }

        /// <summary>
        /// 描    述:密码
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string PWD { get; set; }

        /// <summary>
        /// 描    述:身份证号
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string ID_NO { get; set; }

        /// <summary>
        /// 描    述:电话
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string TEL { get; set; }

        /// <summary>
        /// 描    述:编号
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string CODE { get; set; }

        /// <summary>
        /// 描    述:所属部门ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int DEPART_ID { get; set; }

        /// <summary>
        /// 描    述:性别
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int SEX { get; set; }

        /// <summary>
        /// 描    述:入职时间
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public DateTime? ENTRY_TIME { get; set; }

        /// <summary>
        /// 描    述:邮箱
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string EMAIL { get; set; }

        /// <summary>
        /// 描    述:状态
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int USTATE { get; set; }

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
        /// 描    述:微信账号，企业微信应用时可用
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string WX_CODE { get; set; }

        /// <summary>
        /// 描    述:角色ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int ROLE_ID { get; set; }

    }
}
