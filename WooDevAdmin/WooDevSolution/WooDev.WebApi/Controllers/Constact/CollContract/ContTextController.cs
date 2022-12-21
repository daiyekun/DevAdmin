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
using WooDev.Services;
using WooDev.ViewModel.ExtendModel;
using WooDev.ViewModel;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.Utiltiy;

namespace WooDev.WebApi.Controllers.Constact.CollContract
{

    /// <summary>
    /// 合同文本
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("default")]
    [Authorize]
    public class ContTextController : ControllerBase
    {
        private IDevContConttextService _IDevContConttextService;
        private IMapper _IMapper;
        private IDevContractService _IDevContractService;
        public ContTextController(IDevContConttextService iDevContConttextService, IMapper iMapper, IDevContractService iDevContractService)
        {
            _IDevContConttextService = iDevContConttextService;
            _IMapper = iMapper;
            _IDevContractService = iDevContractService;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="uploadFiles">对象</param>
        /// <returns></returns>
        [Route("contTextSave")]
        [HttpPost]
        public IActionResult ContTextSave([FromBody] List<UploadFileInfo> uploadFiles)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();

            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            List<DEV_CONT_CONTTEXT> listfiles = new List<DEV_CONT_CONTTEXT>();
            foreach (var item in uploadFiles)
            {
                var info = new DEV_CONT_CONTTEXT();
                info.CONT_ID = item.TempId <= 0 ? -userId : item.TempId;
                info.NAME = item.SourceFileName;
                info.FILE_NAME = item.GuidFileName;
                info.TEXT_PATH = item.FolderPath;
                info.EXTEND = item.Extension;
                info.IS_DELETE = 0;
                info.CREATE_TIME = DateTime.Now;
                info.CREATE_USERID = userId;
                info.UPDATE_TIME = DateTime.Now;
                info.UPDATE_USERID = userId;
                info.DOWN_TIMES = 0;
                info.GUID_FILE_NAME = item.GuidFileName;
                info.STAGE = 0;
                listfiles.Add(info);

            }
            _IDevContConttextService.Add(listfiles);

            return new DevResponseJson(result);

        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getContTextList")]
        [HttpGet]
        [Authorize]
        public IActionResult GetContTextList([FromQuery] PageParams pageParams, [FromQuery] DevComFileSearch serachParam)

        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new NoPageInfo<DEV_CONT_CONTTEXT>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_CONT_CONTTEXT>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);
            whereexp = whereexp.And(a => a.CONT_ID == -userId || a.CONT_ID == serachParam.CustId);
            Expression<Func<DEV_CONT_CONTTEXT, object>> orderbyLambda = a => a.ID;
            var data = _IDevContConttextService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevContConttextList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 修改文本字段
        /// </summary>
        /// <param name="updateField"></param>
        /// <returns></returns>
        [Route("contTextUpdateField")]
        [HttpPost]
        public IActionResult ContTextUpdateField([FromBody] UpdateField updateField)
        {
            _IDevContConttextService.UpdateField(updateField);
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
        [Route("contTextDel")]
        [HttpGet]

        public IActionResult ContTextDel(string Ids)
        {
            var arrIds = StringHelper.String2ArrayInt(Ids);
            _IDevContConttextService.Delete(a => arrIds.Contains(a.ID));
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);
        }
    }
}
