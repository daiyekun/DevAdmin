using System;
using System.Collections.Generic;
using System.Text;

namespace WooDev.ViewModel.Flow.FlowInstPdf
{
    /// <summary>
    /// 付款单实体
    /// </summary>
     public  class PaymentPdfInfo : FlowPdfBase
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
        /// 合同对方
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 银行
        /// </summary>
        public string Bank { get; set; }
        /// <summary>
        /// 子账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string SettDic { get; set; }
    }
}
