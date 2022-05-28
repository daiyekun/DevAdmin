using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.WebCore.Middleware
{
    /// <summary>
    /// 处理Option预请求的中间件
    /// </summary>
    public class PreOptionRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public PreOptionRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method.ToUpper() == "OPTIONS")
            {
                context.Response.StatusCode = 200;
                return;
            }
            await _next.Invoke(context);
        }
    }

    /// <summary>
    /// 扩展中间件
    /// </summary>
    public static class PreOptionsRequestMiddlewareExtensions
    {
        public static IApplicationBuilder UsePreOptionsRequest(this IApplicationBuilder app)
        {
            return app.UseMiddleware<PreOptionRequestMiddleware>();
        }
    }

}
