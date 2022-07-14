using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.WooNet.AutoMapper.Extend
{
   public static class AutoMapperHelper
    {
        private static IServiceProvider ServiceProvider;

        public static void UseStateAutoMapper(this IApplicationBuilder applicationBuilder)
        {
            ServiceProvider = applicationBuilder.ApplicationServices;
        }

        public static TDestination Map<TDestination>(object source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();

            return mapper.Map<TDestination>(source);
        }

        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();

            return mapper.Map<TSource, TDestination>(source);
        }
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();

            return mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TDestination>(this object source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();

            return mapper.Map<TDestination>(source);
        }
        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();

            return mapper.Map<TSource, TDestination>(source, destination);
        }


        
    }
}
