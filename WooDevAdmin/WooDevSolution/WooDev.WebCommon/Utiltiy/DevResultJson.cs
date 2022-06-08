using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Utility;

namespace WooDev.WebCommon.Utiltiy
{
    public class DevResultJson: IActionResult
    {
        private object _Data = null;
        /// <summary>
        /// 是否处理时间
        /// </summary>
        private bool _logdate = false;
        /// <summary>
        /// 处理JSON字符串
        /// </summary>
        private Func<string, string> _func;
        /// <summary>
        /// Json Key是否需要小写
        /// </summary>
        private bool _jsonKeylower = false;
        public DevResultJson(object data)
        {
            _Data = data;
        }
        /// <summary>
        /// 处理时间类型
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="logdate">是否处理</param>
        public DevResultJson(object data, bool logdate)
        {
            _Data = data;
            _logdate = logdate;
        }
        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="data"></param>
        /// <param name="logdate"></param>
        /// <param name="func">回调函数</param>
        public DevResultJson(object data, Func<string, string> func, bool logdate = true)
        {
            _Data = data;
            _func = func;
            _logdate = logdate;
        }
        /// <summary>
        /// 构造函数3
        /// </summary>
        /// <param name="data">数据对象</param>
        /// <param name="logdate">是否格式为长时间格式类型</param>
        /// <param name="jsonKeylower">Json Key 是否小写</param>
        public DevResultJson(object data, bool logdate = false, bool keyL = false)
        {
            _Data = data;
            _logdate = logdate;
            _jsonKeylower = keyL;
        }
        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            if (_func != null)
            {
                var strjson = JsonUtility.SerializeObject(this._Data, this._logdate);
                byte[] bytes = Encoding.UTF8.GetBytes(this._func.Invoke(strjson));
                return context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Count());
            }
            else
            {
                byte[] bytes = Encoding.UTF8.GetBytes(JsonUtility.SerializeObject(this._Data, this._logdate, _jsonKeylower));
                return context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Count());

            }



        }
    }
}
