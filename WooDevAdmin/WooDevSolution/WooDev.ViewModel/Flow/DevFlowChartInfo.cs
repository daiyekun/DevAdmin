using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;

namespace WooDev.ViewModel.Flow
{

    /// <summary>
    /// 流程图对象
    /// </summary>
    public class DevFlowChartInfo
    {
        /// <summary>
        /// 节点
        /// </summary>
        public List<LogicFlowNode> nodes;
        /// <summary>
        /// 连线
        /// </summary>
        public List<LogicFlowEdge> edges;
    }

    #region 节点信息定义
    /// <summary>
    /// 流程节点信息
    /// </summary>

    public class LogicFlowNode
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public string id;
        /// <summary>
        /// 节点类型
        /// </summary>
        public string type;
        /// <summary>
        /// x坐标
        /// </summary>
        public int x;
        /// <summary>
        /// y坐标
        /// </summary>
        public int y;
        /// <summary>
        /// 额外属性
        /// </summary>
        public Dictionary<string,object> properties;
        /// <summary>
        /// 文本
        /// </summary>
        public Text text;


    }
    public class Text
    {
        /// <summary>
        /// x坐标
        /// </summary>
        public int? x;
        public int? y;
        public string? value;
    }

    #endregion

    #region 边以及线条定义
    public class LogicFlowEdge
    {
        /// <summary>
        /// id
        /// </summary>
        public string id;
        /// <summary>
        /// 类型
        /// </summary>
        public string type;
        /// <summary>
        /// 链接源
        /// </summary>
        public string sourceNodeId;
        /// <summary>
        /// 链接目标
        /// </summary>
        public string targetNodeId;
        /// <summary>
        /// 开始点坐标
        /// </summary>
        public Point startPoint;
        /// <summary>
        /// 结束点坐标
        /// </summary>
        public Point endPoint;
        /// <summary>
        /// 额外属性
        /// </summary>
        public Dictionary<string, object> properties;
        /// <summary>
        /// 节点集合
        /// </summary>
        public List<Point> pointsList;
        /// <summary>
        /// 文本
        /// </summary>
        public Text text;


    }
    /// <summary>
    /// 坐标点
    /// </summary>
    public class Point
    {
        /// <summary>
        /// 坐标
        /// </summary>
        public int x;
        /// <summary>
        /// y坐标
        /// </summary>
        public int y;
    }
    #endregion
}
