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

namespace WooDev.WebCommon.Utiltiy.Flow
{

    /// <summary>
    /// 流程模板帮助类
    /// </summary>
    public class FlowTempUtility
    {

        /// <summary>
        /// 获取数据库流出图所需基本数据
        /// </summary>
        /// <param name="flowChartTempDTO">界面传过来的数据对象</param>
        /// <param name="userId">当前登录人</param>
        /// <returns></returns>
        public static FlowTempChartData GetFlowChartData(FlowChatData flowChartTempDTO, int userId = 0)
        {
            var flowchart = JsonUtility.DeserializeJsonToObject<DevFlowChartInfo>(flowChartTempDTO.FlowData);
            List<DEV_FLOWTEMP_NODE> listNodes = new List<DEV_FLOWTEMP_NODE>();
            List<DEV_FLOWTEMP_EDGE> listEdges = new List<DEV_FLOWTEMP_EDGE>();
            //遍历节点
            foreach (var tnode in flowchart.nodes)
            {

                DEV_FLOWTEMP_NODE node = new DEV_FLOWTEMP_NODE();
                node.CREATE_USERID = userId;
                node.CREATE_TIME = DateTime.Now;
                node.UPDATE_TIME = DateTime.Now;
                node.UPDATE_USERID = userId;
                node.NODE_STRID = tnode.id;
                node.TEMP_ID = flowChartTempDTO.TempId;
                node.N_TYPE = tnode.type;
                node.X = tnode.x;
                node.Y = tnode.y;
                if (tnode.text != null)
                {
                    node.TEXT_X = tnode.text.x ?? 0;
                    node.TEXT_Y = tnode.text.y ?? 0;
                    node.TEXT_VALUE = tnode.text.value;
                }

                node.NODE_STATE = 0;
                node.IS_DELETE = 0;
                node.ORDER_NUM = 0;
                listNodes.Add(node);

            }
            //遍历边及线条
            foreach (var tedge in flowchart.edges)
            {
                DEV_FLOWTEMP_EDGE edge = new DEV_FLOWTEMP_EDGE();
                edge.TEMP_ID = flowChartTempDTO.TempId;
                edge.IS_DELETE = 0;
                edge.CREATE_USERID = userId;
                edge.CREATE_TIME = DateTime.Now;
                edge.UPDATE_TIME = DateTime.Now;
                edge.UPDATE_USERID = userId;
                edge.EDGE_STRID = tedge.id;
                edge.EDGE_STATE = 0;
                edge.EDGE_TYPE = tedge.type;
                edge.SOURCENODEID = tedge.sourceNodeId;
                edge.TARGETNODEID = tedge.targetNodeId;
                edge.STARTPORT_X = tedge.startPoint.x;
                edge.STARTPORT_Y = tedge.startPoint.y;
                edge.ENDPORT_X = tedge.endPoint.x;
                edge.ENDPORT_Y = tedge.endPoint.y;
                if (tedge.text != null)
                {
                    edge.TEXT_X = tedge.text.x;
                    edge.TEXT_Y = tedge.text.y;
                    edge.TEXT_VALUE = tedge.text.value;

                }

                edge.PONTSLIST = JsonUtility.SerializeObject(tedge.pointsList);
                listEdges.Add(edge);


            }
            var data = new FlowTempChartData()
            {
                FlowNodes = listNodes,
                FlowEdges = listEdges

            };
            return data;
        }

        /// <summary>
        /// 将数据库数据转换成功流出图使用的JSON对象
        /// </summary>
        public static DevFlowChartInfo GetFlowCharData(FlowTempChartData tempChartData)
        {
            //节点
            List<LogicFlowNode> nodes = new List<LogicFlowNode>();
            //连线
            List<LogicFlowEdge> edges = new List<LogicFlowEdge>();
            foreach (var node in tempChartData.FlowNodes)
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
                var dicproperties = new Dictionary<string, object>();

                dicproperties.Add(nameof(node.NRULE), (node.NRULE ?? 0) == 0 ? "" : Convert.ToString(node.NRULE));
                dicproperties.Add(nameof(node.RE_TEXT), (node.RE_TEXT ?? 0) == 0 ? "" : Convert.ToString(node.RE_TEXT));
                logicFlowNode.properties = dicproperties;

                nodes.Add(logicFlowNode);

            }

            foreach (var edge in tempChartData.FlowEdges)
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
                logicFlowEdge.properties = new Dictionary<string, object>();
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
