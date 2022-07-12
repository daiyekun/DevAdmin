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
    /// 客户
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("default")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private IDevCompanyService _IDevCompanyService;
        private IMapper _IMapper;
        private IDevCompFileService _IDevCompFileService;
        public CustomerController(IDevCompanyService iDevCompanyService, IMapper iMapper, IDevCompFileService iDevCompFileService)
        {
            _IDevCompanyService = iDevCompanyService;
            _IMapper = iMapper;
            _IDevCompFileService = iDevCompFileService;
        }


        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getCustomerList")]
        [HttpGet]
        [Authorize]
        public IActionResult GetList([FromQuery] PageParams pageParams, [FromQuery] DevCompanySearch serachParam)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new PageInfo<DEV_COMPANY>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = GetFilterExpress(serachParam);
            whereexp = whereexp.And(a => a.IS_DELETE == 0);
            Expression<Func<DEV_COMPANY, object>> orderbyLambda = a => a.ID;
            var data = _IDevCompanyService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevCompanyList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }

        private Expressionable<DEV_COMPANY> GetFilterExpress(DevCompanySearch serachParam)
        {
            var whereexp = Expressionable.Create<DEV_COMPANY>();
            whereexp = whereexp.And(a=>a.C_TYPE==0);//客户
            if (!string.IsNullOrEmpty(serachParam.Name))
            {//搜索名称
                whereexp = whereexp.And(a => a.NAME.Contains(serachParam.Name));
            }
            if (!string.IsNullOrEmpty(serachParam.Code))
            {//搜索编号
                whereexp = whereexp.And(a => a.CODE.Contains(serachParam.Code));
            }
            if (serachParam.CateId > 0)
            {//部门ID
                whereexp = whereexp.And(a => a.CATE_ID == serachParam.CateId);
            }

            return whereexp;
        }

        /// <summary>
        /// 新增，修改保存
        /// </summary>
        /// <param name="roleDTO">角色对象</param>
        /// <returns></returns>
        [Route("customerSave")]
        [HttpPost]
        public IActionResult CustomerSave([FromBody] DevCompanyDTO devCompanyDTO)
        {

            var userId = HttpContext.User.Claims.GetTokenUserId();
            //if (devCompanyDTO.ID > 0)
            //{

            //    var  customerinfo = _IDevCompanyService.InSingle(devCompanyDTO.ID);
            //    var saveinfo = _IMapper.Map<DevCompanyDTO, DEV_COMPANY>(devCompanyDTO, customerinfo);
            //    saveinfo.UPDATE_TIME = DateTime.Now;
            //    saveinfo.UPDATE_USERID = userId;
            //    saveinfo.C_STATE = 0;//

            //    _IDevCompanyService.Update(saveinfo);


            //}
            //else
            //{
            //    var info = _IMapper.Map<DEV_COMPANY>(devCompanyDTO);
            //    info.CREATE_TIME = DateTime.Now;
            //    info.UPDATE_TIME = DateTime.Now;
            //    info.CREATE_USERID = userId;
            //    info.UPDATE_USERID = userId;
            //    info.C_STATE = 0;//

            //}
            devCompanyDTO.CREATE_ID = userId;
            
            _IDevCompanyService.CompanySave(devCompanyDTO);


            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);


        }

        
    }
}
