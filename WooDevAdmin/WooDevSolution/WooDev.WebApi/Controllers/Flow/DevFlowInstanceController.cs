using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WooDev.Common.Models;
using WooDev.IServices;
using WooDev.Services;
using WooDev.ViewModel.Flow.FlowInstance;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.Utiltiy;

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
    }
}
