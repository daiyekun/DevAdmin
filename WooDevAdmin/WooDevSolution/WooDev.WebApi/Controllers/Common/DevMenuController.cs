using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Linq.Expressions;
using WooDev.Common.Models;
using WooDev.IServices;
using WooDev.Model.Models;
using WooDev.ViewModel;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.Utiltiy;

namespace WooDev.WebApi.Controllers.Common
{

    /// <summary>
    /// 菜单操作
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DevMenuController : ControllerBase
    {
        private IDevFunctionMenuService _IDevFunctionMenuService;
        public DevMenuController(IDevFunctionMenuService iDevFunctionMenuService)
        {
            _IDevFunctionMenuService = iDevFunctionMenuService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getMenuList")]
        [HttpGet]
        //[AllowAnonymous]//跳过授权验证
        [Authorize]
        public IActionResult GetMenuList()
        {
            
            var userId = HttpContext.User.Claims.GetTokenUserId();
            
            var whereexp = Expressionable.Create<DEV_FUNCTION_MENU>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0&&a.IS_SHOW==1);
            Expression<Func<DEV_FUNCTION_MENU, object>> orderbyLambda = a => a.ID;
            var data = _IDevFunctionMenuService.GetMenus(whereexp.ToExpression());
            var result = new ResultListData<MenuItem>
            {
                result = data,
            };
            return new DevResultJson(result,keyL:true);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getMenuListTable")]
        [HttpGet]
        //[AllowAnonymous]//跳过授权验证
        [Authorize]
        public IActionResult GetMenuListTable()
        {

            var userId = HttpContext.User.Claims.GetTokenUserId();

            var whereexp = Expressionable.Create<DEV_FUNCTION_MENU>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);
            Expression<Func<DEV_FUNCTION_MENU, object>> orderbyLambda = a => a.ID;
            var data = _IDevFunctionMenuService.GetList(whereexp.ToExpression());
            var result = new ResultListData<MenuList>
            {
                result = data,
            };
            return new DevResponseJson(result);
        }


        

    }
}
