using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.Common.Models
{

    /// <summary>
    /// redis key
    /// </summary>
    public class RedisKeys
    {
        // 定义流量数量key
        public static string BaseKey_Prefix = "Dev:";
        /// <summary>
        /// 部门哈希
        /// </summary>
        public static string DepartHashKey = $"{BaseKey_Prefix}Depart";
        /// <summary>
        /// 部门list key
        /// </summary>
        public static string DepartAllListKey = $"{BaseKey_Prefix}DepartAllList";
        /// <summary>
        /// 字段哈希key
        /// </summary>
        public static string DataDicHashKey= $"{BaseKey_Prefix}DataDic";
        /// <summary>
        /// 字段哈希key
        /// </summary>
        public static string DataDicALLListKey = $"{BaseKey_Prefix}DataDicALLList";
        /// <summary>
        /// 币种
        /// </summary>
        public static string CurrencyHashKey = $"{BaseKey_Prefix}Currency";
    }
}
