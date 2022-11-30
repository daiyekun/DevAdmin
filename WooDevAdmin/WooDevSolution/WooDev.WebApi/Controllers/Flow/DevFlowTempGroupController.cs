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
        private IDevFlowGroupuserService _IDevFlowGroupuserService;
        private IDevUserService _IDevUserService;

        public DevFlowTempGroupController(IMapper IMapper, IDevFlowGroupService iDevFlowGroupService
           , IDevFlowGroupuserService iDevFlowGroupuserService, IDevUserService iDevUserService)
        {
            _IMapper = IMapper;
            _IDevFlowGroupService = iDevFlowGroupService;
            _IDevFlowGroupuserService = iDevFlowGroupuserService;
            _IDevUserService = iDevUserService;



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
            if (serachParam.SelecType==1)
            {
                //启用的数据
                whereexp = whereexp.And(a => a.G_STATE == 0);
            }

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
        public IActionResult FlowGroupSave([FromBody] DevFlowGroupDTO flowGroupDTO)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var groupUserDto = new DevFlowGroupuserDTO();
            groupUserDto.GROUP_ID = flowGroupDTO.ID;
            groupUserDto.UserIds = StringHelper.String2ArrayInt(flowGroupDTO.UserIds);
            if (flowGroupDTO.ID > 0)
            {
                var groupinfo = _IDevFlowGroupService.InSingle(flowGroupDTO.ID);
                var saveinfo = _IMapper.Map<DevFlowGroupDTO, DEV_FLOW_GROUP>(flowGroupDTO, groupinfo);
                saveinfo.UPDATE_TIME = DateTime.Now;
                saveinfo.UPDATE_USERID = userId;
                _IDevFlowGroupService.Update(saveinfo);
                _IDevFlowGroupuserService.SaveGroupUsers(groupUserDto);

            }
            else
            {
                var info = _IMapper.Map<DEV_FLOW_GROUP>(flowGroupDTO);
                info.CREATE_TIME = DateTime.Now;
                info.UPDATE_TIME = DateTime.Now;
                info.CREATE_USERID = userId;
                info.UPDATE_USERID = userId;
                var saveinfo = _IDevFlowGroupService.Add(info);
                groupUserDto.GROUP_ID = saveinfo.ID;
                _IDevFlowGroupuserService.SaveGroupUsers(groupUserDto);
            }

            _IDevFlowGroupService.SetRedisHash();
            RedisUtility.KeyDeleteAsync($"{RedisKeys.FlowGroupAllListKey}");

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
            _IDevFlowGroupuserService.Delete(a => arrIds.Contains(a.GROUP_ID));
            RedisUtility.KeyDeleteAsync($"{RedisKeys.FlowGroupAllListKey}");
            _IDevFlowGroupService.SetRedisHash();

            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);
        }
        [Route("getAllFlowGroupList")]
        [HttpGet]
        [Authorize]
        public IActionResult GetAllFlowGroupList()
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
        [DevOptionLogActionFilter("修改组状态", OptionLogEnum.Update, "修改组状态", true)]
        [Route("setFlowGroupStatus")]
        [HttpPost]
        public IActionResult SetFlowGroupStatus(DevStatusInfo roleStatus)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            if (roleStatus.id > 0)
            {
                var deprinfo = _IDevFlowGroupService.InSingle(roleStatus.id);
                deprinfo.G_STATE = roleStatus.status;
                deprinfo.UPDATE_TIME = DateTime.Now;
                deprinfo.UPDATE_USERID = userId;
                _IDevFlowGroupService.Update(deprinfo);
;
            }
            RedisUtility.KeyDeleteAsync($"{RedisKeys.RoleAllListKey}");
            _IDevFlowGroupService.SetRedisHash();

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

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getUserList")]
        [HttpGet]
        [Authorize]
        public IActionResult GetList([FromQuery] PageParams pageParams, [FromQuery] DevUserSearch serachParam)
        {
            var userIds=_IDevFlowGroupuserService.Query(a => a.GROUP_ID == serachParam.GroupId).Select(a => a.USER_ID).ToList();
           
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new PageInfo<DEV_USER>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_USER>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);
            whereexp = whereexp.And(a => userIds.Contains(a.ID));//过滤组用户
            if (!string.IsNullOrEmpty(serachParam.NAME))
            {//搜索名称
                whereexp = whereexp.And(a => a.NAME.Contains(serachParam.NAME));
            }
            if (!string.IsNullOrEmpty(serachParam.LOGIN_NAME))
            {//搜索名称
                whereexp = whereexp.And(a => a.LOGIN_NAME.Contains(serachParam.LOGIN_NAME));
            }
            if (serachParam.deptId > 0)
            {//部门ID
                whereexp = whereexp.And(a => a.DEPART_ID == serachParam.deptId);
            }
            Expression<Func<DEV_USER, object>> orderbyLambda = a => a.ID;
            var data = _IDevUserService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevUserList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }

        
    }
}
