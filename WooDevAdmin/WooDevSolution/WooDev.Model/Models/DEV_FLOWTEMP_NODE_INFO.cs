using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 流程模板节点信息
    /// </summary>
    [SugarTable("dev_flowtemp_node_info")]
    public partial class DEV_FLOWTEMP_NODE_INFO
    {
        /// <summary>
        /// DEV_FLOWTEMP_NODE_INFO构造函数
        /// </summary>
        public DEV_FLOWTEMP_NODE_INFO()
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
        /// 描    述:模板ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int TEMP_ID { get; set; }

        /// <summary>
        /// 描    述:节点ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int NODE_ID { get; set; }

        /// <summary>
        /// 描    述:节点字符串ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string NODE_STRID { get; set; }

        /// <summary>
        /// 描    述:个人、审批组、角色、直接上级
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int O_TYPE { get; set; }

        /// <summary>
        /// 描    述:操作对象ID 比如用户ID，直接上级ID，审批组ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int OPT_ID { get; set; }

        /// <summary>
        /// 描    述:当前状态0默认，1审批中 2通过
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int INFO_STATE { get; set; }

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
        /// 描    述:
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string OPT_NAME { get; set; }

    }
}
