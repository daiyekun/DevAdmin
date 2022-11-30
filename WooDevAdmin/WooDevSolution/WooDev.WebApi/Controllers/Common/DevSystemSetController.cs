using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WooDev.Common.Models;
using WooDev.IServices;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.Utiltiy;

namespace WooDev.WebApi.Controllers.Common
{
    /// <summary>
    /// 系统设置
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("default")]
    public class DevSystemSetController : Controller
    {
        private IDevRoleService _IDevRoleService;
        private IDevUserService _IDevUserService;
        private IDevFlowGroupService _IDevFlowGroupService;
        private IDevDatadicService _IDevDatadicService;

        public DevSystemSetController(IDevRoleService iDevRoleService, IDevUserService iDevUserService
            , IDevFlowGroupService iDevFlowGroupService, IDevDatadicService iDevDatadicService)
        {
            _IDevRoleService= iDevRoleService;
            _IDevUserService = iDevUserService;
            _IDevFlowGroupService = iDevFlowGroupService;
            _IDevDatadicService = iDevDatadicService;


        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [Route("setCache")]
        [HttpGet]
        //[AllowAnonymous]//跳过授权验证
        [Authorize]
        public IActionResult SetCache(int sta)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            switch (sta)
            {
                case 1://系统用户
                    _IDevUserService.SetRedisHash();
                    break;
                case 2://审批组
                    _IDevFlowGroupService.SetRedisHash();
                    break;
                case 3://系统角色
                    _IDevRoleService.SetRedisHash();
                    break;
                case 4://数据字典
                    _IDevDatadicService.SetRedisHash();
                    break;
                

            }
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);

        }


    }
}
