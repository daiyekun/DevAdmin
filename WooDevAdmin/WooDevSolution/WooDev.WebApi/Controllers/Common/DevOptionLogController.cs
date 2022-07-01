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
    public class DevOptionLogController : ControllerBase
    {
        private IDevOptionLogService _IDevOptionLogService;
        public DevOptionLogController(IDevOptionLogService IDevOptionLogService)
        {
            _IDevOptionLogService = IDevOptionLogService;
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
            var pageinfo = new PageInfo<DEV_OPTION_LOG>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_OPTION_LOG>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);
            if (!string.IsNullOrEmpty(serachParam.Name))
            {//搜索名称
                whereexp = whereexp.And(a => a.NAME.Contains(serachParam.Name)
                ||a.User.NAME.Contains(serachParam.Name) || a.User.LOGIN_NAME.Contains(serachParam.Name));
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

            Expression<Func<DEV_OPTION_LOG, object>> orderbyLambda = a => a.ID;
            var data = _IDevOptionLogService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevOptionLogList>
            {
                result = data,
            };
            return new DevResultJson(result, logdate:true);
        }
    }
}
