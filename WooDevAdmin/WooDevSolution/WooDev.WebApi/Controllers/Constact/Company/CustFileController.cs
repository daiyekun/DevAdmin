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
using WooDev.ViewModel;
using WooDev.ViewModel.ExtendModel;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.Utiltiy;

namespace WooDev.WebApi.Controllers.Constact.Company
{

    /// <summary>
    /// 合同对方-附件
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("default")]
    [Authorize]
    public class CustFileController : ControllerBase
    {
        private IDevCompanyService _IDevCompanyService;
        private IMapper _IMapper;
        private IDevCompFileService _IDevCompFileService;
        public CustFileController(IDevCompanyService iDevCompanyService, IMapper iMapper, IDevCompFileService iDevCompFileService)
        {
            _IDevCompanyService = iDevCompanyService;
            _IMapper = iMapper;
            _IDevCompFileService = iDevCompFileService;
        }
        #region 附件相关
        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="menuSaveDTO">菜单对象</param>
        /// <returns></returns>
        [Route("customerFileSave")]
        [HttpPost]
        [Authorize]
        public IActionResult CustomerFileSave([FromBody] List<UploadFileInfo> uploadFiles)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();

            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            List<DEV_COMP_FILE> listfiles = new List<DEV_COMP_FILE>();
            foreach (var item in uploadFiles)
            {
                var info = new DEV_COMP_FILE();
                info.COMP_ID = item.TempId<=0? - userId: item.TempId;
                info.NAME = item.SourceFileName;
                info.FILE_NAME = item.GuidFileName;
                info.PATH = item.FolderPath;
                info.EXTEND = item.Extension;
                info.IS_DELETE = 0;
                info.CREATE_TIME = DateTime.Now;
                info.CREATE_USERID = userId;
                info.UPDATE_TIME = DateTime.Now;
                info.UPDATE_USERID = userId;
                info.DOWN_NUM = 0;
                listfiles.Add(info);

            }
            _IDevCompFileService.Add(listfiles);

            return new DevResponseJson(result);

        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getCustFileList")]
        [HttpGet]
        [Authorize]
        public IActionResult GetCustFileList([FromQuery] PageParams pageParams, [FromQuery] DevComFileSearch serachParam)

        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new NoPageInfo<DEV_COMP_FILE>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_COMP_FILE>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);
            whereexp = whereexp.And(a => a.COMP_ID == -userId || a.COMP_ID == serachParam.CustId);
            Expression<Func<DEV_COMP_FILE, object>> orderbyLambda = a => a.ID;
            var data = _IDevCompFileService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevCompFileList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 修改附件字段
        /// </summary>
        /// <param name="updateField"></param>
        /// <returns></returns>
        [Route("custUpdateField")]
        [HttpPost]
        public IActionResult CustUpdateField([FromBody] UpdateField updateField)
        {
            _IDevCompFileService.UpdateField(updateField);
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
        [Route("custFileDel")]
        [HttpGet]

        public IActionResult CustFileDel(string Ids)
        {
            var arrIds = StringHelper.String2ArrayInt(Ids);
            _IDevCompFileService.Delete(a => arrIds.Contains(a.ID));
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);
        }

        #endregion
    }
}
