using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WooDev.Common.Models;
using WooDev.ViewModel.Flow.FlowInstance;
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
        /// <summary>
        /// 获取审批流程模板
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("GetTemp")]
        [HttpPost]
        public IActionResult GetTemp(AppConditionsInfo appConditions)
        {
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);
        }
    }
}
