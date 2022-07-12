using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 沟通记录
    /// </summary>
    [SugarTable("dev_comp_record")]
    public partial class DEV_COMP_RECORD
    {
        /// <summary>
        /// DEV_COMP_RECORD构造函数
        /// </summary>
        public DEV_COMP_RECORD()
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
        /// 描    述:沟通项
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string ITEM { get; set; }

        /// <summary>
        /// 描    述:沟通记录
        /// 默 认 值:
        /// 是否空值:False
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
