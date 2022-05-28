using Dev.WooNet.AutoMapper.Extend;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.WebCommon.ServiceExtend
{
    public static class WebApplicationExtend
    {
        /// <summary>
        /// 把一些app Use放到此居中管理
        /// </summary>
        /// <param name="app"></param>
        public static void DevAppUse(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            app.UseStateAutoMapper();
            app.UseCors("default");
            //jwt授权,放在Cors后面，不然存在跨域问题
            app.UseAuthentication();//鉴权：解析信息--就是读取token，解密token

           

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
