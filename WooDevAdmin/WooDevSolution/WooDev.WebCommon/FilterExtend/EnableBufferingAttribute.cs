using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace WooDev.WebCommon.FilterExtend
{
    public class EnableBufferingAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.HttpContext.Request.EnableBuffering();
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

    }
}
