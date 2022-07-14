using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Utility;

namespace WooDev.ViewModel.Contract.Enums
{
   
    /// <summary>
    /// 合同对方状态
    /// </summary>
    [EnumClass(Max = 5, Min = 0, None = -1)]
    public enum CompanyStateEnums
    {
        /// <summary>
        /// 未审核：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "未审核")]
        WSH = 0,
        /// <summary>
        /// 审核通过:1
        /// </summary>
        [EnumItem(Value = 1, Desc = "审核通过")]
        SHTG = 1,
        /// <summary>
        /// 禁用:2
        /// </summary>
        [EnumItem(Value = 2, Desc = "禁用")]
        JY = 2,
    }
}
