

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Models;
using WooDev.Common.Utility;

namespace WooDev.WebCommon.FilterExtend
{
    /// <summary>
    /// 自定义异常过滤器特性
    /// </summary>
    public  class CustomExceptionFilterAttribute: IExceptionFilter
    {
        private ILogger<CustomExceptionFilterAttribute> _logger = null;
        public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
        {
            this._logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                
                    context.Result = new JsonResult(new AjaxResult()
                {
                    msg = "操作失败",
                    OtherValue = context.Exception.Message,
                    Result = false
                    
                });


                Log4netHelper.Error(context.Exception.Message);
                this._logger.LogError(context.Exception.Message);
            }
            context.ExceptionHandled = true;
        }
    }
}
