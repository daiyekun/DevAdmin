using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Linq.Expressions;
using WooDev.Common.Models;
using WooDev.IServices;
using WooDev.Model.Models;
using WooDev.Services;
using WooDev.ViewModel.Common;
using WooDev.ViewModel;
using WooDev.ViewModel.Flow.FlowInstance;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.Utiltiy;
using WooDev.ViewModel.Flow;
using WooDev.WebCommon.Utiltiy.Flow;

namespace WooDev.WebApi.Controllers.Flow
{

    /// <summary>
    /// 审批实例表
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("default")]
    [Authorize]
    public class DevFlowInstanceController : Controller
    {
        private IDevFlowInstanceService _IDevFlowInstanceService;
        public DevFlowInstanceController(IDevFlowInstanceService iDevFlowInstanceService)
        {
            _IDevFlowInstanceService = iDevFlowInstanceService;
        }
        /// <summary>
        /// 获取审批流程模板
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("GetTemp")]
        [HttpPost]
        public IActionResult GetTemp(AppConditionsInfo appConditions)
        {
            var temp = _IDevFlowInstanceService.GetTemp(appConditions);
            var result = new ResultData
            {
                code = 0,
                message = "ok",
                result= temp
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 创建审批实例
        /// </summary>
        /// <param name="flowInstDTO">创建实例的对象</param>
        /// <returns></returns>
        [Route("createFlowInst")]
        [HttpPost]

        public IActionResult CreateFlowInst([FromBody] FlowInstDTO flowInstDTO)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var instInfo=_IDevFlowInstanceService.CreateFlowInst(flowInstDTO, userId);
            var result = new ResultData
            {
                code = 0,
                message = "ok",
                result = instInfo
            };
            return new DevResultJson(result);

        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getList")]
        [HttpGet]
        //[AllowAnonymous]//跳过授权验证
        [Authorize]
        public IActionResult GetList([FromQuery] PageParams pageParams, [FromQuery] FlowInstSearch serachParam)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new PageInfo<DEV_FLOW_INSTANCE>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_FLOW_INSTANCE>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);
            if ((serachParam.CustId??0)>0&&(serachParam.FlowType??-1)>=0)
            {
                whereexp = whereexp.And(a => a.FLOW_TYPE == serachParam.FlowType&&a.APP_ID== serachParam.CustId);
            }

                if (!string.IsNullOrEmpty(serachParam.KeyWord))
            {//搜索
                whereexp = whereexp.And(a => a.NAME.Contains(serachParam.KeyWord) || a.CODE.Contains(serachParam.KeyWord));
            }
            
            Expression<Func<DEV_FLOW_INSTANCE, object>> orderbyLambda = a => a.ID;
            var data = _IDevFlowInstanceService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevFlowInstanceList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 查询是否存在当前审批
        /// </summary>
        /// <param name="flowInstDTO">创建实例的对象</param>
        /// <returns></returns>
        [Route("isAppExistInfo")]
        [HttpPost]

        public IActionResult IsAppExistInfo([FromBody] ApprovalActionDTO  approval)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var info = _IDevFlowInstanceService.IsAppExistInfo(approval, userId);
            var result = new ResultViewData<PersionApprovalInfo>
            {
                code = 0,
                message = "ok",
                result = info
            };
            return new DevResultJson(result);

        }

        /// <summary>
        /// 提交审批意见
        /// </summary>
        /// <param name="flowInstDTO">创建实例的对象</param>
        /// <returns></returns>
        [Route("submitOption")]
        [HttpPost]

        public IActionResult SubmitOption([FromBody] FlowOptionDTO flowOption)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var info = _IDevFlowInstanceService.SubmitOption(flowOption, userId);
            var result = new ResultData
            {
                code = 0,
                message = "ok",
                result = info
            };
            return new DevResultJson(result);

        }

        /// <summary>
        /// 根据审批实例获取流出图数据
        /// </summary>
        /// <param name="instId">审批实例ID</param>
        /// <returns></returns>
        [Route("getFlowInstChartData")]
        [HttpGet]
        public IActionResult GetFlowInstChartData(int instId)
        {
            var data = _IDevFlowInstanceService.GetFlowChart(instId);
            DevFlowChartInfo flowchartdata = FlowInstUtility.GetFlowCharData(data);
            var resultdata = new ResultViewData<DevFlowChartInfo>
            {
                code = 0,
                message = "ok",
                result = flowchartdata

            };
            return new DevResultJson(resultdata);
        }
    }
}
