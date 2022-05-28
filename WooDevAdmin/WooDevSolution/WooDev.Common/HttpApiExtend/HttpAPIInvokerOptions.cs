using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.MicroService.Framework.HttpApiExtend
{
    public class HttpAPIInvokerOptions
    {
        public string? Message { get; set; } = "HttpAPIInvokerOptions Message";
        public bool IsUseHttpClient { get; set; } = true;
    }
}
