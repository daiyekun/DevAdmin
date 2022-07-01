using System;
using System.Collections.Generic;
using System.Text;

namespace WooDev.Common.Utility
{
    /// <summary>
    /// 处理请求参数-主要用来生成审计日志信息
    /// </summary>
    public class RequestDataHpler
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="keyValuePairs">参数列表</param>
        /// <param name="paraObj">是不是对象传输</param>
        /// <returns></returns>
        public static string ExceListData(List<KeyValuePair<string, object>> keyValuePairs,bool paraObj)
        {
            StringBuilder strb = new StringBuilder();
            if (keyValuePairs!=null&& keyValuePairs.Count > 0)
            {
                if (paraObj)
                {//对象传输

                    for (var i = 0; i < keyValuePairs.Count; i++)
                    {
                        strb.Append(JsonUtility.SerializeObject(keyValuePairs[0].Value));

                    }
                }
                else {
                    strb.Append(JsonUtility.SerializeObject(keyValuePairs));
                }


            }
            return strb.ToString();
        }
    }
}
