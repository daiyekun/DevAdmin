using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;
using WooDev.ViewModel.Flow;

namespace WooDev.IServices
{

    /// <summary>
    /// 流程模板节点
    /// </summary>
    public partial interface IDevFlowtempNodeService
    {
        /// <summary>
        /// 根据节点ID字符& 模板ID 获取节点对象
        /// </summary>
        /// <returns></returns>
        DEV_FLOWTEMP_NODE GetFlowNodeByNodeStrId(string strId, int TempId);
        /// <summary>
        /// 判断节点是否存在
        /// 根据节点ID字符& 模板ID 判断当前节点是否存在
        /// </summary>
        /// <returns></returns>
        bool IsExistNode(string strId, int TempId);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="devFlowTempNode">节点对象</param>
        /// <returns></returns>
        int UpdateNode(DevFlowTempNodeDTO devFlowTempNode, int userId);

    }
}
