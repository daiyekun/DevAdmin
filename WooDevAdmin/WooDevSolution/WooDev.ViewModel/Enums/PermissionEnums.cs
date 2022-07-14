using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Utility;

namespace WooDev.ViewModel.Enums
{
    /// <summary>
    /// 权限枚举
    /// </summary>
    [EnumClass(Max = 10, Min = 0, None = -1)]
    public enum PermissionDicEnum
    {
        /// <summary>
        /// 无权限：0
        /// </summary>
        [EnumItem(Value = -1, Desc = "无权限")]
        None = -1,
        /// <summary>
        /// 有权限：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "有权限")]
        OK = 0,
        /// <summary>
        /// 当前状态不允许操作：2
        /// </summary>
        [EnumItem(Value = 2, Desc = "当前状态不允许此操作")]
        NotState = 2,


    }

    /// <summary>
    /// 权限枚举
    /// </summary>
    [EnumClass(Max = 10, Min = 0, None = -1)]
    public enum PerissionEnums
    {
        /// <summary>
        /// 无权限：-1
        /// </summary>
        [EnumItem(Value = -1, Desc = "无权限")]
        None = -1,
        /// <summary>
        /// 个人：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "个人")]
        personal = 0,
        /// <summary>
        /// 机构：1
        /// </summary>
        [EnumItem(Value = 1, Desc = "机构")]
        DepartTree = 1,
        /// <summary>
        /// 全部：2
        /// </summary>
        [EnumItem(Value = 2, Desc = "全部")]
        All = 2,
        /// <summary>
        /// 本机构：3
        /// </summary>
        [EnumItem(Value = 3, Desc = "本机构")]
         Depart = 3,
        /// <summary>
        /// 是：4
        /// </summary>
        [EnumItem(Value = 4, Desc = "是")]
        Yes = 4,
        /// <summary>
        /// 否：5
        /// </summary>
        [EnumItem(Value = 5, Desc = "否")]
        No = 5,

    }
}
