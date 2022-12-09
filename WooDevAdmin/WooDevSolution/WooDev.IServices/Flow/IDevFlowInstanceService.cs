using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Models;
using WooDev.Model.Models;
using WooDev.ViewModel;
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
        /// <summary>
        /// 创建审批实例
        /// </summary>
        /// <param name="flowInstDTO">建立实例参数</param>
        /// <param name="userId">当前登录人ID</param>
        /// <returns></returns>
        DEV_FLOW_INSTANCE CreateFlowInst(FlowInstDTO flowInstDTO, int userId);
        /// <summary>
        /// 审批历史记录列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where 条件</param>
        /// <param name="orderbyLambda">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        ResultPageData<DevFlowInstanceList> GetList(PageInfo<DEV_FLOW_INSTANCE> pageInfo, Expression<Func<DEV_FLOW_INSTANCE, bool>>? whereLambda,
           Expression<Func<DEV_FLOW_INSTANCE, object>> orderbyLambda, bool isAsc);
        ///// <summary>
        ///// 根据当前人员获取当前审批对象是否可以审批
        ///// 让其在查看页面是否能看到审批按钮
        ///// </summary>
        ///// <returns></returns>
        PersionApprovalInfo IsAppExistInfo(ApprovalActionDTO approval, int userid);
        /// <summary>
        /// 提交审批意见
        /// </summary>
        /// <param name="flowOption">审批意见相关信息</param>
        /// <param name="userId">审批人员</param>
        /// <returns></returns>
        int SubmitOption(FlowOptionDTO flowOption, int userId);
    }
}
