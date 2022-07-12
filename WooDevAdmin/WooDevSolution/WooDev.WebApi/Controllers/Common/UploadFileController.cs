using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using WooDev.Common.Models;
using WooDev.Common.Utility;
using WooDev.IServices;
using WooDev.ViewModel.ExtendModel;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.FilterExtend;
using WooDev.WebCommon.Utiltiy;

namespace WooDev.WebApi.Controllers.Common
{

    /// <summary>
    /// 公共上传文件
    /// 计划所有文件上传都经过此控制
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UploadFileController : ControllerBase
    {
        private IDevUserOtherInfoService _IDevUserOtherInfoService;
        private static readonly FormOptions _defaultFormOptions = new FormOptions();
        public  UploadFileController(IDevUserOtherInfoService iDevUserOtherInfoService)
        {
            _IDevUserOtherInfoService = iDevUserOtherInfoService;
        }

        /// <summary>
        ///文件上传入口
        /// </summary>
        /// <returns></returns>
        [Route("upload")]
        [HttpPost]
        [DisableFormValueModelBinding]
        [EnableBuffering]
        public async Task<IActionResult> Upload()
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            FormValueProvider formModel;
            UploadFileInfo uploadFileInfo = new UploadFileInfo();
            var tempfolder = 0;
            var foleder = HttpContext.Request.Form["FolderIndex"];
            int.TryParse(foleder, out  tempfolder);
            if (tempfolder == 0)
            {//上传头像
                formModel = await Request.StreamFilesUploadFileInfo(uploadFileInfo);
                switch (uploadFileInfo.FolderIndex)
                {
                    case 1://头像
                        var strsql = $"update dev_user_other_info set HEADPATH='/Uploads/{uploadFileInfo.FolderPath}' where USER_ID={userId};";
                        _IDevUserOtherInfoService.ExecuteCommand(strsql);

                        break;

                }
               
                var result = new HeadUploadData
                {
                    code = 0,
                    message = "ok",
                    src = $"{Request.Host}/Uploads/{uploadFileInfo.FolderPath}"
                };
                return new DevResultJson(result);
            }
            else
            {//其他文件上传
                uploadFileInfo.FolderIndex = tempfolder;
                formModel = await Request.DevStreamFiles(uploadFileInfo);
                var result = new UploadResultData<UploadFileInfo>
                {
                    code = 0,
                    message = "ok",
                    result = uploadFileInfo,
                    url = JsonUtility.SerializeObject(uploadFileInfo)

                };
                return new DevResultJson(result);

            }




            }



     

    }
    /// <summary>
    /// 目前没有意义
    /// </summary>
    public class MyViewModel
    {
        public string Username { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }
    }



}
