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
            var data= _IDevDatadicService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, true);
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
            DEV_DATADIC devdic = _IDevDatadicService.InSingle(devDatadicDTO.ID);
            devdic.NAME = devDatadicDTO.NAME;
            devdic.SORT_NAME = devDatadicDTO.SORT_NAME;
            devdic.REMARK = devDatadicDTO.REMARK;
            devdic.ORDER_NUM = devDatadicDTO.ORDER_NUM;
            _IDevDatadicService.Update(devdic);
            // _IDevDatadicService.Add();

            var result = new ResultData();


            return new DevResultJson(result);


        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="datadicDTO">新增对象</param>
        [Route("datadicAdd")]
        [HttpGet]
        [Authorize]
        public IActionResult AddDataDic(int TypeInt)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var info = new DEV_DATADIC();
            info.NAME = $"新增类别-{DateTime.Now.Ticks}";
            info.APP_TYPE = TypeInt;
            info.CREATE_TIME = DateTime.Now;
            info.CREATE_USERID = userId;
            info.UPDATE_TIME= DateTime.Now;
            info.UPDATE_USERID = userId;
            info.IS_APP = 1;
            info.IS_DELETE = 0;

            _IDevDatadicService.Add(info);
            _IDevDatadicService.SetRedisHash();
            RedisUtility.KeyDeleteAsync($"{RedisKeys.DataDicALLListKey}");
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Ids">选中ID</param>
        [Route("deleteDic")]
        [HttpGet]
        [Authorize]
        public IActionResult DeleteDic(string Ids)
        {
            var arrIds=StringHelper.String2ArrayInt(Ids);
            _IDevDatadicService.Delete(a => arrIds.Contains(a.ID));
            _IDevDatadicService.SetRedisHash();
            RedisUtility.KeyDeleteAsync($"{RedisKeys.DataDicALLListKey}");
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);
        }
        /// <summary>
        /// 字典列表
        /// </summary>
        /// <returns></returns>
        [Route("getdiclist")]
        [HttpGet]
        [Authorize]
        public IActionResult GetDataList([FromQuery] SerachParam serachParam)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();

            var whereexp = Expressionable.Create<DEV_DATADIC>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);
            whereexp = whereexp.And(a => a.APP_TYPE == serachParam.LbId);
            if (!string.IsNullOrEmpty(serachParam.Name))
            {//搜索名称
                whereexp = whereexp.And(a => a.NAME.Contains(serachParam.Name));
            }
            Expression<Func<DEV_DATADIC, object>> orderbyLambda = a => a.ORDER_NUM;
            var data = _IDevDatadicService.GetDataList(whereexp.ToExpression(), orderbyLambda, true);
            var result = new ResultListData<DevDatadicList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getTreeList")]
        [HttpGet]
        //[AllowAnonymous]//跳过授权验证
        [Authorize]
        public IActionResult GetTreeList(int dtype)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            
            var whereexp = Expressionable.Create<DEV_DATADIC>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);
            whereexp = whereexp.And(a => a.APP_TYPE == dtype);

            Expression<Func<DEV_DATADIC, object>> orderbyLambda = a => a.ID;
            var data = _IDevDatadicService.GetDicTree(whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultListData<DataDicTree>
            {
                result = data,
            };

            return new DevResultJson(result);
        }




    }
}
