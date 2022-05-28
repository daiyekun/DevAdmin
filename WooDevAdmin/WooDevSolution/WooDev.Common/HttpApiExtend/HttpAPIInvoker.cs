using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.MicroService.Framework.HttpApiExtend
{
    /// <summary>
    /// 调用HttpAPI的帮助类
    /// </summary>
    public class HttpAPIInvoker : IHttpAPIInvoker
    {
        private readonly HttpAPIInvokerOptions _httpInvokerOptions;
        public HttpAPIInvoker(IOptions<HttpAPIInvokerOptions> options)
        {
            this._httpInvokerOptions = options.Value;
        }


        /// <summary>
        /// 给个URL，然后发起Http请求，拿到结果
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string InvokeApi(string url)
        {
            Console.WriteLine(this._httpInvokerOptions.Message);
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage message = new HttpRequestMessage();
                message.Method = HttpMethod.Get;
                message.RequestUri = new Uri(url);
                var result = httpClient.SendAsync(message).Result;
                string content = result.Content.ReadAsStringAsync().Result;
                return content;
            }
        }
    }
}
