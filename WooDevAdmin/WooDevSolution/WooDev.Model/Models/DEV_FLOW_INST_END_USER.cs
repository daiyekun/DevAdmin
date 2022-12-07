using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 审批结束列表
    /// </summary>
    [SugarTable("dev_flow_inst_end_user")]
    public partial class DEV_FLOW_INST_END_USER:IDevEntitiy
    {
        /// <summary>
        /// DEV_FLOW_INST_END_USER构造函数
        /// </summary>
        public DEV_FLOW_INST_END_USER()
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
        /// 描    述:节点ID字符串
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string NODE_STR { get; set; }

        /// <summary>
        /// 描    述:审批人员ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int USER_ID { get; set; }

        /// <summary>
        /// 描    述:审批对象类型
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int FLOW_TYPE { get; set; }

        /// <summary>
        /// 描    述:审批事项
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int FLOW_ITEM { get; set; }

        /// <summary>
        /// 描    述:审批对象名称
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string OBJ_NAME { get; set; }

        /// <summary>
        /// 描    述:编号
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string OBJ_CODE { get; set; }

        /// <summary>
        /// 描    述:审批金额
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public decimal OBJ_MONERY { get; set; }

        /// <summary>
        /// 描    述:描述
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string REMARK { get; set; }

        /// <summary>
        /// 描    述:意见
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string WF_OPTION { get; set; }

        /// <summary>
        /// 描    述:到达时间
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public DateTime START_TIME { get; set; }

        /// <summary>
        /// 描    述:审批时间
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public DateTime END_TIME { get; set; }

        /// <summary>
        /// 描    述:审批类型，属于组、人力资源等
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int SP_TYPE { get; set; }

        /// <summary>
        /// 描    述:类型对应ID。如果人力资源和UserID相等
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int SP_TYPE_OBJID { get; set; }

        /// <summary>
        /// 描    述:是否属于授权
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int IS_AUTH { get; set; }

        /// <summary>
        /// 描    述:授权人
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? AUTH_USERID { get; set; }

        /// <summary>
        /// 描    述:排序
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int ORDER_NUM { get; set; }

        /// <summary>
        /// 描    述:状态
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int FLOW_STATE { get; set; }

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
        /// 描    述:审批对象ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int OBJ_ID { get; set; }

    }
}
