using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Utility;

namespace WooDev.ViewModel.Enums
{

    /// <summary>
    /// 文件夹枚举
    /// </summary>
    [EnumClass(Max = 50, Min = 1, None = 0)]
    public enum DevFolderEnums
    { /// <summary>
      /// 未知
      /// </summary>
        [EnumItem(Value = 0, Desc = "None")]
        None = 0,
        /// <summary>
        /// 头像文件夹
        /// </summary>
        [EnumItem(Value = 1, Desc = "UserHead")]
        UserHead = 1,
        /// <summary>
        /// 客户
        /// </summary>
        [EnumItem(Value = 2, Desc = "Customer")]
        Customer = 2,
        /// <summary>
        /// 供应商
        /// </summary>
        [EnumItem(Value = 3, Desc = "Supplier")]
        Supplier = 3,
        /// <summary>
        ///其他对方
        /// </summary>
        [EnumItem(Value = 4, Desc = "Other")]
        Other = 4,
        /// <summary>
        /// 项目
        /// </summary>
        [EnumItem(Value = 5, Desc = "Project")]
        Project = 5,
        /// <summary>
        /// 合同文本
        /// </summary>
        [EnumItem(Value = 6, Desc = "ContractText")]
        ContractText = 6,
        /// <summary>
        /// 合同附件
        /// </summary>
        [EnumItem(Value = 7, Desc = "ContractFile")]
        ContractFile = 7,
        /// <summary>
        /// 发票
        /// </summary>
        [EnumItem(Value = 8, Desc = "InvoiceFile")]
        InvoiceFile = 8,
        /// <summary>
        /// 实际资金
        /// </summary>
        [EnumItem(Value = 9, Desc = "ActualFile")]
        ActualFile = 9,
        /// <summary>
        /// 导出文件
        /// </summary>
        [EnumItem(Value = 10, Desc = "ExportFile")]
        ExportFile = 10,
    }
}
