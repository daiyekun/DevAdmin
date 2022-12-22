using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        /// 所有模块公用
        /// </summary>
        /// <returns></returns>
        [Route("getCreatePermission")]
        [HttpGet]
        public IActionResult GetCreatePermission([FromQuery]DevReqAddPermission reqAddPermission)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var deptId = HttpContext.User.Claims.GetTokenDeptId();
            var roleId = HttpContext.User.Claims.GetTokenRoleId();
            var rest = new ResData() { data = "ok" };
            var result = new ResultObjData<ResData>
            {
                code = 0,
                message = "ok",
                result = rest
            };
            var res=_IDevRolePermissionService.GetCreateCompanyPerssion(reqAddPermission.PerCode, userId, deptId, roleId);
            if (!res)
            {
               
                rest.data = "没有相关权限";
                rest.result = -1;
                result.result = rest;

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
            var rest = new ResData() { data = "ok" };
            var result = new ResultObjData<ResData>
            {
                code = 0,
                message = "ok",
                result= rest
            };
            switch (reqDelPermission.PerCode)
            {
                case "customerdelete"://客户
                    {
                        var res = _IDevRolePermissionService.GetCompanyDeletePermission(reqDelPermission.PerCode, userId, deptId, roleId, listIds);
                        if (res.Code != 0)
                        {
                            rest.data = res.GetOptionMsg(res.Code); ;
                            rest.result = -1;
                            result.result = rest;
                          

                        }
                    }
                    break;
                case "collcontractdelete"://收款合同
                    {
                        var res = _IDevRolePermissionService.GetContractDeletePermission(reqDelPermission.PerCode, userId, deptId, roleId, listIds);
                        if (res.Code != 0)
                        {
                            rest.data = res.GetOptionMsg(res.Code); ;
                            rest.result = -1;
                            result.result = rest;

                        }
                    }
                    break;
                default:

                    result.code = -1;
                    result.message = "权限标识错误";


                    break;

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

            var rest = new ResData() { data="ok"};
            var result = new ResultObjData<ResData>
            {
                code = 0,
                message = "ok",
                result = rest

            };
            switch (reqUpdatePermission.PerCode)
            {
                case "customerupdate"://客户
                    {
                        PermissionDicEnum res = _IDevRolePermissionService.GetCompanyUpdatePermission(reqUpdatePermission.PerCode, userId, deptId, roleId, reqUpdatePermission.Id ?? 0);
                        if (res != PermissionDicEnum.OK)
                        {
                            rest.data= EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)res);
                            rest.result = -1;
                            result.result = rest;
                            

                        }
                    }
                    break;
                case "collcontractupdate"://收款合同
                    {
                        PermissionDicEnum res = _IDevRolePermissionService.GetContractUpdatePermission(reqUpdatePermission.PerCode, userId, deptId, roleId, reqUpdatePermission.Id ?? 0);
                        if (res != PermissionDicEnum.OK)
                        {
                            rest.data = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)res);
                            rest.result = -1;
                            result.result = rest;

                        }
                    }
                    break;
                default:

                    result.code = -1;
                    result.message = "权限标识错误";
                    rest.data = "权限标识错误";
                    rest.result = -1;
                    result.result = rest;


                    break;

            }
            

            return new DevResultJson(result);
        }

        /// <summary>
        /// 查看权限
        /// </summary>
        /// <returns></returns>
        [Route("detailPermission")]
        [HttpGet]
        public IActionResult GetDetailPermission([FromQuery] DevReqUpdatePermission reqUpdatePermission)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var deptId = HttpContext.User.Claims.GetTokenDeptId();
            var roleId = HttpContext.User.Claims.GetTokenRoleId();

            var rest = new ResData() { data = "ok" };
            var result = new ResultObjData<ResData>
            {
                code = 0,
                message = "ok",
                result = rest
            };
            switch (reqUpdatePermission.PerCode)
            {
                case "customerdetail"://客户
                    {
                        PermissionDicEnum res = _IDevRolePermissionService.GetCompanyDetailPermission(reqUpdatePermission.PerCode, userId, deptId, roleId, reqUpdatePermission.Id ?? 0);
                        if (res != PermissionDicEnum.OK)
                        {
                            rest.data = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)res);
                            rest.result = -1;
                            result.result = rest;
                           

                        }
                    }
                    break;
                case "collcontractdetail"://收款合同
                    {
                        PermissionDicEnum res = _IDevRolePermissionService.GetContractDetailPermission(reqUpdatePermission.PerCode, userId, deptId, roleId, reqUpdatePermission.Id ?? 0);
                        if (res != PermissionDicEnum.OK)
                        {
                            rest.data = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)res);
                            rest.result = -1;
                            result.result = rest;

                        }
                    }
                    break;
                default:
                    rest.data = "权限标识错误";
                    rest.result = -1;
                    result.result = rest;
                    result.code = -1;
                    result.message = "权限标识错误";

                    
                    break;
            }
            

            return new DevResultJson(result);
        }






    }
}
