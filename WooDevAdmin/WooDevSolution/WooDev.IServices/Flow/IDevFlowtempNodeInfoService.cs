using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Models;
using WooDev.Model.Models;
using WooDev.ViewModel;
using WooDev.ViewModel.Flow;

namespace WooDev.IServices
{

    /// <summary>
    /// 节点信息
    /// </summary>
    public partial interface IDevFlowtempNodeInfoService
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
        ResultPageData<DevFlowTempNodeInfoList> GetList(PageInfo<DEV_FLOWTEMP_NODE_INFO> pageInfo, Expression<Func<DEV_FLOWTEMP_NODE_INFO, bool>>? whereLambda,
           Expression<Func<DEV_FLOWTEMP_NODE_INFO, object>> orderbyLambda, bool isAsc);
    }
}
