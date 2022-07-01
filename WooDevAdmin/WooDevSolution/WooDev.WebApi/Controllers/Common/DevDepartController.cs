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

namespace WooDev.WebApi.Controllers.Common
{

    /// <summary>
    /// 部门
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("default")]
    public class DevDepartController : ControllerBase
    {
        private IMapper _IMapper;
        private IDevDepartmentService _IDevDepartmentService;
        public DevDepartController(IMapper IMapper,IDevDepartmentService IDevDepartmentService)
        {
            _IMapper = IMapper;
            _IDevDepartmentService = IDevDepartmentService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getList")]
        [HttpGet]
        //[AllowAnonymous]//跳过授权验证
        [Authorize]
        public IActionResult GetList([FromQuery] PageParams pageParams, [FromQuery] SerachDepartData serachParam)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new PageInfo<DEV_DEPARTMENT>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_DEPARTMENT>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);
           
            if (!string.IsNullOrEmpty(pageParams.keyword))
            {//搜索名称
                whereexp = whereexp.And(a => a.NAME.Contains(pageParams.keyword));
            }
            Expression<Func<DEV_DEPARTMENT, object>> orderbyLambda = a => a.ORDER_NUM;
            var data = _IDevDepartmentService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, true);
            var result = new ResultData<DevDepartmentList>
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
        public IActionResult GetTreeList([FromQuery] PageParams pageParams, [FromQuery] SerachDepartData serachParam)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new NoPageInfo<DEV_DEPARTMENT>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_DEPARTMENT>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);

            if (serachParam!=null&&!string.IsNullOrEmpty(serachParam.depName))
            {//搜索名称
                whereexp = whereexp.And(a => a.NAME.Contains(serachParam.depName));
            }
            Expression<Func<DEV_DEPARTMENT, object>> orderbyLambda = a => a.ORDER_NUM;
            var data = _IDevDepartmentService.GetTableTree(pageinfo, whereexp.ToExpression(), orderbyLambda, true);
            var result = new ResultListData<DeptTreeTable>
            {
                result = data,
            };
            
            return new DevResultJson(result);
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="deptInfo">部门对象</param>
        /// <param name="deptMain"></param>
        /// <returns></returns>
        [Route("departSave")]
        [HttpPost]
        public IActionResult AddSave([FromBody] DevDepartmentDTO devDepartmentDTO)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            if (devDepartmentDTO.ID > 0)
            {
                var deprinfo = _IDevDepartmentService.InSingle(devDepartmentDTO.ID);
                var saveinfo = _IMapper.Map<DevDepartmentDTO, DEV_DEPARTMENT>(devDepartmentDTO);
                saveinfo.UPDATE_TIME = DateTime.Now;
                saveinfo.UPDATE_USERID = userId;
                _IDevDepartmentService.Update(saveinfo);

            }
            else
            {
                var deptinfo = _IMapper.Map<DEV_DEPARTMENT>(devDepartmentDTO);
                deptinfo.CREATE_TIME = DateTime.Now;
                deptinfo.UPDATE_TIME = DateTime.Now;
                deptinfo.CREATE_USERID = userId;
                deptinfo.UPDATE_USERID = userId;
                _IDevDepartmentService.Add(deptinfo);
            }
            
            _IDevDepartmentService.SetRedisHash();
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
        [Route("deleteDepart")]
        [HttpGet]
        [Authorize]
        public IActionResult DeleteDepart(string Ids)
        {
            var arrIds = StringHelper.String2ArrayInt(Ids);
            _IDevDepartmentService.Delete(a => arrIds.Contains(a.ID));
            _IDevDepartmentService.SetRedisHash();
            RedisUtility.KeyDeleteAsync($"{RedisKeys.DataDicALLListKey}");
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);
        }


        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getDeptTree")]
        [HttpGet]
        //[AllowAnonymous]//跳过授权验证
        [Authorize]
        public IActionResult GetDeptTree([FromQuery] PageParams pageParams, [FromQuery] SerachDepartData serachParam)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new NoPageInfo<DEV_DEPARTMENT>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_DEPARTMENT>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);

            if (serachParam != null && !string.IsNullOrEmpty(serachParam.depName))
            {//搜索名称
                whereexp = whereexp.And(a => a.NAME.Contains(serachParam.depName));
            }
            Expression<Func<DEV_DEPARTMENT, object>> orderbyLambda = a => a.ORDER_NUM;
            List <DeptTree> data= _IDevDepartmentService.GetDeptTree(pageinfo, whereexp.ToExpression(), orderbyLambda, true);
            var result = new ResultListData<DeptTree>
            {
                result = data,
            };

            return new DevResultJson(data);
        }
    }
}
