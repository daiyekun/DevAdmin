using log4net;
using log4net.Config;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Utility;
using WooDev.WebCommon.FilterExtend;

namespace WooDev.WebCommon.ServiceExtend
{

    /// <summary>
    /// 其他扩展
    /// </summary>
    public static class DevOtherServicesExtension
    {
        /// <summary>
        /// 注册业务服务
        /// 其他项注册
        /// </summary>
        /// <param name="services"></param>
        public static void AddDevOtherServices(this IServiceCollection services)
        {
            #region Filter
            services.AddControllers(o =>
            {
                o.Filters.Add(typeof(CustomExceptionFilterAttribute));
                o.Filters.Add(typeof(CustomActionFilterAttribute));
            });
            #endregion
            services.AddControllers().AddNewtonsoftJson();
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddEndpointsApiExplorer();


        }
        /// <summary>
        /// 添加日志
        /// </summary>
        public static void AddDevLog4Net(this IServiceCollection services)
        {
            Log4netHelper.Repository = LogManager.CreateRepository("DevLog4Repository");
            XmlConfigurator.Configure(Log4netHelper.Repository, new FileInfo(Environment.CurrentDirectory + "/Config/log4net.config"));

        }
    }
}
