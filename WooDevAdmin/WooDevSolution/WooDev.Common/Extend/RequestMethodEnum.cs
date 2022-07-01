
using System;
using System.Collections.Generic;
using System.Text;
using WooDev.Common.Utility;

namespace WooDev.Common.Extend
{
    /// <summary>
    /// 请求方式
    /// </summary>
    [EnumClass(Max = 3, Min = 0, None = -1)]
    public enum RequestMethodEnum
    {
        /// <summary>
        /// None：-1
        /// </summary>
        [EnumItem(Value = -1, Desc = "无")]
        None = -1,
        /// <summary>
        /// POST：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "POST")]
        POST = 0,
        /// <summary>
        /// GET:1
        /// </summary>
        [EnumItem(Value = 1, Desc = "GET")]
        GET = 1,

    }
}
