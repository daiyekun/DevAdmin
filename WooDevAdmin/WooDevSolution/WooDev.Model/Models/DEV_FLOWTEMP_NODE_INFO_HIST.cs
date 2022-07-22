using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 流程模板接节点信息历史
    /// </summary>
    [SugarTable("dev_flowtemp_node_info_hist")]
    public partial class DEV_FLOWTEMP_NODE_INFO_HIST
    {
        /// <summary>
        /// DEV_FLOWTEMP_NODE_INFO_HIST构造函数
        /// </summary>
        public DEV_FLOWTEMP_NODE_INFO_HIST()
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
        /// 描    述:模板历史 ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int TEMP_HIST_ID { get; set; }

        /// <summary>
        /// 描    述:节点ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int NODE_HIST_ID { get; set; }

        /// <summary>
        /// 描    述:节点字符串ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string NODE_STRID { get; set; }

        /// <summary>
        /// 描    述:是否允许修改文本 0 否默认 1是
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int RE_TEXT { get; set; }

        /// <summary>
        /// 描    述:审批规则 1全部通过 2任意通过
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int NRULE { get; set; }

        /// <summary>
        /// 描    述:审批组ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int GROUP_ID { get; set; }

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
