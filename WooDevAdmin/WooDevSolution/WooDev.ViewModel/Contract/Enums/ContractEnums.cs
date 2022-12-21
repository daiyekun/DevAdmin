using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Utility;

namespace WooDev.ViewModel.Contract.Enums
{
    /// <summary>
    /// 资金性质
    /// </summary>
    [EnumClass(Max = 3, Min = 0, None = -1)]
    public enum FinceType
    {
        /// <summary>
        /// 收款：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "收款")]
        Put = 0,
        /// <summary>
        /// 付款：1
        /// </summary>
        [EnumItem(Value = 1, Desc = "付款")]
        pay = 1,
        /// <summary>
        /// 收付款：2
        /// </summary>
        [EnumItem(Value = 2, Desc = "收付款")]
        All = 2,

    }
    /// <summary>
    /// 合同属性
    /// </summary>
    [EnumClass(Max = 3, Min = 0, None = -1)]
    public enum ContractProperty
    {
        /// <summary>
        /// standard：0：标准合同
        /// </summary>
        [EnumItem(Value = 0, Desc = "标准合同")]
        Standard = 0,
        /// <summary>
        /// Framework:框架合同：1
        /// </summary>
        [EnumItem(Value = 1, Desc = "框架合同")]
        Framework = 1,


    }
    /// <summary>
    /// 合同状态
    /// </summary>
    [EnumClass(Max = 10, Min = 0, None = -1)]
    public enum ContractStateEnums
    {
        /// <summary>
        /// 未执行：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "未执行")]
        NonExecution = 0,
        /// <summary>
        /// 执行中1
        /// </summary>
        [EnumItem(Value = 1, Desc = "执行中")]
        Execution = 1,
        /// <summary>
        /// 已终止2
        /// </summary>
        [EnumItem(Value = 2, Desc = "已终止")]
        Terminated = 2,
        /// <summary>
        /// 已作废3
        /// </summary>
        [EnumItem(Value = 3, Desc = "已作废")]
        Dozee = 3,
        /// <summary>
        /// 审批中4
        /// </summary>
        [EnumItem(Value = 4, Desc = "审批中")]
        Approvaling = 4,
        /// <summary>
        /// 审批中5
        /// </summary>
        [EnumItem(Value = 5, Desc = "被打回")]
        Back = 5,
        /// <summary>
        /// 审批中6
        /// </summary>
        [EnumItem(Value = 6, Desc = "已完成")]
        Completed = 6,
        /// <summary>
        /// 审批通过8
        /// </summary>
        [EnumItem(Value = 8, Desc = "审批通过")]
        Approve = 8,
    }

    /// <summary>
    /// 标的类型
    /// </summary>
    public enum IsFromCategoryEnum
    {
        /// <summary>
        /// 0 非业务类
        /// </summary>
        NoClass = 0,
        /// <summary>
        /// 1 业务类
        /// </summary>
        Class = 1,
        /// <summary>
        /// 2 Excel导入
        /// </summary>
        Excel = 2,
    }
}
