
using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooDev.Auth;
using WooDev.Auth.Model;
using WooDev.Common.Utility;
using WooDev.ViewModel;
using WooDev.WebCommon.Utility;

namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{
    /// <summary>
    /// 授权服务
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DevAuthController : ControllerBase
    {
        private readonly ILogger<DevAuthController> _logger;
        private ICustomJWTService _iJWTService = null;
        private HttpHelperService _HttpHelperService = null;
        public DevAuthController(ILogger<DevAuthController> logger, 
            ICustomJWTService service, HttpHelperService httpHelperService)
        {
            _logger = logger;
            _iJWTService = service;
            _HttpHelperService = httpHelperService;
        }


        [Route("accredit")]
        [HttpPost]
        public JsonResult Accredit([FromBody] DevLoginInfo devLogin )
        {
            AjaxResult<LoginResult> ajaxResult = _HttpHelperService.VerifyUser(devLogin.UserName, devLogin.Password);
            if (ajaxResult.Result)
            {
                if(ajaxResult.data!=null&&ajaxResult.data.LoginUser != null) { 
                string token = this._iJWTService.GetToken(devLogin.UserName, devLogin.Password, ajaxResult.data.LoginUser);
                //ajaxResult.OtherValue = token;
                 ajaxResult.data.Token = token;
                 ajaxResult.data.LoginKey = EncryptUtility.DesEncrypt(ajaxResult.data.LoginUser.Id.ToString());
                // TokenSessionUtility.SetTokenToRedis(ajaxResult.data.LoginKey, "1");
                }
                else
                {
                    ajaxResult.Tag = ajaxResult.data != null ? ajaxResult.data.Reult : 500;
                }
            }
           
            Console.WriteLine($"Accredit Result : {JsonUtility.SerializeObject(ajaxResult)}");
            return new JsonResult(ajaxResult);
        }


    }

    public class DevLoginInfo
    {
        /// <summary>
        /// 登录名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
