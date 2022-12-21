using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 合同文本历史
    /// </summary>
    [SugarTable("dev_cont_conttext_hist")]
    public partial class DEV_CONT_CONTTEXT_HIST:IDevEntitiy
    {
        /// <summary>
        /// DEV_CONT_CONTTEXT_HIST构造函数
        /// </summary>
        public DEV_CONT_CONTTEXT_HIST()
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
        /// 描    述:文本路径
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string TEXT_PATH { get; set; }

        /// <summary>
        /// 描    述:原文件名称
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string FILE_NAME { get; set; }

        /// <summary>
        /// 描    述:名称
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 描    述:模板ID
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? TEMP_ID { get; set; }

        /// <summary>
        /// 描    述:文本类别
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int CATE_ID { get; set; }

        /// <summary>
        /// 描    述:说明
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string REMARK { get; set; }

        /// <summary>
        /// 描    述:下载次数
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int DOWN_TIMES { get; set; }

        /// <summary>
        /// 描    述:合同ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int CONT_ID { get; set; }

        /// <summary>
        /// 描    述:阶段
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int STAGE { get; set; }

        /// <summary>
        /// 描    述:Guid 名称
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string GUID_FILE_NAME { get; set; }

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
        /// 描    述:扩展名称
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string EXTEND { get; set; }

        /// <summary>
        /// 描    述:合同历史ID
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? CONT_HIST_ID { get; set; }

    }
}
