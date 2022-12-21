using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WooDev.Common.Extend
{
    /// <summary>
    /// 字段扩展
    /// </summary>
    public static class FieldExtensions
    {
        /// <summary>
        /// 时间到字符串
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string ConvertToString(this DateTime? field)
        {
            if (!field.HasValue)
                return "";

            return field.Value.ConvertToString();
        }
        /// <summary>
        /// 时间到字符串
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string ConvertToString(this DateTime field)
        {
            return field.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 时间到日期时间字符串
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string ConvertToDateTimeString(this DateTime field)
        {
            return field.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 到百分比
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string ConvertToPercent(this decimal? field)
        {
            if (!field.HasValue)
                return "";

            return field.Value.ConvertToPercent();
        }
        /// <summary>
        /// 到百分比
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string ConvertToPercent(this decimal field)
        {
            return string.Format("{0:0.00}%", field * 100);
        }
        /// <summary>
        /// 转到千分位
        /// </summary>
        /// <param name="srcValue"></param>
        /// <param name="NumberDecimalDigits">数值中使用的小数位数</param>
        /// <returns></returns>
        public static string ThousandsSeparator(this string srcValue, int NumberDecimalDigits = 2)
        {
            if (srcValue == null)
                return "";
            Int32 intValue;
            if (!Int32.TryParse(srcValue, out intValue))
                return "";

            return ThousandsSeparatorInner(intValue, NumberDecimalDigits);
        }
        /// <summary>
        /// 转到千分位
        /// </summary>
        /// <param name="srcValue"></param>
        /// <param name="NumberDecimalDigits">数值中使用的小数位数</param>
        /// <returns></returns>
        public static string ThousandsSeparator(this int? srcValue, int NumberDecimalDigits = 2)
        {
            if (!srcValue.HasValue)
                return "";

            return ThousandsSeparatorInner(srcValue.Value, NumberDecimalDigits);
        }
        /// <summary>
        /// 转到千分位
        /// </summary>
        /// <param name="srcValue"></param>
        /// <param name="NumberDecimalDigits">数值中使用的小数位数</param>
        /// <returns></returns>
        public static string ThousandsSeparator(this decimal? srcValue, int NumberDecimalDigits = 2)
        {
            if (!srcValue.HasValue)
                return "0.00";

            return ThousandsSeparatorInner(srcValue.Value, NumberDecimalDigits);
        }
        /// <summary>
        /// 转到千分位
        /// </summary>
        /// <param name="srcValue"></param>
        /// <param name="NumberDecimalDigits">数值中使用的小数位数</param>
        /// <returns></returns>
        public static string ThousandsSeparator(this decimal srcValue, int NumberDecimalDigits = 2)
        {
            return ThousandsSeparatorInner(srcValue, NumberDecimalDigits);
        }

        private static string ThousandsSeparatorInner(decimal value, int NumberDecimalDigits)
        {
            if (NumberDecimalDigits <= 0)
            {
                NumberDecimalDigits = 2;
            }
            if (NumberDecimalDigits > 16)
            {
                NumberDecimalDigits = 16;
            }
            var format = "{0:N" + NumberDecimalDigits + "}";
            var str = string.Format(format, value);
            if (NumberDecimalDigits > 0)
            {
              //  str = str.TrimEnd('0');

                str = str.TrimEnd('.');
            }
            return str;
        }
        /// <summary>
        /// 转到千分位
        /// </summary>
        /// <param name="srcValue"></param>
        /// <param name="NumberDecimalDigits">数值中使用的小数位数</param>
        /// <returns></returns>
        public static string ThousandsSeparator2(this decimal srcValue, int NumberDecimalDigits = 2)
        {
            return ThousandsSeparatorInner2(srcValue, NumberDecimalDigits);
        }
        /// <summary>
        /// 数据千分位
        /// </summary>
        /// <param name="value">转换值</param>
        /// <param name="NumberDecimalDigits">转换小数位</param>
        /// <returns>返回转换后的值</returns>
        private static string ThousandsSeparatorInner2(decimal value, int NumberDecimalDigits)
        {
            var format = "{0:N2}";
            if (NumberDecimalDigits <= 0)
            {
                NumberDecimalDigits = 2;
            }
            if (NumberDecimalDigits > 16)
            {
                NumberDecimalDigits = 16;
            }
            var vastr = Convert.ToString(value);
            if (vastr.IndexOf(".") > 0)
            {
                var inles = vastr.Substring(vastr.IndexOf(".") + 1, vastr.Length - (vastr.IndexOf(".") + 1)).ToString().TrimEnd('0').Length;

                format = "{0:N" + inles + "}";
            }
            else
            {
                format = "{0:N" + NumberDecimalDigits + "}";
            }
            var str = string.Format(format, value);
            return str;

        }
    }
}
