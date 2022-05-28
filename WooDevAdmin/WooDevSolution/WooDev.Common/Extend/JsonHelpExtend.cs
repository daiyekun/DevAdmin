using Dev.WooNet.Common.Utility;
using System.Collections.Generic;
using WooDev.Common.Utility;

namespace WooDev.Common.Extend
{
    public static class JsonHelpExtend
    {
        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string ToJson(this object obj)
        {
            return JsonUtility.SerializeObject(obj);

        }
        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static T JsonToObje<T>(this string jsonstr) where T : class
        {
            return JsonUtility.DeserializeJsonToObject<T>(jsonstr);

        }

        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static IList<T> JsonToList<T>(this string jsonstr) where T : class
        {
            return JsonUtility.DeserializeJsonToList<T>(jsonstr);

        }
    }
}
