using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Utility;

namespace WooDev.WebCommon.Utiltiy.Quartz
{
    public class DevLogsJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
             GetDevLogs();

            return Task.CompletedTask;
        }
        /// <summary>
        /// 从redis 获取日志添加到数据库。当然未来可以用其他更好的中间
        /// 考虑到用户量不是很大就用redis作为中间件
        /// </summary>
        public async Task GetDevLogs()
        {
           
                HttpClient client = new HttpClient();
            var quarturl = DevConfigurationManager.GetAppSettValue("QuartzInfo:QuartzUrl");
            string dqhturl = $"{quarturl}/Api/DevCommon/getLogs";
                try
                {
                    HttpResponseMessage response = await client.GetAsync(dqhturl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Above three lines can be replaced with new helper method below
                    // string responseBody = await client.GetStringAsync(uri);

                    Console.WriteLine(responseBody);
                }
                catch (HttpRequestException e)
                {
                Log4netHelper.Error("获取日志系统异常GetDevLogs：" + e.Message);
                }

               
            


        }
    }
}
