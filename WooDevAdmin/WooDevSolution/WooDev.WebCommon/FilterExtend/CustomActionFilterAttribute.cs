using Dev.WooNet.Common.Utility;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Utility;

namespace WooDev.WebCommon.FilterExtend
{
    public class CustomActionFilterAttribute: ActionFilterAttribute
    {
        private ILogger<CustomActionFilterAttribute> _logger = null;
        public CustomActionFilterAttribute(ILogger<CustomActionFilterAttribute> logger)
        {
            this._logger = logger;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //context.HttpContext.Response.Headers.Add("Cache-Control", "public,max-age=6000");
            Console.WriteLine($"This {nameof(CustomActionFilterAttribute)} OnActionExecuted{this.Order}");
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string url = context.HttpContext.Request.Path.Value;
            string argument = JsonUtility.SerializeObject(context.ActionArguments);
            this._logger.LogInformation($"{url}----->argument={argument}");
            Console.WriteLine($"This {nameof(CustomActionFilterAttribute)} OnActionExecuting{this.Order}");
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine($"This {nameof(CustomActionFilterAttribute)} OnResultExecuting{this.Order}");
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine($"This {nameof(CustomActionFilterAttribute)} OnResultExecuted{this.Order}");
        }

    }
}
