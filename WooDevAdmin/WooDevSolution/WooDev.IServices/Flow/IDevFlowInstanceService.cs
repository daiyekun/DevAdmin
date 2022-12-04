using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;
using WooDev.ViewModel.Flow.FlowInstance;

namespace WooDev.IServices
{
    /// <summary>
    /// 审批实例
    /// </summary>
    public partial interface IDevFlowInstanceService
    {
        /// <summary>
        /// 根据条件查询模板
        /// </summary>
        /// <param name="appConditions">流程模板条件</param>
        /// <returns></returns>
        DEV_FLOW_TEMP GetTemp(AppConditionsInfo appConditions);
    }
}
