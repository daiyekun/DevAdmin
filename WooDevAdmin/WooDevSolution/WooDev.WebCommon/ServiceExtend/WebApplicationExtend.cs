using Dev.WooNet.AutoMapper.Extend;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
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

            #region 访问静态文件
            app.UseStaticFiles();
            string filepath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads");
            if (!System.IO.Directory.Exists(filepath))
                System.IO.Directory.CreateDirectory(filepath);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(filepath),
                RequestPath = "/Uploads"
            });
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot")),
            //});
            #endregion
            //jwt授权,放在Cors后面，不然存在跨域问题
            app.UseAuthentication();//鉴权：解析信息--就是读取token，解密token
                                    //上传大文件时使用
            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next();
            });

          


            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
