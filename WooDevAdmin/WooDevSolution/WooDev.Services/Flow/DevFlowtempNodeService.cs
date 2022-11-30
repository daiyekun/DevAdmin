using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;
using WooDev.ViewModel;
using WooDev.ViewModel.Flow;

namespace WooDev.Services
{

    /// <summary>
    /// 流程模板节点
    /// </summary>
    public partial class DevFlowtempNodeService
    {

        /// <summary>
        /// 根据节点ID字符& 模板ID 获取节点对象
        /// </summary>
        /// <returns></returns>
        public DEV_FLOWTEMP_NODE GetFlowNodeByNodeStrId(string strId,int TempId)
        {
            var info = DbClient.Queryable<DEV_FLOWTEMP_NODE>().Where(a => a.TEMP_ID == TempId&&a.NODE_STRID== strId).Single();

            return info;

        }

        /// <summary>
        /// 判断节点是否存在
        /// 根据节点ID字符& 模板ID 判断当前节点是否存在
        /// </summary>
        /// <returns></returns>
        public bool IsExistNode(string strId, int TempId)
        {
            var exist = DbClient.Queryable<DEV_FLOWTEMP_NODE>().Any(a => a.TEMP_ID == TempId && a.NODE_STRID == strId);

            return exist;

        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="devFlowTempNode">节点对象</param>
        /// <returns></returns>
        public int UpdateNode(DevFlowTempNodeDTO devFlowTempNode,int userId)
        {
            var info = DbClient.Queryable<DEV_FLOWTEMP_NODE>()
                .Where(a => a.TEMP_ID == devFlowTempNode.TempId && a.NODE_STRID == devFlowTempNode.NodeId).Single();
            info.NRULE = devFlowTempNode.SpRules;//审批规则
            info.TEXT_VALUE = devFlowTempNode.Name;//名称
            info.UPDATE_TIME = DateTime.Now;
            info.UPDATE_USERID = userId;
           return Update(info);
        }
    }
}
