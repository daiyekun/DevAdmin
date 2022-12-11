using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using System.Globalization;
using WooDev.Common.Models;
using WooDev.Common.Utility;
using WooDev.IServices;
using WooDev.ViewModel.Enums;
using WooDev.ViewModel.ExtendModel;
using WooDev.ViewModel.Flow.FlowInstPdf;
using WooDev.WebCommon.Utiltiy;

namespace WooDev.WebApi.Controllers.Flow
{

    /// <summary>
    /// 打印审批单
    /// </summary>
    [Route("api/[controller]")]
    [EnableCors("default")]
    //[Authorize]
    public class FlowToPdfController : Controller
    {
        private IDevFlowInstanceService _IDevFlowInstanceService;
        private IFlowInstPdfService _IFlowInstPdfService;
        public FlowToPdfController(IDevFlowInstanceService iDevFlowInstanceService, IFlowInstPdfService iFlowInstPdfService)
        {
            _IDevFlowInstanceService = iDevFlowInstanceService;
            _IFlowInstPdfService = iFlowInstPdfService;
        }


        /// <summary>
        /// 根据审批实例生成pdf
        /// </summary>
        /// <param name="instId">审批实例ID</param>
        /// <returns></returns>
        [Route("flowInstToPdf")]
        [HttpGet]
        public IActionResult FlowInstToPdf(int instId)
        {

            ViewAsPdf demoViewPortrait = null;
            var wfinfo = _IDevFlowInstanceService.InSingle(instId);
            switch (wfinfo.FLOW_TYPE)
            {
                case (int)FlowObjEnums.Customer:
                    {
                        CompanyInfo info = _IFlowInstPdfService.GetCommpanyFlowPdfData(wfinfo);
                        demoViewPortrait = new ViewAsPdf("CustomerPDF", info);
                        demoViewPortrait.FileName = "customer.pdf";


                    }
                    break;

            }

            //纵向、横向
            demoViewPortrait.PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait;
            //页面大小
            demoViewPortrait.PageSize = Rotativa.AspNetCore.Options.Size.A4;



            return demoViewPortrait;





        }
    }
}
