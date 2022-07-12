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
     /// 审批中：0
     /// </summary>
        [EnumItem(Value = 0, Desc = "审批中")]
        SPZ = 0,
        /// <summary>
        /// 审批通过:1
        /// </summary>
        [EnumItem(Value = 1, Desc = "审批通过")]
        SPTG = 1,
        /// <summary>
        /// 被打回:1
        /// </summary>
        [EnumItem(Value = 2, Desc = "被打回")]
         BDH = 2,
       
    
}
}
