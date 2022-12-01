using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 流程模板节点
    /// </summary>
    [SugarTable("dev_flowtemp_node")]
    public partial class DEV_FLOWTEMP_NODE
    {
        /// <summary>
        /// DEV_FLOWTEMP_NODE构造函数
        /// </summary>
        public DEV_FLOWTEMP_NODE()
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
        /// 描    述:节点字符串ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string NODE_STRID { get; set; }

        /// <summary>
        /// 描    述:节点类型
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string N_TYPE { get; set; }

        /// <summary>
        /// 描    述:X轴 确定位置
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// 描    述:Y轴 确定位置
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// 描    述:文本X轴
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int TEXT_X { get; set; }

        /// <summary>
        /// 描    述:文本Y轴
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int TEXT_Y { get; set; }

        /// <summary>
        /// 描    述:文本内容
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string TEXT_VALUE { get; set; }

        /// <summary>
        /// 描    述:节点状态
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int NODE_STATE { get; set; }

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
        /// 描    述:
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? NRULE { get; set; }

        /// <summary>
        /// 描    述:
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? RE_TEXT { get; set; }

    }
}
