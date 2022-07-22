using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 流程模板边（连接线条）历史
    /// </summary>
    [SugarTable("dev_flowtemp_edge_hist")]
    public partial class DEV_FLOWTEMP_EDGE_HIST
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
        /// 描    述:连接线ID DEV_FLOWTEMP_EDGE
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int EDGE_ID { get; set; }

        /// <summary>
        /// 描    述:连接线字符串ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string EDGE_STRID { get; set; }

        /// <summary>
        /// 描    述:连接线数据
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string EDGE_DATA { get; set; }

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
