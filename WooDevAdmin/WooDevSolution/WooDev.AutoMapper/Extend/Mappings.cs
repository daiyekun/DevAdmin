using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.AutoMapper.Extend
{
   public static class Mappings
    {
      

        /// <summary>
        ///映射文件
        /// </summary>
        /// <param name="service"></param>
        public static void AddDevMapperFiles(this IServiceCollection service)
        {
            var allType =
          Assembly
             .GetEntryAssembly()//获取默认程序集
             .GetReferencedAssemblies()//获取所有引用程序集
             .Select(Assembly.Load)
             .SelectMany(y => y.DefinedTypes)
             .Where(type => typeof(IProfile).GetTypeInfo().IsAssignableFrom(type.AsType()));
            service.AddAutoMapper(allType.ToArray());

            

        }
    }

    /// <summary>
    /// Automapper 映射规则配置扩展方法
    /// </summary>
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            // 从 appsettings.json 中获取包含配置规则的程序集信息
            string assemblies = "WooDev.AutoMapper";

            if (!string.IsNullOrEmpty(assemblies))
            {
                var profiles = new List<Type>();

                // 获取继承的 Profile 类型信息
                var parentType = typeof(Profile);

                foreach (var item in assemblies.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    // 获取所有继承于 Profile 的类
                    //
                    var types = Assembly.Load(item).GetTypes()
                        .Where(i => i.BaseType != null && i.BaseType.Name == parentType.Name);

                    if (types.Count() != 0 || types.Any())
                        profiles.AddRange(types);
                }

                // 添加映射规则
                if (profiles.Count() != 0 || profiles.Any())
                    services.AddAutoMapper(profiles.ToArray());
            }

            return services;
        }
    }


}
