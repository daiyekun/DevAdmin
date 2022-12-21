using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 交付描述表
    /// </summary>
    [SugarTable("dev_cont_sub_desc")]
    public partial class DEV_CONT_SUB_DESC:IDevEntitiy
    {
        /// <summary>
        /// DEV_CONT_SUB_DESC构造函数
        /// </summary>
        public DEV_CONT_SUB_DESC()
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
        /// 描    述:交付方式
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? DEL_TYPE { get; set; }

        /// <summary>
        /// 描    述:交付地点
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string DEL_LOCAL { get; set; }

        /// <summary>
        /// 描    述:交付人
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? DEL_USER { get; set; }

        /// <summary>
        /// 描    述:交付时间
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public DateTime? DEL_DATE { get; set; }

        /// <summary>
        /// 描    述:文件路径
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string FILE_PATH { get; set; }

        /// <summary>
        /// 描    述:文件名称 带后缀
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string FILE_NAME { get; set; }

        /// <summary>
        /// 描    述:文件名称
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 描    述:guid文件名称
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string GUIDFILENAME { get; set; }

        /// <summary>
        /// 描    述:字段1
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string FILED1 { get; set; }

        /// <summary>
        /// 描    述:字段2
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string FILED2 { get; set; }

        /// <summary>
        /// 描    述:标的ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int SUB_ID { get; set; }

        /// <summary>
        /// 描    述:当前交付数量
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public decimal? CURR_DEV_NUM { get; set; }

        /// <summary>
        /// 描    述:未交付数量
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public decimal? NOT_DEV_NUM { get; set; }

        /// <summary>
        /// 描    述:状态
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? D_STATE { get; set; }

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

    }
}
