using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Linq.Expressions;
using WooDev.Common.Models;
using WooDev.IServices;
using WooDev.Model.Models;
using WooDev.ViewModel;
using WooDev.ViewModel.Common;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.Utiltiy;

namespace WooDev.WebApi.Controllers.Common
{

    /// <summary>
    /// 登录日志
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DevLoginLogController : ControllerBase
    {
        private IDevLoginLogService _IDevLoginLogService;
        public DevLoginLogController(IDevLoginLogService IDevLoginLogService)
        {
            _IDevLoginLogService = IDevLoginLogService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getList")]
        [HttpGet]
        [Authorize]
        public IActionResult GetList([FromQuery] PageParams pageParams, [FromQuery] DevLoginLogSearch serachParam)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new PageInfo<DEV_LOGIN_LOG>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_LOGIN_LOG>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);
            if (!string.IsNullOrEmpty(serachParam.Name))
            {//搜索名称
                whereexp = whereexp.And(a => a.NAME.Contains(serachParam.Name));
            }
            if (serachParam.StartDate!=null&& serachParam.StartDate.HasValue)
            {
                whereexp = whereexp.And(a => a.CREATE_TIME>=serachParam.StartDate);
            }
            if (serachParam.EndDate != null&& serachParam.EndDate.HasValue)
            {
                var tdate = serachParam.EndDate!.Value.AddDays(1);
                whereexp = whereexp.And(a => a.CREATE_TIME < tdate);
            }

            Expression<Func<DEV_LOGIN_LOG, object>> orderbyLambda = a => a.ID;
            var data = _IDevLoginLogService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevLoginLogList>
            {
                result = data,
            };
            return new DevResultJson(result, logdate:true);
        }
    }
}
