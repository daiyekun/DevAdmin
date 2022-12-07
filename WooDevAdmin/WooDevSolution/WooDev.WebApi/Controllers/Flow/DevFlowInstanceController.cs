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
            if ((serachParam.CustId??0)>0&&(serachParam.FlowType??-1)>0)
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
    }
}
