using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
        [EnumItemFlowItemExt(Value = 1, Desc = "未执行-->执行中", StartSta = 0, EndSta = 1)]
        State1 = 1,
        /// <summary>
        /// 2:变更未执行-->执行中
        /// </summary>
        [EnumItemFlowItemExt(Value = 2, Desc = "变更未执行-->执行中", StartSta = 0, EndSta = 1)]
        State2 = 2,
        /// <summary>
        /// 3:执行中-->已完成
        /// </summary>
        [EnumItemFlowItemExt(Value = 3, Desc = "执行中-->已完成", StartSta = 1, EndSta = 2)]
        State3 = 3,
        /// <summary>
        /// 4:执行中-->已终止
        /// </summary>
        [EnumItemFlowItemExt(Value = 4, Desc = "执行中-->已终止", StartSta = 1, EndSta = 3)]
        State4 = 4,
        /// <summary>
        /// 5:未执行-->已作废
        /// </summary>
        [EnumItemFlowItemExt(Value = 5, Desc = "未执行-->已作废", StartSta = 0, EndSta = 4)]
        State5 = 5,
        /// <summary>
        /// 6:审批通过-->执行中
        /// </summary>
        [EnumItemFlowItemExt(Value = 6, Desc = "审批通过-->执行中", StartSta = 6, EndSta = 1)]
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
        [EnumItemFlowItemExt(Value = 1, Desc = "未审核-->审核通过",StartSta =0,EndSta =1)]
        State1 = 1,
        /// <summary>
        /// 2:审核通过-->已终止
        /// </summary>
        [EnumItemFlowItemExt(Value = 2, Desc = "审核通过-->已终止",StartSta =1,EndSta =2)]
        State2 = 2,
        /// <summary>
        /// 3:已终止-->审核通过
        /// </summary>
        [EnumItemFlowItemExt(Value = 3, Desc = "已终止-->审核通过",StartSta =2,EndSta =1)]
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
        [EnumItemFlowItemExt(Value = 1, Desc = "未提交-->已提交", StartSta = 0, EndSta = 1)]
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
        [EnumItemFlowItemExt(Value = 1, Desc = "未提交-->已提交", StartSta = 0, EndSta = 1)]
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
        [EnumItemFlowItemExt(Value = 1, Desc = "未执行-->执行中", StartSta = 0, EndSta = 1)]
        State1 = 1,
        /// <summary>
        /// 2:未执行-->已作废
        /// </summary>
        [EnumItemFlowItemExt(Value = 2, Desc = "未执行-->已作废", StartSta = 0, EndSta = 3)]
        State2 = 2,
        /// <summary>
        /// 3:执行中-->已完成
        /// </summary>
        [EnumItemFlowItemExt(Value = 3, Desc = "执行中-->已完成", StartSta = 1, EndSta = 2)]
        State3 = 3,
        /// <summary>
        /// 4:执行中-->已终止
        /// </summary>
        [EnumItemFlowItemExt(Value = 4, Desc = "执行中-->已终止",StartSta = 1, EndSta = 4)]
        State4 = 4
    }





}
