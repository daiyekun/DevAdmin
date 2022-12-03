using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 审批流程实例用户组
    /// </summary>
    [SugarTable("dev_flow_inst_groupuser")]
    public partial class DEV_FLOW_INST_GROUPUSER
    {
        /// <summary>
        /// DEV_FLOW_INST_GROUPUSER构造函数
        /// </summary>
        public DEV_FLOW_INST_GROUPUSER()
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
        /// 描    述:审批实例ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int INST_ID { get; set; }

        /// <summary>
        /// 描    述:用户ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int USER_ID { get; set; }

        /// <summary>
        /// 描    述:组ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int GROUP_ID { get; set; }

        /// <summary>
        /// 描    述:组名称
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string GROUP_NAME { get; set; }

    }
}
