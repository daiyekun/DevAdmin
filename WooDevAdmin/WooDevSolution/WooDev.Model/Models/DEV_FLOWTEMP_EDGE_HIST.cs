using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 流程模板边历史（连接线条）
    /// </summary>
    [SugarTable("dev_flowtemp_edge_hist")]
    public partial class DEV_FLOWTEMP_EDGE_HIST:IDevEntitiy
    {
        /// <summary>
        /// DEV_FLOWTEMP_EDGE_HIST构造函数
        /// </summary>
        public DEV_FLOWTEMP_EDGE_HIST()
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
        /// 描    述:模板历史ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string TEMP_HIST_ID { get; set; }

        /// <summary>
        /// 描    述:状态
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int EDGE_STATE { get; set; }

        /// <summary>
        /// 描    述:连接线字符串ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string EDGE_STRID { get; set; }

        /// <summary>
        /// 描    述:线类型
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string EDGE_TYPE { get; set; }

        /// <summary>
        /// 描    述:链接开始节点ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string SOURCENODEID { get; set; }

        /// <summary>
        /// 描    述:链接目标节点ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string TARGETNODEID { get; set; }

        /// <summary>
        /// 描    述:开始X轴
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int STARTPORT_X { get; set; }

        /// <summary>
        /// 描    述:开始Y轴
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int STARTPORT_Y { get; set; }

        /// <summary>
        /// 描    述:结束X轴
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int ENDPORT_X { get; set; }

        /// <summary>
        /// 描    述:介绍Y轴
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int ENDPORT_Y { get; set; }

        /// <summary>
        /// 描    述:线条拐角以及开始结束点XY坐标 JSON字符串
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string PONTSLIST { get; set; }

        /// <summary>
        /// 描    述:文本X轴
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? TEXT_X { get; set; }

        /// <summary>
        /// 描    述:文本Y轴
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? TEXT_Y { get; set; }

        /// <summary>
        /// 描    述:文本值
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string TEXT_VALUE { get; set; }

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
