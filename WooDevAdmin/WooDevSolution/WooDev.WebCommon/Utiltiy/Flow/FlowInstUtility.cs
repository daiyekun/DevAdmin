using Dm.Config;
using Org.BouncyCastle.Crypto.Tls;
using Org.BouncyCastle.Math;
using Pipelines.Sockets.Unofficial.Arenas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WooDev.Common.Utility;
using WooDev.Model.Models;
using WooDev.ViewModel;
using WooDev.ViewModel.Flow;
using WooDev.ViewModel.Flow.FlowInstance;

namespace WooDev.WebCommon.Utiltiy.Flow
{

    /// <summary>
    /// 流程审批实例帮助类
    /// </summary>
    public class FlowInstUtility
    {

        

        /// <summary>
        /// 将数据库数据转换成功流出图使用的JSON对象
        /// </summary>
        public static DevFlowChartInfo GetFlowCharData(FlowInstChartData instChartData)
        {
            //节点
            List<LogicFlowNode> nodes = new List<LogicFlowNode>();
            //连线
            List<LogicFlowEdge> edges = new List<LogicFlowEdge>();
            foreach (var node in instChartData.FlowNodes)
            {
                LogicFlowNode logicFlowNode = new LogicFlowNode();
                logicFlowNode.id = node.NODE_STRID;
                logicFlowNode.type = node.N_TYPE;
                logicFlowNode.x = node.X;
                logicFlowNode.y = node.Y;
                logicFlowNode.text = new Text()
                {
                    x = node.TEXT_X,
                    y = node.TEXT_Y,
                    value = node.TEXT_VALUE

                };
                var dicproperties = new Dictionary<string, string>();
                dicproperties.Add("statu", node.NODE_STATE.ToString());

                dicproperties.Add(nameof(node.NRULE), (node.NRULE ?? 0) == 0 ? "" : Convert.ToString(node.NRULE));
                dicproperties.Add(nameof(node.RE_TEXT), (node.RE_TEXT ?? 0) == 0 ? "" : Convert.ToString(node.RE_TEXT));
                logicFlowNode.properties = dicproperties;

                nodes.Add(logicFlowNode);

            }

            foreach (var edge in instChartData.FlowEdges)
            {
                LogicFlowEdge logicFlowEdge = new LogicFlowEdge();
                logicFlowEdge.id = edge.EDGE_STRID;
                logicFlowEdge.type = edge.EDGE_TYPE;
                logicFlowEdge.sourceNodeId = edge.SOURCENODEID;
                logicFlowEdge.targetNodeId = edge.TARGETNODEID;
                logicFlowEdge.startPoint = new Point()
                {
                    x = edge.STARTPORT_X,
                    y = edge.STARTPORT_Y,

                };
                logicFlowEdge.endPoint = new Point()
                {
                    x = edge.ENDPORT_X,
                    y = edge.ENDPORT_Y,

                };
                var dicproperties = new Dictionary<string, string>();
                dicproperties.Add("statu", edge.EDGE_STATE.ToString());
                logicFlowEdge.properties = dicproperties;
               
                logicFlowEdge.pointsList = JsonUtility.DeserializeObject<List<Point>>(edge.PONTSLIST);
                logicFlowEdge.text = new Text()
                {
                    x = edge.TEXT_X,
                    y = edge.TEXT_Y,
                    value = edge.TEXT_VALUE

                };

                edges.Add(logicFlowEdge);



            }

            var data = new DevFlowChartInfo()
            {
                nodes = nodes,
                edges = edges

            };

            return data;

        }
    }
}
