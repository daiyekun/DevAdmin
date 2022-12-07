using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Utility;

namespace WooDev.ViewModel.Enums
{
    [EnumClass(Max = 5, Min = 0, None = -1)]
    public enum WorkFlowStateEnums
    {/// <summary>
     /// 审批中：1
     /// </summary>
        [EnumItem(Value = 1, Desc = "审批中")]
        SPZ = 1,
        /// <summary>
        /// 审批通过:2
        /// </summary>
        [EnumItem(Value = 2, Desc = "审批通过")]
        SPTG = 2,
        /// <summary>
        /// 被打回:3
        /// </summary>
        [EnumItem(Value = 3, Desc = "被打回")]
         BDH = 3,
       
    
}
}
