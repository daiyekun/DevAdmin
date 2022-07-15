using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WooDev.Common.Models;
using WooDev.Common.Utility;
using WooDev.IServices;
using WooDev.ViewModel.Common;
using WooDev.ViewModel.Enums;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.Utiltiy;

namespace WooDev.WebApi.Controllers.Common
{

    /// <summary>
    /// 权限请求
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DevPermissionController : ControllerBase
    {
        private IMapper _IMapper;
        private IDevRolePermissionService _IDevRolePermissionService;
        public DevPermissionController( IMapper iMapper, IDevCompFileService iDevCompFileService
           , IDevRolePermissionService iDevRolePermissionService)
        {
            _IMapper = iMapper;
            _IDevRolePermissionService = iDevRolePermissionService;
        }

        /// <summary>
        /// 新建权限
        /// </summary>
        /// <returns></returns>
        [Route("getCreatePermission")]
        [HttpGet]
        public IActionResult GetCreatePermission([FromQuery]DevReqAddPermission reqAddPermission)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var deptId = HttpContext.User.Claims.GetTokenDeptId();
            var roleId = HttpContext.User.Claims.GetTokenRoleId();
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            var res=_IDevRolePermissionService.GetCreateCompanyPerssion(reqAddPermission.PerCode, userId, deptId, roleId);
            if (!res)
            {
                result.code = -1;
                result.message = "没有相关权限";
                
            }
            
            return new DevResultJson(result);
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <returns></returns>
        [Route("deletePermission")]
        [HttpGet]
        public IActionResult GetDeletePermission([FromQuery] DevReqDelPermission reqDelPermission)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var deptId = HttpContext.User.Claims.GetTokenDeptId();
            var roleId = HttpContext.User.Claims.GetTokenRoleId();
            var listIds = StringHelper.String2ArrayInt(reqDelPermission.Ids);
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            var res = _IDevRolePermissionService.GetCompanyDeletePermission(reqDelPermission.PerCode, userId, deptId, roleId, listIds);
            if (res.Code!=0)
            {
                result.code = -1;
                result.message = res.GetOptionMsg(res.Code);

            }

            return new DevResultJson(result);
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <returns></returns>
        [Route("updatePermission")]
        [HttpGet]
        public IActionResult GetUpdatePermission([FromQuery] DevReqUpdatePermission reqUpdatePermission)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var deptId = HttpContext.User.Claims.GetTokenDeptId();
            var roleId = HttpContext.User.Claims.GetTokenRoleId();
            
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            PermissionDicEnum res = _IDevRolePermissionService.GetCompanyUpdatePermission(reqUpdatePermission.PerCode, userId, deptId, roleId, reqUpdatePermission.Id??0);
            if (res!= PermissionDicEnum.OK)
            {
                result.code = -1;
                result.message = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)res);//EmunUtility.GetDefaultDesc(typeof(PermissionDicEnum));

            }

            return new DevResultJson(result);
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <returns></returns>
        [Route("detailPermission")]
        [HttpGet]
        public IActionResult GetDetailPermission([FromQuery] DevReqUpdatePermission reqUpdatePermission)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var deptId = HttpContext.User.Claims.GetTokenDeptId();
            var roleId = HttpContext.User.Claims.GetTokenRoleId();

            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            PermissionDicEnum res = _IDevRolePermissionService.GetCompanyUpdatePermission(reqUpdatePermission.PerCode, userId, deptId, roleId, reqUpdatePermission.Id ?? 0);
            if (res != PermissionDicEnum.OK)
            {
                result.code = -1;
                result.message = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)res);//EmunUtility.GetDefaultDesc(typeof(PermissionDicEnum));

            }

            return new DevResultJson(result);
        }






    }
}
