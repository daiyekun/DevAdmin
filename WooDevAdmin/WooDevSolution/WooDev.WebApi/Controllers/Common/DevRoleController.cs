using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Linq.Expressions;
using WooDev.Common.Extend;
using WooDev.Common.Models;
using WooDev.Common.Utility;
using WooDev.IServices;
using WooDev.Model.Models;
using WooDev.ViewModel;
using WooDev.ViewModel.Common;
using WooDev.ViewModel.ExtendModel;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.FilterExtend;
using WooDev.WebCommon.Utiltiy;

namespace WooDev.WebApi.Controllers.Common
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DevRoleController : ControllerBase
    {
        private IDevRoleService _IDevRoleService;
        private IMapper _IMapper;
        private IDevRoleFunctionService _IDevRoleFunctionService;
        public DevRoleController(IMapper IMapper, IDevRoleService IDevRoleService
            , IDevRoleFunctionService iDevRoleFunctionService)
        {
            _IMapper = IMapper;
            _IDevRoleService = IDevRoleService;
            _IDevRoleFunctionService = iDevRoleFunctionService;

        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getList")]
        [HttpGet]
        //[AllowAnonymous]//跳过授权验证
        [Authorize]
        public IActionResult GetList([FromQuery] PageParams pageParams, [FromQuery] BaseSearch serachParam)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new PageInfo<DEV_ROLE>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_ROLE>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);
           
            if (!string.IsNullOrEmpty(serachParam.KeyWord))
            {//搜索
                whereexp = whereexp.And(a => a.NAME.Contains(serachParam.KeyWord)|| a.CODE.Contains(serachParam.KeyWord));
            }
            Expression<Func<DEV_ROLE, object>> orderbyLambda = a => a.ID;
            var data = _IDevRoleService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevRoleList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 新增，修改保存
        /// </summary>
        /// <param name="roleDTO">角色对象</param>
        /// <returns></returns>
        [DevOptionLogActionFilter("新增角色", OptionLogEnum.Add, "新增角色", true)]
        [Route("roleSave")]
        [HttpPost]
        public IActionResult roleSave([FromBody] DevRoleDTO roleDTO)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var rolenum= new RoleMenuDTO();
            rolenum.MenuIds = roleDTO.menu;
            rolenum.UserId = userId;
            rolenum.RoleId = roleDTO.ID;
            if (roleDTO.ID > 0)
            {
                var deprinfo = _IDevRoleService.InSingle(roleDTO.ID);
                var saveinfo = _IMapper.Map<DevRoleDTO, DEV_ROLE>(roleDTO);
                saveinfo.UPDATE_TIME = DateTime.Now;
                saveinfo.UPDATE_USERID = userId;
               
                _IDevRoleService.Update(saveinfo);
                _IDevRoleFunctionService.SaveRoleMenus(rolenum);

            }
            else
            {
                var info = _IMapper.Map<DEV_ROLE>(roleDTO);
                info.CREATE_TIME = DateTime.Now;
                info.UPDATE_TIME = DateTime.Now;
                info.CREATE_USERID = userId;
                info.UPDATE_USERID = userId;
                var saveinfo=_IDevRoleService.Add(info);
                rolenum.RoleId = saveinfo.ID;
                _IDevRoleFunctionService.SaveRoleMenus(rolenum);
            }

            _IDevRoleService.SetRedisHash();
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
        [DevOptionLogActionFilter("删除角色", OptionLogEnum.Del, "删除角色", true)]
        [Route("delRole")]
        [HttpGet]
        [Authorize]
        public IActionResult DelRole(string Ids)
        {
            var arrIds = StringHelper.String2ArrayInt(Ids);
            _IDevRoleService.Delete(a => arrIds.Contains(a.ID));
             RedisUtility.KeyDeleteAsync($"{RedisKeys.RoleAllListKey}");
            _IDevRoleService.SetRedisHash();
           
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);
        }
        [Route("getAllRoleList")]
        [HttpGet]
        [Authorize]
        public IActionResult GetAllRoleList()
        {
           var data= _IDevRoleService.GetAll().Where(a=>a.RUSTATE==0).ToList();//只要启用的
            var result = new ResultListData<DevRoleDTO>
            {
                result = data,
            };
            return new DevResultJson(result);
        }


        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="roleDTO">角色对象</param>
        /// <returns></returns>
        [DevOptionLogActionFilter("修改角色状态", OptionLogEnum.Update, "修改角色状态", true)]
        [Route("setRoleStatus")]
        [HttpPost]
        public IActionResult SetRoleStatus(DevStatusInfo roleStatus)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            if (roleStatus.id > 0)
            {
                var deprinfo = _IDevRoleService.InSingle(roleStatus.id);
                deprinfo.RUSTATE = roleStatus.status;
                deprinfo.UPDATE_TIME = DateTime.Now;
                deprinfo.UPDATE_USERID = userId;
                _IDevRoleService.Update(deprinfo);

            }
            RedisUtility.KeyDeleteAsync($"{RedisKeys.RoleAllListKey}");
            _IDevRoleService.SetRedisHash();
           
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
        [Route("getAllRoles")]
        [HttpGet]
        //[AllowAnonymous]//跳过授权验证
        [Authorize]
        public IActionResult GetAllRoles([FromQuery] BaseSearch serachParam)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new NoPageInfo<DEV_ROLE>() {  };
            var whereexp = Expressionable.Create<DEV_ROLE>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);

            if (!string.IsNullOrEmpty(serachParam.KeyWord))
            {//搜索
                whereexp = whereexp.And(a => a.NAME.Contains(serachParam.KeyWord) || a.CODE.Contains(serachParam.KeyWord));
            }
            Expression<Func<DEV_ROLE, object>> orderbyLambda = a => a.ID;
            var data = _IDevRoleService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevRoleList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }



    }
}
