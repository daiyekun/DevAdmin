using System;
using System.Collections.Generic;
using System.Text;

namespace WooDev.ViewModel.Flow.FlowInstPdf
{
    /// <summary>
    /// 收票审批单实例
    /// </summary>
    public  class InvoiceInPdfInfo : FlowPdfBase
    {
        /// <summary>
        /// 单据ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContName { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContCode { get; set; }
        /// <summary>
        /// 合同类别
        /// </summary>
        public string CateName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 付款金额
        /// </summary>
        public string AmountMoneyThod { get; set; }
        /// <summary>
        /// 发票类型
        /// </summary>
        public string InTypeDic { get; set; }
        /// <summary>
        /// 开具时间
        /// </summary>
        public string MakeOutDateTime { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string InCode { get; set; }
        /// <summary>
        /// 合同对方
        /// </summary>
        public string CompanyName { get; set; }
    }
}
