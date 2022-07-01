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

        /// <summary>
        /// 角色哈希
        /// </summary>
        public static string RoleHashKey = $"{BaseKey_Prefix}Role";
        /// <summary>
        /// 角色list key
        /// </summary>
        public static string RoleAllListKey = $"{BaseKey_Prefix}RoleAllList";
        /// <summary>
        /// 用户哈希
        /// </summary>
        public static string UserHashKey = $"{BaseKey_Prefix}User";
        /// <summary>
        /// 操作日志
        /// </summary>
        public static string OptionLog = $"{BaseKey_Prefix}Sys:OptionLog";
        /// <summary>
        /// 登录日志
        /// </summary>
        public static string LoginLog = $"{BaseKey_Prefix}Sys:LoginLog";


    }
}
