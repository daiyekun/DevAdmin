
using System;
using System.Collections.Generic;
using System.Text;
using WooDev.Common.Utility;

namespace WooDev.Common.Extend
{
   
    /// <summary>
    /// 操作日志类型
    /// </summary>
    [EnumClass(Max = 20, Min = 0, None = -1)]
    public enum OptionLogEnum
    {
        /// <summary>
        /// None：-1
        /// </summary>
        [EnumItem(Value = -1, Desc = "无")]
        None = -1,
        /// <summary>
        /// 新增：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "新增")]
        Add = 0,
        /// <summary>
        /// 删除:1
        /// </summary>
        [EnumItem(Value = 1, Desc = "删除")]
        Del = 1,
        /// <summary>
        /// 修改:2
        /// </summary>
        [EnumItem(Value = 2, Desc = "修改")]
        Update = 2,
        /// <summary>
        /// 查询:3
        /// </summary>
        [EnumItem(Value = 3, Desc = "查询")]
        Select = 3,
        /// <summary>
        /// 查询:4
        /// </summary>
        [EnumItem(Value = 4, Desc = "其他")]
        Other = 4,
        /// <summary>
        /// 设置:5
        /// </summary>
        [EnumItem(Value = 5, Desc = "设置")]
        Set = 5,
        /// <summary>
        /// 上传文件:6
        /// </summary>
        [EnumItem(Value = 6, Desc = "上传文件")]
        Upload = 6,
        /// <summary>
        /// 上传文件:7
        /// </summary>
        [EnumItem(Value = 7, Desc = "下载文件")]
        Download = 7,
        /// <summary>
        /// 变更:8
        /// </summary>
        [EnumItem(Value = 8, Desc = "变更")]
        Change =8,
        /// <summary>
        /// 权限分配:9
        /// </summary>
        [EnumItem(Value = 9, Desc = "权限分配")]
        Perssion = 9,
        /// <summary>
        /// 新建/修改:10
        /// </summary>
        [EnumItem(Value = 10, Desc = "新建/修改")]
        UpdateOrAdd = 10,

    }
}
