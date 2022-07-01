

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace WooDev.WebCommon.FilterExtend
{
    
   public class TokenSessionActionFilterAttribute: ActionFilterAttribute
    {
       
      

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
       

        /// <summary>
        /// 方法执行时
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // var actName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;
            //if (!ActionFilterData.ActionDatas.ContainsKey(actName))
            //{
            //    var loginkey = context.HttpContext.Request.Headers["loginkey"].ToString();
            //    if (RedisUtility.KeyExists($"{RedisKeys.TokenRedis}:{loginkey}"))
            //    {
            //        TokenSessionUtility.SetTokenToRedis(loginkey, "1");//再延长30分钟
            //    }
            //    else
            //    {//跳转登录页面
            //        context.Result = new JsonResult(new AjaxResult()
            //        {
            //            msg = "登录超时失效,退出重新登录",
            //            code = 1001,
            //            count = 0
            //        });

            //    }
            //} 
                

            
            
           
        }


    }
}
