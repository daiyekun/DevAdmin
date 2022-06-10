using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Linq.Expressions;
using WooDev.Common.Models;
using WooDev.IServices;
using WooDev.Model.Models;
using WooDev.ViewModel;
using WooDev.ViewModel.ExtendModel;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.Utiltiy;

namespace WooDev.WebApi.Controllers.Common
{

    /// <summary>
    /// 部门
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("default")]
    public class DevDepartController : ControllerBase
    {
        private IDevDepartmentService _IDevDepartmentService;
        public DevDepartController(IDevDepartmentService IDevDepartmentService)
        {
            _IDevDepartmentService = IDevDepartmentService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getList")]
        [HttpGet]
        //[AllowAnonymous]//跳过授权验证
        [Authorize]
        public IActionResult GetList([FromQuery] PageParams pageParams, [FromQuery] SerachDepartData serachParam)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new PageInfo<DEV_DEPARTMENT>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_DEPARTMENT>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);
           
            if (!string.IsNullOrEmpty(pageParams.keyword))
            {//搜索名称
                whereexp = whereexp.And(a => a.NAME.Contains(pageParams.keyword));
            }
            Expression<Func<DEV_DEPARTMENT, object>> orderbyLambda = a => a.ORDER_NUM;
            var data = _IDevDepartmentService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, true);
            var result = new ResultData<DevDepartmentList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getTreeList")]
        [HttpGet]
        //[AllowAnonymous]//跳过授权验证
        [Authorize]
        public IActionResult GetTreeList([FromQuery] PageParams pageParams, [FromQuery] SerachDepartData serachParam)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new NoPageInfo<DEV_DEPARTMENT>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_DEPARTMENT>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);

            if (!string.IsNullOrEmpty(pageParams.keyword))
            {//搜索名称
                whereexp = whereexp.And(a => a.NAME.Contains(pageParams.keyword));
            }
            Expression<Func<DEV_DEPARTMENT, object>> orderbyLambda = a => a.ORDER_NUM;
            var data = _IDevDepartmentService.GetTableTree(pageinfo, whereexp.ToExpression(), orderbyLambda, true);
            var result = new ResultListData<DeptTreeTable>
            {
                result = data,
            };
            
            return new DevResultJson(result);
        }
    }
}
