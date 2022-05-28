using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;


namespace WooDev.Common.Utility
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    public class DevConfigurationManager
    {
        static IConfiguration Configuration;
        static DevConfigurationManager()
        {
            var baseDir = AppContext.BaseDirectory;
            Configuration = new ConfigurationBuilder()
           .SetBasePath(baseDir)
           .Add(new JsonConfigurationSource() { Path = "appsettings.json", Optional = false, ReloadOnChange = true })
           .Build();
        }
       /// <summary>
       /// 获取配置文件某个信息
       /// </summary>
       /// <param name="key">key</param>
       /// <returns>配置字符串</returns>
        public static string GetAppSettValue(string key)
        {
            return Configuration.GetSection(key).Value;
        }
    }
    
}
