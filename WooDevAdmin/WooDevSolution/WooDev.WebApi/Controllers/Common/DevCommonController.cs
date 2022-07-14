using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WooDev.Common.Models;
using WooDev.Common.Utility;
using WooDev.IServices;
using WooDev.Model.Models;

namespace WooDev.WebApi.Controllers.Common
{

    /// <summary>
    /// 一些公用的请求控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("default")]
    public class DevCommonController : ControllerBase
    {
        private IDevLoginLogService _IDevLoginLogService;
        private IDevOptionLogService _IDevOptionLogService;
        public DevCommonController(IDevLoginLogService IDevLoginLogService, IDevOptionLogService IDevOptionLogService)
        {
            _IDevLoginLogService = IDevLoginLogService;
            _IDevOptionLogService = IDevOptionLogService;

        }


        /// <summary>
        /// 获取登录日志，操作日志存储到数据库
        /// </summary>
        /// <returns></returns>
        [Route("getLogs")]
        [HttpGet]
        public IActionResult GetLogs()
        {
            try
            {
                var loginlogstr = RedisUtility.ListLeftPop(RedisKeys.LoginLog);
                if (!string.IsNullOrEmpty(loginlogstr))
                {
                    var loginfo = JsonUtility.DeserializeObject<DEV_LOGIN_LOG>(loginlogstr);
                    if (loginfo != null)
                    {
                        _IDevLoginLogService.Add(loginfo);
                    }

                }
               
            }
            catch (Exception ex )
            {
                Log4netHelper.Error("DevCommon->GetLogs:"+ex.Message);
                
            }

            try
            {
                var optionlogstr = RedisUtility.ListLeftPop(RedisKeys.OptionLog);
                if (!string.IsNullOrEmpty(optionlogstr))
                {
                    var oploginfo = JsonUtility.DeserializeObject<DEV_OPTION_LOG>(optionlogstr);
                    if (oploginfo != null)
                    {
                        oploginfo.NAME= oploginfo.ACTION_TITLE;
                        _IDevOptionLogService.Add(oploginfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Log4netHelper.Error("DevCommon->GetLogs:" + ex.Message);

            }
            return Ok();
        }

       
    }
}
