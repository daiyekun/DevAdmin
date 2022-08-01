using System;
using System.Collections.Generic;
using System.Text;
using WooDev.Common.Utility;

namespace WooDev.ViewModel.Enums
{
    /// <summary>
    /// 审批模板对象
    /// </summary>
    [EnumClass(Max = 15, Min = 0, None = -1)]
    public  enum FlowObjEnums
    {
        /// <summary>
        /// 客户：0
        /// </summary>
        [EnumItemExAttribute(Value = 0, Desc = "客户", TypeValue = typeof(CommpanyFlowItemsEnum))]
        Customer = 0,
        /// <summary>
        /// 供应商：1
        /// </summary>
        [EnumItemExAttribute(Value = 1, Desc = "供应商", TypeValue = typeof(CommpanyFlowItemsEnum))]
        Supplier = 1,
        /// <summary>
        /// 其他对方：2
        /// </summary>
        [EnumItemExAttribute(Value = 2, Desc = "其他对方", TypeValue = typeof(CommpanyFlowItemsEnum))]
        Other = 2,
        /// <summary>
        /// 合同：3
        /// </summary>
        [EnumItemExAttribute(Value = 3, Desc = "合同", TypeValue = typeof(ContFlowItemsEnum))]
        Contract = 3,
        /// <summary>
        /// 收票：4
        /// </summary>
        [EnumItemExAttribute(Value = 4, Desc = "收票", TypeValue = typeof(InvoiceFlowItemsEnum))]
        InvoiceIn = 4,
        /// <summary>
        /// 开票：5
        /// </summary>
        [EnumItemExAttribute(Value = 5, Desc = "开票", TypeValue = typeof(InvoiceFlowItemsEnum))]
        InvoiceOut = 5,
        /// <summary>
        /// 付款：6
        /// </summary>
        [EnumItemExAttribute(Value = 6, Desc = "付款", TypeValue = typeof(ActFanceFlowItemsEnum))]
        payment = 6,
        /// <summary>
        /// 项目：7
        /// </summary>
        [EnumItemExAttribute(Value = 7, Desc = "项目", TypeValue = typeof(ProjectFlowItemsEnum))]
        project = 7,
        

    }

    
}
