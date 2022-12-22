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

namespace WooDev.WebApi.Controllers.Constact.CollContract
{

    /// <summary>
    /// 标的
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("default")]
    [Authorize]
    public class SubMatterController : ControllerBase
    {
        private IDevContSubMatterService _IDevContSubMatterService;
        private IMapper _IMapper;
        public SubMatterController(IDevContSubMatterService iDevContSubMatterService, IMapper iMapper)
        {
            _IDevContSubMatterService = iDevContSubMatterService;
            _IMapper = iMapper;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getSubmatterList")]
        [HttpGet]

        public IActionResult GetPlanFinceList([FromQuery] PageParams pageParams, [FromQuery] DevComFileSearch serachParam)

        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new NoPageInfo<DEV_CONT_SUB_MATTER>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_CONT_SUB_MATTER>();

            whereexp = whereexp.And(a => a.IS_DELETE == 0);
            whereexp = whereexp.And(a => a.CONT_ID == -userId || a.CONT_ID == serachParam.CustId);
            Expression<Func<DEV_CONT_SUB_MATTER, object>> orderbyLambda = a => a.ID;
            var data = _IDevContSubMatterService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevContSubMatterList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="menuSaveDTO">保存对象</param>
        /// <returns></returns>
        [Route("submatterSave")]
        [HttpPost]
        [Authorize]
        public IActionResult SubmatterSave([FromBody] DevContSubMatterDTO devContSubMatterDTO)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();

            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            var info = _IMapper.Map<DEV_CONT_SUB_MATTER>(devContSubMatterDTO);
            if (devContSubMatterDTO.ID > 0)
            {//修改
                var userinfo = _IDevContSubMatterService.InSingle(devContSubMatterDTO.ID);
                var saveinfo = _IMapper.Map<DevContSubMatterDTO, DEV_CONT_SUB_MATTER>(devContSubMatterDTO);
                saveinfo.UPDATE_TIME = DateTime.Now;
                saveinfo.UPDATE_USERID = userId;
                saveinfo.IS_DELETE = 0;
                saveinfo.CODE = "001";
                saveinfo.IS_FROM = 0;
                saveinfo.COMP_AMOUNT = 0;
                saveinfo.SUB_STATE = 0;
                saveinfo.ORDER_NUM = 0;
                _IDevContSubMatterService.Update(saveinfo);
            }
            else
            {
                info.UPDATE_TIME = DateTime.Now;
                info.UPDATE_USERID = userId;
                info.IS_DELETE = 0;
                info.CODE = "001";
                info.IS_FROM = 0;
                info.COMP_AMOUNT = 0;
                info.SUB_STATE = 0;
                info.ORDER_NUM = 0;
                info.CREATE_TIME = DateTime.Now;
                info.CREATE_USERID = userId;
                info.CONT_ID = devContSubMatterDTO.CONT_ID <= 0 ? -userId : devContSubMatterDTO.CONT_ID;
                _IDevContSubMatterService.Add(info);
            }


            return new DevResponseJson(result);

        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Ids">选中ID</param>
        [Route("submatterDel")]
        [HttpGet]
        public IActionResult SubmatterDel(string Ids)
        {
            var arrIds = StringHelper.String2ArrayInt(Ids);
            _IDevContSubMatterService.Delete(a => arrIds.Contains(a.ID));
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);
        }
    }
}
