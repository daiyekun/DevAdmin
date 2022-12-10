using System;
using System.Collections.Generic;
using System.Text;

namespace WooDev.ViewModel.Flow.FlowInstPdf
{
    /// <summary>
    /// 开票审批单实例
    /// </summary>
    public class InvoiceOutPdfInfo : FlowPdfBase
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
        /// <summary>
        /// 发票抬头
        /// </summary>
        public string InTitle { get; set; }
        /// <summary>
        /// 纳税人识别号
        /// </summary>
        public string NaShuiHao { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string InAddress { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string InTel { get; set; }
        /// <summary>
        /// 银行
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string BankAccount { get; set; }




    }
}
