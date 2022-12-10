using System;
using System.Collections.Generic;
using System.Text;

namespace WooDev.ViewModel.Flow.FlowInstPdf
{
    /// <summary>
    /// 项目
    /// </summary>
   public class ProjectInfo: FlowPdfBase
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 类别
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
        /// 负责人
        /// </summary>
        public string PrincipalUser { get; set; }
        /// <summary>
        /// 计划收款
        /// </summary>
        public string jhskThod { get; set; }
        /// <summary>
        /// 计划付款
        /// </summary>
        public string jhfkThod { get; set; }
    }
}
