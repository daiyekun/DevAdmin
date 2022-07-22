using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 流程模板
    /// </summary>
    [SugarTable("dev_flow_temp")]
    public partial class DEV_FLOW_TEMP
    {
        /// <summary>
        /// DEV_FLOW_TEMP构造函数
        /// </summary>
        public DEV_FLOW_TEMP()
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
        /// 描    述:名称
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 描    述:编号
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string CODE { get; set; }

        /// <summary>
        /// 描    述:版本
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int VERSION { get; set; }

        /// <summary>
        /// 描    述:审批对象
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int OBJ_TYPE { get; set; }

        /// <summary>
        /// 描    述:机构ID 豆号隔开
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string DEPART_IDS { get; set; }

        /// <summary>
        /// 描    述:审批事项 豆号隔开
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string FLOW_ITEMS { get; set; }

        /// <summary>
        /// 描    述:类别ID 豆号隔开
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string CATE_IDS { get; set; }

        /// <summary>
        /// 描    述:开始金额
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public decimal MIN_MONERY { get; set; }

        /// <summary>
        /// 描    述:结束金额
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public decimal MAX_MONERY { get; set; }

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
