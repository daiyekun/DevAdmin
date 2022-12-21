using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Linq.Expressions;
using WooDev.Common.Models;
using WooDev.Common.Utility;
using WooDev.IServices;
using WooDev.Model.Models;
using WooDev.ViewModel.ExtendModel;
using WooDev.ViewModel;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.Utiltiy;

namespace WooDev.WebApi.Controllers.Constact.CollContract
{
    /// <summary>
    /// 合同附件
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("default")]
    [Authorize]
    public class ContAttachmentController : ControllerBase
    {
        private IDevContAttachmentService _IDevContAttachmentService;
        private IMapper _IMapper;
        private IDevContractService _IDevContractService;
        public ContAttachmentController(IDevContAttachmentService iDevContAttachmentService, IMapper iMapper, IDevContractService iDevContractService)
        {
            _IDevContAttachmentService = iDevContAttachmentService;
            _IMapper = iMapper;
            _IDevContractService = iDevContractService;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="uploadFiles">对象</param>
        /// <returns></returns>
        [Route("contAttachmentSave")]
        [HttpPost]
        public IActionResult ContAttachmentSave([FromBody] List<UploadFileInfo> uploadFiles)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();

            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            List<DEV_CONT_ATTACHMENT> listfiles = new List<DEV_CONT_ATTACHMENT>();
            foreach (var item in uploadFiles)
            {
                var info = new DEV_CONT_ATTACHMENT();
                info.CONT_ID = item.TempId <= 0 ? -userId : item.TempId;
                info.F_NAME = item.SourceFileName;
                info.FILE_NAME = item.GuidFileName;
                info.FILE_PATH = item.FolderPath;
                info.EXTEND = item.Extension;
                info.IS_DELETE = 0;
                info.CREATE_TIME = DateTime.Now;
                info.CREATE_USERID = userId;
                info.UPDATE_TIME = DateTime.Now;
                info.UPDATE_USERID = userId;
                info.DOWN_TIME = 0;
                info.GUID_FILE_NAME = item.GuidFileName;
                listfiles.Add(info);

            }
            _IDevContAttachmentService.Add(listfiles);

            return new DevResponseJson(result);

        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getContAttachmentList")]
        [HttpGet]
        [Authorize]
        public IActionResult GetContAttachmentList([FromQuery] PageParams pageParams, [FromQuery] DevComFileSearch serachParam)

        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new NoPageInfo<DEV_CONT_ATTACHMENT>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_CONT_ATTACHMENT>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);
            whereexp = whereexp.And(a => a.CONT_ID == -userId || a.CONT_ID == serachParam.CustId);
            Expression<Func<DEV_CONT_ATTACHMENT, object>> orderbyLambda = a => a.ID;
            var data = _IDevContAttachmentService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevContAttachmentList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 修改合同附件字段
        /// </summary>
        /// <param name="updateField"></param>
        /// <returns></returns>
        [Route("contAttachmentUpdateField")]
        [HttpPost]
        public IActionResult ContAttachmentUpdateField([FromBody] UpdateField updateField)
        {
            _IDevContAttachmentService.UpdateField(updateField);
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);
        }
        /// <summary>
        /// 删除附件数据
        /// </summary>
        /// <param name="Ids">选中ID</param>
        [Route("contAttachmentDel")]
        [HttpGet]

        public IActionResult ContAttachmentDel(string Ids)
        {
            var arrIds = StringHelper.String2ArrayInt(Ids);
            _IDevContAttachmentService.Delete(a => arrIds.Contains(a.ID));
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);
        }
    }
}
