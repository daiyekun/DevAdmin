using Microsoft.AspNetCore.Mvc.Filters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WooDev.Model.Models;
using WooDev.WebCommon.Extend;
using WooDev.Common.Utility;
using WooDev.Common.Extend;
using WooDev.Common.Models;

namespace WooDev.WebCommon.FilterExtend
{
    /// <summary>
    /// 方法执行过滤器,主要用于记录操作日志
    /// </summary>
    public class LoginActionFilterAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// 名称
        /// </summary>
        private string Name { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        private string LoginName { get; set; } = "";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">方法标题</param>
        
        public LoginActionFilterAttribute(string Name)
        {
            this.Name = Name;

        }

        /// <summary>
        /// 方法执行结束
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            DEV_LOGIN_LOG OptionLog = GetOptionLogInfo(context);
            OptionLog.NAME = this.LoginName;
            
            //存储Redis队列 后台定时器自动刷新
            RedisUtility.ListRightPush(RedisKeys.LoginLog, OptionLog);

        }
        /// <summary>
        /// 方法执行中
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var reqprarms = context.ActionArguments.ToList();
            var count = reqprarms.Count();
            for (var i=0;i< count; i++)
            {
                if(reqprarms[i].Key== "username")
                {
                    this.LoginName = reqprarms[i].Value!=null? reqprarms[i].Value.ToString():"";
                    break;
                }

            }
            



        }
        /// <summary>
        /// 生成日志对象
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private DEV_LOGIN_LOG GetOptionLogInfo(ActionExecutedContext context)
        {
            int  userid = 0;
            int res = -1;
            try
            {
                var resultdata = ((WooDev.ViewModel.LoginResult)((Microsoft.AspNetCore.Mvc.JsonResult)context.Result).Value);
                userid = resultdata.UserId;
                res = resultdata.Reult;

                
            }
            catch (Exception)
            {
                userid = -1;


            }
            return new DEV_LOGIN_LOG
            {
                CREATE_TIME = DateTime.Now,
                UPDATE_TIME = DateTime.Now,
                UPDATE_USERID = userid,
                CREATE_USERID = userid,
                IP = context.HttpContext.GetUserIp(),
                LOGIN_USERID = userid,
                IS_DELETE = 0,
                LOGIN_RES = res,//登录结果
                NAME=this.Name



            };
        }
    }
}
