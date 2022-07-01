using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.WebCommon.Utiltiy.Quartz
{
    /// <summary>
    /// 定时器
    /// </summary>
    public class QuartzUtility
    {
        /// <summary>
        /// Quarzt初始化
        /// </summary>
        public static void QuarztLog(string WxCron)
        {
            IScheduler scheduler = new StdSchedulerFactory().GetScheduler().Result;
            IJobDetail testJob = JobBuilder.Create<DevLogsJob>()
                     .WithIdentity("optionlog", "Logs")
                     .WithDescription("login log option log")
                     .StoreDurably()
                     .Build();

            ITrigger trigger =
                        TriggerBuilder.Create()
                             .StartAt(DateTime.Now)//什么时候开始执行
                             .WithCronSchedule(WxCron) //https://cron.qqe2.com/
                             .Build();
            //每个三秒执行一次：0/3 * * * * ?
            // 每天执行一次0 0 0 1/1 * ?
            scheduler.ScheduleJob(testJob, trigger);
            scheduler.Start();
        }
    }
}
