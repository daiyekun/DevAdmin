
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Linq.Expressions;
using Ubiety.Dns.Core.Records;
using WooDev.Auth.Model;
using WooDev.Common.Models;
using WooDev.Common.Utility;
using WooDev.IServices;
using WooDev.Model.Models;
using WooDev.ViewModel;
using WooDev.ViewModel.Common;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.FilterExtend;
using WooDev.WebCommon.Utiltiy;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WooDev.WebApi.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("default")]
    public class DevUserController : ControllerBase
    {
        private IDevUserService _IDevUserService;
        private IMapper _IMapper;
        private IDevUserOtherInfoService _IDevUserOtherInfoService;
        public DevUserController(IDevUserService iDevUserService, IMapper IMapper
            , IDevUserOtherInfoService IDevUserOtherInfoService)
        {
            _IDevUserService = iDevUserService;
            _IMapper = IMapper;
            _IDevUserOtherInfoService = IDevUserOtherInfoService;
        }


        /// <summary>
        /// 修改状态
        /// </summary>
        /// <returns></returns>
        [Route("query")]
        [HttpGet]
        [AllowAnonymous]//跳过授权验证
        [LoginActionFilter(Name:"Login")]//记录登录日志
        public IActionResult QueryUser(string username, string password)
        {

            AjaxResult<LoginResult> ajaxResult = null;
            var result = _IDevUserService.Login(username, password);

            ajaxResult = new AjaxResult<LoginResult>()
            {
                Result = true,
                data = result
            };
            return new JsonResult(result);


        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [Route("getUserInfo")]
        [HttpGet]
        //[AllowAnonymous]//跳过授权验证
        [Authorize]
        public JsonResult GetUserInfo()
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            DevResult<CurrLoginUser> devResult;
            var user = _IDevUserService.GetUserInfoById(userId);
            if (user!=null&&!user.Avatar.Contains("http"))
            {
                user.Avatar=$"http://{HttpContext.Request.Host}"+user.Avatar;
            }
            devResult = new DevResult<CurrLoginUser>
            {
                result = user

            };
           
            return new JsonResult(devResult);
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [Route("logout")]
        [HttpGet]
        [AllowAnonymous]//跳过授权验证
        //[Authorize]
        public JsonResult Logout()
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            //目前没有退出业务逻辑，未来再考虑添加
            var devResult = new DevResult
            {



            };
            return new JsonResult(devResult);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getUserList")]
        [HttpGet]
        [Authorize]
        public IActionResult GetList([FromQuery] PageParams pageParams, [FromQuery] DevUserSearch serachParam)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new PageInfo<DEV_USER>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_USER>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);
            if (serachParam.SelecType == 1)
            {//过滤启用状态
                whereexp = whereexp.And(a => a.USTATE == 1);
            }
            if (!string.IsNullOrEmpty(serachParam.NAME))
            {//搜索名称
                whereexp = whereexp.And(a => a.NAME.Contains(serachParam.NAME));
            }
            if (!string.IsNullOrEmpty(serachParam.LOGIN_NAME))
            {//搜索名称
                whereexp = whereexp.And(a => a.LOGIN_NAME.Contains(serachParam.LOGIN_NAME));
            }
            if (serachParam.deptId > 0)
            {//部门ID
                whereexp = whereexp.And(a => a.DEPART_ID == serachParam.deptId);
            }
            Expression<Func<DEV_USER, object>> orderbyLambda = a => a.ID;
            var data = _IDevUserService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevUserList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 判断账号是否存在
        /// </summary>
        /// <param name="account">账号名称</param>
        /// <returns></returns>
        [Route("isAccountExist")]
        [HttpPost]
        [Authorize]
        public IActionResult IsAccountExist(ExistAcccod existAcccod)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var whereexp = Expressionable.Create<DEV_USER>();
            whereexp = whereexp.And(a => a.LOGIN_NAME == existAcccod.account&&a.ID!= existAcccod.Id);
            var resl = _IDevUserService.IsAccountExist(whereexp.ToExpression());
            var mess = resl ? "已经存在" : "ok";
            var result = new ResultData
            {
                code = resl ? 1 : 0,
                message = mess,
                //tag = resl ? 1:0
            };
            return new DevResultJson(result);


        }

        /// <summary>
        /// 新增，修改保存
        /// </summary>
        /// <param name="roleDTO">角色对象</param>
        /// <returns></returns>
        [Route("userSave")]
        [HttpPost]
        public IActionResult userSave([FromBody] DevUserDTO devUserDTO)
           // [FromBody]DevUserOtherInfoDTO devUserOtherInfoDTO)
        {
            
            var userId = HttpContext.User.Claims.GetTokenUserId();
            if (devUserDTO.ID > 0)
            {
                var otherinfo = _IDevUserOtherInfoService.GetInfoByUserId(devUserDTO.ID);
                var userinfo = _IDevUserService.InSingle(devUserDTO.ID);
                var saveinfo = _IMapper.Map<DevUserDTO, DEV_USER>(devUserDTO, userinfo);
                saveinfo.UPDATE_TIME = DateTime.Now;
                saveinfo.UPDATE_USERID = userId;
                saveinfo.USTATE = 1;//
                saveinfo.PWD = EncryptUtility.PwdToMD5(devUserDTO.PWD, saveinfo.LOGIN_NAME);
               
                _IDevUserService.Update(saveinfo);
                //修改部分其他信息
                if (otherinfo != null)
                {
                    otherinfo.REMARK = devUserDTO.REMARK;
                    otherinfo.ADDRESS = devUserDTO.ADDRESS;
                    otherinfo.UPDATE_USERID = userId;
                    otherinfo.UPDATE_TIME = DateTime.Now;
                    _IDevUserOtherInfoService.Update(otherinfo);
                }
                else
                {
                    var tempother = new DEV_USER_OTHER_INFO();
                    tempother.REMARK = devUserDTO.REMARK;
                    tempother.ADDRESS = devUserDTO.ADDRESS;
                    tempother.CREATE_USERID = userId;
                    tempother.CREATE_TIME = DateTime.Now;
                    tempother.USER_ID = devUserDTO.ID;//用户ID
                    tempother.IS_DELETE = 0;
                    _IDevUserOtherInfoService.Add(tempother);

                }

            }
            else
            {
                var info = _IMapper.Map<DEV_USER>(devUserDTO);
                info.CREATE_TIME = DateTime.Now;
                info.UPDATE_TIME = DateTime.Now;
                info.CREATE_USERID = userId;
                info.UPDATE_USERID = userId;
                info.USTATE = 1;//
                info.PWD = EncryptUtility.PwdToMD5(devUserDTO.PWD, info.LOGIN_NAME);
                var user=_IDevUserService.Add(info);

                var tempother = new DEV_USER_OTHER_INFO();
                tempother.REMARK = devUserDTO.REMARK;
                tempother.ADDRESS = devUserDTO.ADDRESS;
                tempother.CREATE_USERID = userId;
                tempother.CREATE_TIME = DateTime.Now;
                tempother.USER_ID = user.ID;//用户ID
                tempother.IS_DELETE = 0;
                
                _IDevUserOtherInfoService.Add(tempother);
            }

            _IDevUserService.SetRedisHash();
           
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);


        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Ids">选中ID</param>
        [Route("delUser")]
        [HttpGet]
        [Authorize]
        public IActionResult DelUser(string Ids)
        {
            var arrIds = StringHelper.String2ArrayInt(Ids);
            _IDevUserService.Delete(a => arrIds.Contains(a.ID));
            _IDevUserService.SetRedisHash();
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="roleDTO">角色对象</param>
        /// <returns></returns>
        [Route("setUserStatus")]
        [HttpPost]
        [Authorize]
        public IActionResult SetUserStatus(DevStatusInfo roleStatus)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            if (roleStatus.id > 0)
            {
                var deprinfo = _IDevUserService.InSingle(roleStatus.id);
                deprinfo.USTATE = roleStatus.status;
                deprinfo.UPDATE_TIME = DateTime.Now;
                deprinfo.UPDATE_USERID = userId;
                _IDevUserService.Update(deprinfo);

            }
            
            _IDevUserService.SetRedisHash();
            
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);


        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="roleDTO">角色对象</param>
        /// <returns></returns>
        [Route("userview")]
        [HttpGet]
        public IActionResult Userview(int id)
        {
            

            var data=_IDevUserService.GetViewInfoById(id);

            var result = new ResultViewData<DevUserViewInfo>
            {
                code = 0,
                message = "ok",
                result= data
            };
            return new DevResultJson(result);


        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("restpwd")]
        [HttpGet]
        public IActionResult RestPwd(string Ids)
        {
            _IDevUserService.RestPwd(Ids);
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);

        }

        /// <summary>
        /// 新增，修改保存
        /// </summary>
        /// <param name="roleDTO">角色对象</param>
        /// <returns></returns>
        [Route("userUpdate")]
        [HttpPost]
        [Authorize]
        public IActionResult UserUpdate([FromBody] DevUserDTO devUserDTO)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            if (devUserDTO.ID > 0)
            {
                var otherinfo = _IDevUserOtherInfoService.GetInfoByUserId(devUserDTO.ID);
                var userinfo = _IDevUserService.InSingle(devUserDTO.ID);
                var saveinfo = _IMapper.Map<DevUserDTO,DEV_USER>(devUserDTO,userinfo);
               
                saveinfo.UPDATE_TIME = DateTime.Now;
                saveinfo.UPDATE_USERID = userId;
                saveinfo.USTATE = 1;//
                _IDevUserService.Update(saveinfo);
                //修改部分其他信息
                if (otherinfo != null)
                {
                    otherinfo.REMARK = devUserDTO.REMARK;
                    otherinfo.ADDRESS = devUserDTO.ADDRESS;
                    otherinfo.UPDATE_USERID = userId;
                    otherinfo.UPDATE_TIME = DateTime.Now;
                    _IDevUserOtherInfoService.Update(otherinfo);
                }
                else
                {
                    var tempother = new DEV_USER_OTHER_INFO();
                    tempother.REMARK = devUserDTO.REMARK;
                    tempother.ADDRESS = devUserDTO.ADDRESS;
                    tempother.CREATE_USERID = userId;
                    tempother.CREATE_TIME = DateTime.Now;
                    tempother.USER_ID = devUserDTO.ID;//用户ID
                    tempother.IS_DELETE = 0;
                    _IDevUserOtherInfoService.Add(tempother);

                }

            }
            

            _IDevUserService.SetRedisHash();

            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);


        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="newpwd">新密码</param>
        /// <param name="oldpwd">旧密码</param>
        /// <returns></returns>
        [Route("updatePwd")]
        [HttpPost]
        [Authorize]
        public IActionResult UpdatePwd([FromBody] UpdatePwdInfo updatePwdInfo)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var logname = HttpContext.User.Claims.GetTokenLoginName();
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            if (updatePwdInfo.userid > 0)
            {
                var userinfo = _IDevUserService.InSingle(updatePwdInfo.userid);
                if (userinfo != null)
                {
                    var oldmd5pwd = EncryptUtility.PwdToMD5(updatePwdInfo.oldpwd, logname);
                    if (oldmd5pwd!= userinfo.PWD)
                    {
                        result.code = 1;
                        result.message = "旧密码输入不正确";
                    }
                    else
                    {
                        var newmd5pwd = EncryptUtility.PwdToMD5(updatePwdInfo.newpwd, logname);
                        userinfo.PWD = newmd5pwd;
                       _IDevUserService.Update(userinfo);
                    }

                }
                else
                {
                    result.code = 1;
                    result.message = "当前用户不存在";
                }
                
                

            }

            _IDevUserService.SetRedisHash();

           
            return new DevResultJson(result);


        }

        /// <summary>
        /// 获取权限编码
        /// --目前返回没有意义值
        /// 只为前端不报错
        /// </summary>
        /// <returns></returns>
        [Route("getPermCode")]
        [HttpGet]
        public IActionResult GetPermCode()
        {
            string[] arr = new string[] {
            "1000","3000","5000"
            };
            var result = new ResultObjData<string[]>
            {
                result = arr,
            };
            return new DevResultJson(result);
        }









    }
}
