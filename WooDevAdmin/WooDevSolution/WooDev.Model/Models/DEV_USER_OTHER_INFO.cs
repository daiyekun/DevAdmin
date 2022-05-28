using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 用户其他信息
    /// </summary>
    [SugarTable("dev_user_other_info")]
    public partial class DEV_USER_OTHER_INFO
    {
        /// <summary>
        /// DEV_USER_OTHER_INFO构造函数
        /// </summary>
        public DEV_USER_OTHER_INFO()
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
        /// 描    述:用户ID外键用户表ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int USER_ID { get; set; }

        /// <summary>
        /// 描    述:说明
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string REMARK { get; set; }

        /// <summary>
        /// 描    述:头像路径
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string HEADPATH { get; set; }

        /// <summary>
        /// 描    述:地址
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string ADDRESS { get; set; }

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
