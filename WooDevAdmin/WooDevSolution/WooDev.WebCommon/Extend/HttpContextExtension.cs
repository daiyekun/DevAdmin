using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WooDev.WebCommon.Extend
{
    /// <summary>
    /// HttpContext请求扩展
    /// </summary>
    public static class HttpContextExtension
    {
        /// <summary>
        /// 获取ID
        /// </summary>
        /// <param name="context">请求内容</param>
        /// <returns></returns>
        public static string GetUserIp(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }
    }

}
