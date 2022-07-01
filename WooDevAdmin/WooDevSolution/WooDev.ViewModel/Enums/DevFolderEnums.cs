using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Utility;

namespace WooDev.ViewModel.Enums
{

    /// <summary>
    /// 文件夹
    /// </summary>
    [EnumClass(Max = 50, Min = 0, None = -1)]
    public enum DevFolderEnums
    {
        /// <summary>
        /// 头像文件夹
        /// </summary>
        [EnumItem(Value = 1, Desc = "UserHead")]
        UserHead = 1,
    }
}
