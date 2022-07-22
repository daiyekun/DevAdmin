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
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.FilterExtend;
using WooDev.WebCommon.Utiltiy;

namespace WooDev.WebApi.Controllers.Flow
{


    /// <summary>
    /// 审批组
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DevFlowTempGroupController : ControllerBase
    {
        private IDevFlowGroupService _IDevFlowGroupService;
        private IMapper _IMapper;

        public DevFlowTempGroupController(IMapper IMapper, IDevFlowGroupService iDevFlowGroupService
           )
        {
            _IMapper = IMapper;
            _IDevFlowGroupService = iDevFlowGroupService;
            

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
            var pageinfo = new PageInfo<DEV_FLOW_GROUP>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_FLOW_GROUP>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);

            if (!string.IsNullOrEmpty(serachParam.KeyWord))
            {//搜索
                whereexp = whereexp.And(a => a.NAME.Contains(serachParam.KeyWord) || a.CODE.Contains(serachParam.KeyWord));
            }
            Expression<Func<DEV_FLOW_GROUP, object>> orderbyLambda = a => a.ID;
            var data = _IDevFlowGroupService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevFlowGroupList>
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
        [DevOptionLogActionFilter("新增审批组", OptionLogEnum.Add, "新增审批组", true)]
        [Route("flowGroupSave")]
        [HttpPost]
        public IActionResult FlowGroupSave([FromBody] DevFlowGroupDTO roleDTO)
        {
            //var userId = HttpContext.User.Claims.GetTokenUserId();
            //var rolenum = new RoleMenuDTO();
            //rolenum.MenuIds = roleDTO.menu;
            //rolenum.UserId = userId;
            //rolenum.RoleId = roleDTO.ID;
            //if (roleDTO.ID > 0)
            //{
            //    var deprinfo = _IDevFlowGroupService.InSingle(roleDTO.ID);
            //    var saveinfo = _IMapper.Map<DevRoleDTO, DEV_FLOW_GROUP>(roleDTO);
            //    saveinfo.UPDATE_TIME = DateTime.Now;
            //    saveinfo.UPDATE_USERID = userId;

            //    _IDevFlowGroupService.Update(saveinfo);
               

            //}
            //else
            //{
            //    var info = _IMapper.Map<DEV_FLOW_GROUP>(roleDTO);
            //    info.CREATE_TIME = DateTime.Now;
            //    info.UPDATE_TIME = DateTime.Now;
            //    info.CREATE_USERID = userId;
            //    info.UPDATE_USERID = userId;
            //    var saveinfo = _IDevFlowGroupService.Add(info);
            //    rolenum.RoleId = saveinfo.ID;
            //    _IDevRoleFunctionService.SaveRoleMenus(rolenum);
            //}

            //_IDevFlowGroupService.SetRedisHash();
            //RedisUtility.KeyDeleteAsync($"{RedisKeys.DataDicALLListKey}");
           
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
        [Route("delFlowGroup")]
        [HttpGet]
        [Authorize]
        public IActionResult DelFlowGroup(string Ids)
        {
            var arrIds = StringHelper.String2ArrayInt(Ids);
            _IDevFlowGroupService.Delete(a => arrIds.Contains(a.ID));
            RedisUtility.KeyDeleteAsync($"{RedisKeys.FlowGroupAllListKey}");
            _IDevFlowGroupService.SetRedisHash();

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
            var data = _IDevFlowGroupService.GetAll().Where(a => a.G_STATE == 0).ToList();//只要启用的
            var result = new ResultListData<DevFlowGroupDTO>
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
        [Route("setFlowGroupStatus")]
        [HttpPost]
        public IActionResult SetFlowGroupStatus(DevStatusInfo roleStatus)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            //if (roleStatus.id > 0)
            //{
            //    var deprinfo = _IDevFlowGroupService.InSingle(roleStatus.id);
            //    deprinfo.RUSTATE = roleStatus.status;
            //    deprinfo.UPDATE_TIME = DateTime.Now;
            //    deprinfo.UPDATE_USERID = userId;
            //    _IDevFlowGroupService.Update(deprinfo);

            //}
            //RedisUtility.KeyDeleteAsync($"{RedisKeys.RoleAllListKey}");
            //_IDevFlowGroupService.SetRedisHash();

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
        [Route("getAllFlowGroup")]
        [HttpGet]
        //[AllowAnonymous]//跳过授权验证
        [Authorize]
        public IActionResult GetAllFlowGroup([FromQuery] BaseSearch serachParam)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new NoPageInfo<DEV_FLOW_GROUP>() { };
            var whereexp = Expressionable.Create<DEV_FLOW_GROUP>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);

            if (!string.IsNullOrEmpty(serachParam.KeyWord))
            {//搜索
                whereexp = whereexp.And(a => a.NAME.Contains(serachParam.KeyWord) || a.CODE.Contains(serachParam.KeyWord));
            }
            Expression<Func<DEV_FLOW_GROUP, object>> orderbyLambda = a => a.ID;
            var data = _IDevFlowGroupService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevFlowGroupList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }
    }
}
