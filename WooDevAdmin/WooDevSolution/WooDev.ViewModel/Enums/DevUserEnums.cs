using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Utility;

namespace WooDev.ViewModel.Enums
{

    /// <summary>
    /// 用户状态
    /// </summary>
    [EnumClass(Max = 5, Min = 0, None = -1)]
    public enum UserStateEnum
    {
        /// <summary>
        /// 禁用：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "禁用")]
        JinYong = 0,
        /// <summary>
        /// 启用:1
        /// </summary>
        [EnumItem(Value = 1, Desc = "启用")]
        QuYong = 1,
        /// <summary>
        /// 离职:1
        /// </summary>
        [EnumItem(Value = 2, Desc = "离职")]
        LiZhi = 2,
    }
    /// <summary>
    /// 用户状态
    /// </summary>
    [EnumClass(Max = 3, Min = 0, None = -1)]
    public enum UserSexEnum
    {
        /// <summary>
        /// 女：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "女")]
        NVR = 0,
        /// <summary>
        /// 男:1
        /// </summary>
        [EnumItem(Value = 1, Desc = "男")]
        NR = 1,
        /// <summary>
        /// 未知:2
        /// </summary>
        [EnumItem(Value = 2, Desc = "未知")]
        WZ = 2,
    }
}
