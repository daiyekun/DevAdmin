using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 角色权限
    /// </summary>
    [SugarTable("dev_role_permission")]
    public partial class DEV_ROLE_PERMISSION:IDevEntitiy
    {
        /// <summary>
        /// DEV_ROLE_PERMISSION构造函数
        /// </summary>
        public DEV_ROLE_PERMISSION()
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
        /// 描    述:角色ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int R_ID { get; set; }

        /// <summary>
        /// 描    述:菜单ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int M_ID { get; set; }

        /// <summary>
        /// 描    述:权限标识
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int P_IDEN { get; set; }

        /// <summary>
        /// 描    述:权限编号
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string M_CODE { get; set; }

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

    }
}
