using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Utility;

namespace WooDev.ViewModel.Enums
{

    
        /// <summary>
        /// 节点信息操作类型
        /// </summary>
        [EnumClass(Min = 1, Max = 5, Default = -1)]
        public enum OptTypeEnum
        {
            /// <summary>
            /// 1:人力资源
            /// </summary>
            [EnumItem(Value = 1, Desc = "人力资源")]
            UserRes = 1,
            /// <summary>
            /// 2:审批组
            /// </summary>
            [EnumItem(Value = 2, Desc = "审批组")]
            FlowGroup = 2,
            /// <summary>
            /// 3:系统角色
            /// </summary>
            [EnumItem(Value = 3, Desc = "系统角色")]
            Role = 3,
            /// <summary>
            /// 4:岗位
            /// </summary>
            [EnumItem(Value = 4, Desc = "岗位")]
            Post = 4
        }

    /// <summary>
    /// 审批规则
    /// </summary>
    [EnumClass(Max = 2, Min = 0, None = -1)]
    public enum NruleEnum
    {
        /// <summary>
        /// 全部通过：1
        /// </summary>
        [EnumItem(Value = 1, Desc = "全部通过")]
        Qbtg = 1,
        /// <summary>
        /// 任意通过:2
        /// </summary>
        [EnumItem(Value = 2, Desc = "任意通过")]
        RyTg =2,
       
    }


}

