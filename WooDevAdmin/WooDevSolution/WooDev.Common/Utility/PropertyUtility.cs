using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace WooDev.Common.Utility
{
    /// <summary>
    /// 对象属性工具类
    /// </summary>
  public  class PropertyUtility
    {
        /// <summary>
        /// 根据属性获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="propertyname"></param>
        /// <returns></returns>
        public static string GetObjectPropertyValue<T>(T t, string propertyname)
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyname);

            if (property == null) return string.Empty;

            object o = property.GetValue(t, null);

            if (o == null) return string.Empty;

            return o.ToString();
        }
    }
}
