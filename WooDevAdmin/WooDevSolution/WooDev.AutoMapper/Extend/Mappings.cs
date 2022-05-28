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
}
