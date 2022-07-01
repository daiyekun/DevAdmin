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
    public class DevOptionLogActionFilterAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// 方法标题
        /// </summary>
        private string ActionTitle { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
       private OptionLogEnum optionType { get; set; }
        /// <summary>
        /// 操作描述
        /// </summary>
       private string Remark { get; set; }
        /// <summary>
        /// 参数是不是对象传输
        /// </summary>
        private bool RequestObj { get; set; }
     
        //public NfCustomActionFilterAttribute() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_actionTitle">方法标题</param>
        /// <param name="_optionLogType">操作类型</param>
        public DevOptionLogActionFilterAttribute(string _actionTitle, OptionLogEnum _optionLogType,string _Remark,bool _RequestObj)
        {
            ActionTitle = _actionTitle;
            optionType = _optionLogType;
            Remark = _Remark;
            RequestObj = _RequestObj;

        }

        /// <summary>
        /// 方法执行结束
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
           
        }
        /// <summary>
        /// 方法执行中
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            DEV_OPTION_LOG OptionLog = GetOptionLogInfo(context);
            //存储Redis队列 后台定时器自动刷新
            RedisUtility.ListRightPush(RedisKeys.OptionLog, OptionLog);


        }
        /// <summary>
        /// 生成日志对象
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private DEV_OPTION_LOG GetOptionLogInfo(ActionExecutingContext context)
        {
            return new DEV_OPTION_LOG
            {
                CREATE_TIME = DateTime.Now,
                UPDATE_TIME = DateTime.Now,
                UPDATE_USERID = context.HttpContext.User.Claims.GetTokenUserId(),
                CREATE_USERID = context.HttpContext.User.Claims.GetTokenUserId(),
                REQ_URL = context.HttpContext.Request.Path.ToString(),
                ACTION_TITLE = this.ActionTitle,
                REQ_PARAMETER = RequestDataHpler.ExceListData(context.ActionArguments.ToList(), RequestObj),
                ACTION_NAME = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName,
                CONTROLLER_NAME = context.Controller.ToString(),
                REQ_METHOD = (int)(context.HttpContext.Request.Method.ToString().ToUpper().Equals("POST") ? RequestMethodEnum.POST : RequestMethodEnum.GET),
                REMARK = this.Remark,
                OPTION_TYPE = (int)optionType,
                IP = context.HttpContext.GetUserIp(),
                IS_DELETE = 0

              

            };
        }
    }
}
