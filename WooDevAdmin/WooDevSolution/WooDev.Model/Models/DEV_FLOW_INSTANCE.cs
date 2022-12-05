using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 审批实例表
    /// </summary>
    [SugarTable("dev_flow_instance")]
    public partial class DEV_FLOW_INSTANCE
    {
        /// <summary>
        /// DEV_FLOW_INSTANCE构造函数
        /// </summary>
        public DEV_FLOW_INSTANCE()
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
        /// 描    述:审批对象名称
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 描    述:审批对象编号
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string CODE { get; set; }

        /// <summary>
        /// 描    述:审批对象ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int APP_ID { get; set; }

        /// <summary>
        /// 描    述:审批金额
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public decimal? APP_MONERY { get; set; }

        /// <summary>
        /// 描    述:提交人
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int START_USER_ID { get; set; }

        /// <summary>
        /// 描    述:当前审批节点
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? CURR_NODE_ID { get; set; }

        /// <summary>
        /// 描    述:当前审批节点名称
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string CURR_NODE_NAME { get; set; }

        /// <summary>
        /// 描    述:审批事项ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int FLOW_ITEM_ID { get; set; }

        /// <summary>
        /// 描    述:审批事项描述
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string FLOW_ITEM_DIC { get; set; }

        /// <summary>
        /// 描    述:审批结束时间
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public DateTime? FLOW_END_TIME { get; set; }

        /// <summary>
        /// 描    述:审批实例状态
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int FLOW_STATE { get; set; }

        /// <summary>
        /// 描    述:字段
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string FIELD_1 { get; set; }

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
        /// 描    述:对象类别
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int FLOW_TYPE { get; set; }

    }
}
