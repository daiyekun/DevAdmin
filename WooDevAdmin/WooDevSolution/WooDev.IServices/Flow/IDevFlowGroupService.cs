using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Models;
using WooDev.Model.Models;
using WooDev.ViewModel;

namespace WooDev.IServices
{
    /// <summary>
    /// 审批流程组
    /// </summary>
    public partial interface IDevFlowGroupService
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where 条件</param>
        /// <param name="orderbyLambda">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        ResultPageData<DevFlowGroupList> GetList(PageInfo<DEV_FLOW_GROUP> pageInfo, Expression<Func<DEV_FLOW_GROUP, bool>>? whereLambda,
           Expression<Func<DEV_FLOW_GROUP, object>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<DevFlowGroupDTO> GetAll();
        /// <summary>
        /// 设置Redis
        /// </summary>
        /// <param name="datadic">角色</param>
        /// <returns></returns>
        void SetRedisHash();
        }
}
