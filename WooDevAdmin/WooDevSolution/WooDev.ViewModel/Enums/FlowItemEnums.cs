using System;
using System.Collections.Generic;
using System.Text;
using WooDev.Common.Utility;

namespace WooDev.ViewModel.Enums
{
   
    /// <summary>
    /// 合同审批事项
    /// </summary>
    [EnumClass(Max = 5, Min = 1, None = -1)]
    public enum ContFlowItemsEnum
    {
        /// <summary>
        /// 1:未执行-->执行中
        /// </summary>
        [EnumItem(Value = 1, Desc = "未执行-->执行中")]
        State1 = 1,
        /// <summary>
        /// 2:变更未执行-->执行中
        /// </summary>
        [EnumItem(Value = 2, Desc = "变更未执行-->执行中")]
        State2 = 2,
        /// <summary>
        /// 3:执行中-->已完成
        /// </summary>
        [EnumItem(Value = 3, Desc = "执行中-->已完成")]
        State3 = 3,
        /// <summary>
        /// 4:执行中-->已终止
        /// </summary>
        [EnumItem(Value = 4, Desc = "执行中-->已终止")]
        State4 = 4,
        /// <summary>
        /// 5:未执行-->已作废
        /// </summary>
        [EnumItem(Value = 5, Desc = "未执行-->已作废")]
        State5 = 5,
        /// <summary>
        /// 6:审批通过-->执行中
        /// </summary>
        [EnumItem(Value = 6, Desc = "审批通过-->执行中")]
        State6 = 6,



    }

    /// <summary>
    /// 合同对方审批事项
    /// </summary>
    [EnumClass(Min = 1, Max = 3, Default = -1)]
    public enum CommpanyFlowItemsEnum
    {
        /// <summary>
        /// 1:未审核-->审核通过
        /// </summary>
        [EnumItem(Value = 1, Desc = "未审核-->审核通过")]
        State1 = 1,
        /// <summary>
        /// 2:审核通过-->已终止
        /// </summary>
        [EnumItem(Value = 2, Desc = "审核通过-->已终止")]
        State2 = 2,
        /// <summary>
        /// 3:已终止-->审核通过
        /// </summary>
        [EnumItem(Value = 3, Desc = "已终止-->审核通过")]
        State3 = 3,

    }
    /// <summary>
    /// 发票
    /// </summary>
    [EnumClass(Min = 1, Max = 3, Default = -1)]
    public enum InvoiceFlowItemsEnum
    {
        /// <summary>
        /// 1:未提交-->已提交
        /// </summary>
        [EnumItem(Value = 1, Desc = "未提交-->已提交")]
        State1 = 1,
       
    }

    /// <summary>
    /// 实际资金
    /// </summary>
    [EnumClass(Min = 1, Max = 3, Default = -1)]
    public enum ActFanceFlowItemsEnum
    {
        /// <summary>
        /// 1:未提交-->已提交
        /// </summary>
        [EnumItem(Value = 1, Desc = "未提交-->已提交")]
        State1 = 1,
    }

    /// <summary>
    /// 项目
    /// </summary>
    [EnumClass(Min = 1, Max = 3, Default = -1)]
    public enum ProjectFlowItemsEnum
    {
        /// <summary>
        /// 1:未执行-->执行中
        /// </summary>
        [EnumItem(Value = 1, Desc = "未执行-->执行中")]
        State1 = 1,
        /// <summary>
        /// 2:未执行-->已作废
        /// </summary>
        [EnumItem(Value = 2, Desc = "未执行-->已作废")]
        State2 = 2,
        /// <summary>
        /// 3:执行中-->已完成
        /// </summary>
        [EnumItem(Value = 3, Desc = "执行中-->已完成")]
        State3 = 3,
        /// <summary>
        /// 4:执行中-->已终止
        /// </summary>
        [EnumItem(Value = 4, Desc = "执行中-->已终止")]
        State4 = 4
    }





}
