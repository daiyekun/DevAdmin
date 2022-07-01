using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooDev.WebCommon.FilterExtend
{
    /// <summary>
    /// 用来禁用ASP.NET Core MVC的模型绑定器，这样当一个Http请求到达服务器后，
    /// ASP.NET Core MVC就不会在将请求的所有上传文件数据都加载到服务器内存后，才执行Contoller的Action方法，
    /// 而是当Http请求到达服务器时，就立刻执行Contoller的Action方法。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DisableFormValueModelBindingAttribute : Attribute, IResourceFilter
    {
       
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var factories = context.ValueProviderFactories;
            factories.RemoveType<FormValueProviderFactory>();
            factories.RemoveType<FormFileValueProviderFactory>();
            factories.RemoveType<JQueryFormValueProviderFactory>();
            //var formValueProviderFactory = context.ValueProviderFactories
            // .OfType<FormValueProviderFactory>()
            // .FirstOrDefault();
            //if (formValueProviderFactory != null)
            //{
            //    context.ValueProviderFactories.Remove(formValueProviderFactory);
            //}

            //var jqueryFormValueProviderFactory = context.ValueProviderFactories
            //    .OfType<JQueryFormValueProviderFactory>()
            //    .FirstOrDefault();
            //if (jqueryFormValueProviderFactory != null)
            //{
            //    context.ValueProviderFactories.Remove(jqueryFormValueProviderFactory);
            //}


        }
    }
}
