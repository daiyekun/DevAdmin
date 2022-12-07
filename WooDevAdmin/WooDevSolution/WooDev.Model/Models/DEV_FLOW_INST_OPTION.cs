using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 审批意见表
    /// </summary>
    [SugarTable("dev_flow_inst_option")]
    public partial class DEV_FLOW_INST_OPTION:IDevEntitiy
    {
        /// <summary>
        /// DEV_FLOW_INST_OPTION构造函数
        /// </summary>
        public DEV_FLOW_INST_OPTION()
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
        /// 描    述:节点ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int NODE_ID { get; set; }

        /// <summary>
        /// 描    述:节点ID字符串
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string NODE_STR_ID { get; set; }

        /// <summary>
        /// 描    述:审批实例ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int INST_ID { get; set; }

        /// <summary>
        /// 描    述:审批人ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int USER_ID { get; set; }

        /// <summary>
        /// 描    述:审批人名称
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string USER_NAME { get; set; }

        /// <summary>
        /// 描    述:审批状态 1：同意 2：打回 3：跳过
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int APP_STATE { get; set; }

        /// <summary>
        /// 描    述:审批意见
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string APP_OPTION { get; set; }

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
