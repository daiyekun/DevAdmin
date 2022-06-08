using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Linq.Expressions;
using WooDev.Common.Models;
using WooDev.IServices;
using WooDev.Model.Models;
using WooDev.ViewModel;
using WooDev.ViewModel.ExtendModel;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.Utiltiy;

namespace WooDev.WebApi.Controllers.Common
{

    /// <summary>
    /// 数据字典控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("default")]
    public class DevDataDicController : ControllerBase
    {
        private IDevDatadicService _IDevDatadicService;
        public DevDataDicController(IDevDatadicService iDevDatadicService)
        {
            _IDevDatadicService = iDevDatadicService;
        }

        /// <summary>
        /// 字典列表
        /// </summary>
        /// <returns></returns>
        [Route("getdatadicList")]
        [HttpGet]
        //[AllowAnonymous]//跳过授权验证
         [Authorize]
        public IActionResult GetDataDicList([FromQuery] PageParams pageParams,[FromQuery] SerachParam serachParam)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new PageInfo<DEV_DATADIC>() { PageIndex= pageParams.page,PageSize= pageParams .pageSize};
            var whereexp = Expressionable.Create<DEV_DATADIC>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);
            whereexp = whereexp.And(a => a.APP_TYPE == serachParam.LbId);
            if (!string.IsNullOrEmpty(serachParam.Name))
            {//搜索名称
                whereexp = whereexp.And(a =>a.NAME.Contains(serachParam.Name));
            }
            Expression<Func<DEV_DATADIC, object>> orderbyLambda = a =>a.ORDER_NUM ;
            var data= _IDevDatadicService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevDatadicList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 新增修改字典
        /// </summary>
        /// <param name="devDatadicDTO"></param>
        /// <returns></returns>

        [Route("datadicSave")]
        [HttpPost]
        [Authorize]
        public IActionResult DatadicSave(DevDatadicDTO devDatadicDTO)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            // _IDevDatadicService.Add();

            var result = new ResultData();


            return new DevResultJson(result);


        }

    }
}
