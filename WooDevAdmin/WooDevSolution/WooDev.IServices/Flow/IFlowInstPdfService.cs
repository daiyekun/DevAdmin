using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;
using WooDev.ViewModel.Flow.FlowInstPdf;

namespace WooDev.IServices
{

    /// <summary>
    /// 审批单
    /// </summary>
    public partial interface IFlowInstPdfService: IBaseService<DEV_FLOW_INSTANCE>
    {
        /// <summary>
        /// 根据审批实例获取审批单需要的合同对方数据
        /// </summary>
        /// <param name="appInst">审批实例</param>
        /// <returns>审批单数据对象</returns>
        CompanyInfo GetCommpanyFlowPdfData(DEV_FLOW_INSTANCE appInst);
        /// <summary>
        /// 合同审批单信息
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>合同审批单对象</returns>
        ContractPdfInfo GetContractFlowPdfData(DEV_FLOW_INSTANCE appInst);
    }
}
