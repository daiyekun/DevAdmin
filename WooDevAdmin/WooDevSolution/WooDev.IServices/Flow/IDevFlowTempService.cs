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
    /// 流程模板
    /// </summary>
    public partial interface IDevFlowTempService
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
         ResultPageData<DevFlowTempList> GetList(PageInfo<DEV_FLOW_TEMP> pageInfo, Expression<Func<DEV_FLOW_TEMP, bool>>? whereLambda,
            Expression<Func<DEV_FLOW_TEMP, object>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 保存流程模板
        /// </summary>
        /// <param name="devFlowTempDTO">保存模板对象</param>
        /// <param name="userId">当前登录人员ID</param>
        /// <returns></returns>
        bool SaveFlowTemp(DevFlowTempSaveInfo devFlowTempDTO, int userId);
        /// <summary>
        /// 查看详情
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns></returns>
        DevFlowTempView ShowDetail(int Id);
        /// <summary>
        /// 根据模板ID获取流出图
        /// </summary>
        /// <param name="tempId">模板ID</param>
        /// <returns></returns>
        FlowTempChartData GetFlowChart(int tempId);
        
        }
}
