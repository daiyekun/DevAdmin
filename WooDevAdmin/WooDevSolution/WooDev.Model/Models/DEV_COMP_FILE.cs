using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 附件
    /// </summary>
    [SugarTable("dev_comp_file")]
    public partial class DEV_COMP_FILE
    {
        /// <summary>
        /// DEV_COMP_FILE构造函数
        /// </summary>
        public DEV_COMP_FILE()
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
        /// 描    述:附件类型
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int CATE_ID { get; set; }

        /// <summary>
        /// 描    述:文件路径
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string PATH { get; set; }

        /// <summary>
        /// 描    述:下载次数
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int DOWN_NUM { get; set; }

        /// <summary>
        /// 描    述:文件名称
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string FILE_NAME { get; set; }

        /// <summary>
        /// 描    述:扩展名
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string EXTEND { get; set; }

        /// <summary>
        /// 描    述:IP
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string IP { get; set; }

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
