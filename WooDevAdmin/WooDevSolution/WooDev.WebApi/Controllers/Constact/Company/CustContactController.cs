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
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.Utiltiy;

namespace WooDev.WebApi.Controllers.Constact.Company
{

    /// <summary>
    /// 合同对方联系人
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("default")]
    [Authorize]
    public class CustContactController : ControllerBase
    {
        private IDevCompContactsService _IDevCompContactsService;
        private IMapper _IMapper;
        public CustContactController(IDevCompContactsService iDevCompContactsService
            , IMapper iMapper)
        {
            _IDevCompContactsService = iDevCompContactsService;
            _IMapper = iMapper;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getCustContactList")]
        [HttpGet]
       
        public IActionResult GetCustContactList([FromQuery] PageParams pageParams, [FromQuery] DevComFileSearch serachParam)

        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new NoPageInfo<DEV_COMP_CONTACTS>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_COMP_CONTACTS>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);
            whereexp = whereexp.And(a => a.COMP_ID == -userId || a.COMP_ID == serachParam.CustId);
            Expression<Func<DEV_COMP_CONTACTS, object>> orderbyLambda = a => a.ID;
            var data = _IDevCompContactsService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevCompContactsList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="menuSaveDTO">菜单对象</param>
        /// <returns></returns>
        [Route("custContactSave")]
        [HttpPost]
        [Authorize]
        public IActionResult CustContactSave([FromBody] DevCompContactsDTO contactsDTO)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();

            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            var info = _IMapper.Map<DEV_COMP_CONTACTS>(contactsDTO);
            if (contactsDTO.ID>0)
            {//修改
                var userinfo = _IDevCompContactsService.InSingle(contactsDTO.ID);
                var saveinfo = _IMapper.Map<DevCompContactsDTO, DEV_COMP_CONTACTS>(contactsDTO);
                saveinfo.UPDATE_TIME = DateTime.Now;
                saveinfo.UPDATE_USERID = userId;
                saveinfo.IS_DELETE = 0;
                _IDevCompContactsService.Update(saveinfo);
            }
            else
            {
                info.UPDATE_TIME = DateTime.Now;
                info.UPDATE_USERID = userId;
                info.IS_DELETE = 0;
                info.CREATE_TIME = DateTime.Now;
                info.CREATE_USERID = userId;
                info.COMP_ID = -userId;
                _IDevCompContactsService.Add(info);
            }
            

            return new DevResponseJson(result);

        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Ids">选中ID</param>
        [Route("custContactDel")]
        [HttpGet]
        public IActionResult CustFileDel(string Ids)
        {
            var arrIds = StringHelper.String2ArrayInt(Ids);
            _IDevCompContactsService.Delete(a => arrIds.Contains(a.ID));
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);
        }

        
    }
}
