using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Linq.Expressions;
using WooDev.Common.Models;
using WooDev.Common.Utility;
using WooDev.IServices;
using WooDev.Model.Models;
using WooDev.Services;
using WooDev.ViewModel;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.Utiltiy;

namespace WooDev.WebApi.Controllers.Constact.CollContract
{
    /// <summary>
    /// 计划资金
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("default")]
    [Authorize]
    public class PlanFinceController : ControllerBase
    {
        private IDevContPlanFinanceService _IDevContPlanFinanceService;
        private IMapper _IMapper;
        public PlanFinceController(IDevContPlanFinanceService iDevContPlanFinanceService, IMapper iMapper)
        {
            _IDevContPlanFinanceService = iDevContPlanFinanceService;
            _IMapper = iMapper;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getPlanFinceList")]
        [HttpGet]

        public IActionResult GetPlanFinceList([FromQuery] PageParams pageParams, [FromQuery] DevComFileSearch serachParam)

        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new NoPageInfo<DEV_CONT_PLAN_FINANCE>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_CONT_PLAN_FINANCE>();

            whereexp = whereexp.And(a => a.IS_DELETE == 0);
            whereexp = whereexp.And(a => a.CONT_ID == -userId || a.CONT_ID == serachParam.CustId);
            Expression<Func<DEV_CONT_PLAN_FINANCE, object>> orderbyLambda = a => a.ID;
            var data = _IDevContPlanFinanceService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevContPlanFinanceList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="menuSaveDTO">菜单对象</param>
        /// <returns></returns>
        [Route("planFinceSave")]
        [HttpPost]
        [Authorize]
        public IActionResult PlanFinceSave([FromBody] DevContPlanFinanceDTO contPlanFinanceDTO)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();

            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            var info = _IMapper.Map<DEV_CONT_PLAN_FINANCE>(contPlanFinanceDTO);
            if (contPlanFinanceDTO.ID > 0)
            {//修改
                var userinfo = _IDevContPlanFinanceService.InSingle(contPlanFinanceDTO.ID);
                var saveinfo = _IMapper.Map<DevContPlanFinanceDTO, DEV_CONT_PLAN_FINANCE>(contPlanFinanceDTO);
                saveinfo.UPDATE_TIME = DateTime.Now;
                saveinfo.UPDATE_USERID = userId;
                saveinfo.IS_DELETE = 0;
                _IDevContPlanFinanceService.Update(saveinfo);
            }
            else
            {
                info.UPDATE_TIME = DateTime.Now;
                info.UPDATE_USERID = userId;
                info.IS_DELETE = 0;
                info.CREATE_TIME = DateTime.Now;
                info.CREATE_USERID = userId;
                info.CONT_ID = contPlanFinanceDTO.CONT_ID <= 0 ? -userId : contPlanFinanceDTO.CONT_ID;
                _IDevContPlanFinanceService.Add(info);
            }


            return new DevResponseJson(result);

        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Ids">选中ID</param>
        [Route("planFinceDel")]
        [HttpGet]
        public IActionResult PlanFinceDel(string Ids)
        {
            var arrIds = StringHelper.String2ArrayInt(Ids);
            _IDevContPlanFinanceService.Delete(a => arrIds.Contains(a.ID));
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);
        }

    }
}
