using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Linq.Expressions;
using WooDev.Common.Models;
using WooDev.IServices;
using WooDev.Model.Models;
using WooDev.ViewModel;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.Utiltiy;

namespace WooDev.WebApi.Controllers.Common
{

    /// <summary>
    /// 菜单操作
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DevMenuController : ControllerBase
    {
        private IDevFunctionMenuService _IDevFunctionMenuService;
        private IDevRoutemetaService _IDevRoutemetaService;
        public DevMenuController(IDevFunctionMenuService iDevFunctionMenuService, IDevRoutemetaService iDevRoutemetaService)
        {
            _IDevFunctionMenuService = iDevFunctionMenuService;
            _IDevRoutemetaService = iDevRoutemetaService;

        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getMenuList")]
        [HttpGet]
        //[AllowAnonymous]//跳过授权验证
        [Authorize]
        public IActionResult GetMenuList()
        {

            var userId = HttpContext.User.Claims.GetTokenUserId();

            var whereexp = Expressionable.Create<DEV_FUNCTION_MENU>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0 && a.IS_SHOW == 1);
            Expression<Func<DEV_FUNCTION_MENU, object>> orderbyLambda = a => a.ID;
            var data = _IDevFunctionMenuService.GetMenus(whereexp.ToExpression());
            var result = new ResultListData<MenuItem>
            {
                result = data,
            };
            return new DevResultJson(result, keyL: true);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getRoleMenuList")]
        [HttpGet]
        //[AllowAnonymous]//跳过授权验证
        [Authorize]
        public IActionResult GetRoleMenuList()
        {

            var userId = HttpContext.User.Claims.GetTokenUserId();

            var whereexp = Expressionable.Create<DEV_FUNCTION_MENU>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0 && a.IS_SHOW == 1&&(a.M_TYPE==0||a.M_TYPE==1));
            Expression<Func<DEV_FUNCTION_MENU, object>> orderbyLambda = a => a.ID;
            var data = _IDevFunctionMenuService.GetMenus(whereexp.ToExpression());
            var result = new ResultListData<MenuItem>
            {
                result = data,
            };
            return new DevResultJson(result, keyL: true);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getMenuListTable")]
        [HttpGet]
        //[AllowAnonymous]//跳过授权验证
        [Authorize]
        public IActionResult GetMenuListTable([FromQuery] SearMenu searMenu)
        {

            var userId = HttpContext.User.Claims.GetTokenUserId();

            var whereexp = Expressionable.Create<DEV_FUNCTION_MENU>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);
            if (!string.IsNullOrEmpty(searMenu.menuName))
            {//搜索名称
                whereexp = whereexp.And(a => a.NAME.Contains(searMenu.menuName) || (a.RouteMate != null && a.RouteMate.TITLE.Contains(searMenu.menuName)));
            }
            else if (searMenu.status > -1)
            {
                whereexp = whereexp.And(a => a.IS_SHOW == searMenu.status);
            }
            Expression<Func<DEV_FUNCTION_MENU, object>> orderbyLambda = a => a.ID;
            var data = _IDevFunctionMenuService.GetList(whereexp.ToExpression());
            var result = new ResultListData<MenuList>
            {
                result = data,
            };
            return new DevResponseJson(result);
        }

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="menuSaveDTO">菜单对象</param>
        /// <returns></returns>
        [Route("menuSave")]
        [HttpPost]
        [Authorize]
        public IActionResult MenuSave([FromBody] MenuSaveDTO menuSaveDTO)
        {
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            var userId = HttpContext.User.Claims.GetTokenUserId();
            if (menuSaveDTO.id > 0)
            {//修改
                var info = _IDevFunctionMenuService.InSingle(menuSaveDTO.id);
                if (info.META_ID > 0)
                {
                    var routmate = _IDevRoutemetaService.InSingle(info.META_ID ?? 0);
                    var mateinfo = CreateMateInst(routmate, menuSaveDTO);
                    _IDevRoutemetaService.Update(routmate);
                    var tinfo = CreateMenuInst(info, menuSaveDTO);
                    _IDevFunctionMenuService.Update(tinfo);
                }

            }
            else
            {//新增
             //先创建mate
                DEV_ROUTEMETA routeMate = new DEV_ROUTEMETA();
                routeMate.CREATE_TIME = DateTime.Now;
                routeMate.CREATE_USERID = userId;
                routeMate.UPDATE_TIME = DateTime.Now;
                routeMate.UPDATE_USERID = userId;
                routeMate.IS_DELETE = 0;

                if (menuSaveDTO.type == 1)
                {//菜单
                    //隐藏子菜单-按钮，用于显示左侧树
                    routeMate.HIDE_CHILDRENINMENU = 1;
                }
                var tmate = CreateMateInst(routeMate, menuSaveDTO);
                var smate = _IDevRoutemetaService.Add(tmate);
                //然后保存菜单
                DEV_FUNCTION_MENU info = new DEV_FUNCTION_MENU();
                info.CREATE_TIME = DateTime.Now;
                info.CREATE_USERID = userId;
                info.UPDATE_TIME = DateTime.Now;
                info.UPDATE_USERID = userId;
                info.IS_DELETE = 0;
                info.META_ID = smate.ID;
                var tinfo = CreateMenuInst(info, menuSaveDTO);
                _IDevFunctionMenuService.Add(tinfo);



            }
            return new DevResponseJson(result);

        }
        /// <summary>
        /// 返回  DEV_FUNCTION_MENU 类型
        /// </summary>
        /// <param name="info">赋值实体</param>
        /// <param name="menuSaveDTO">表单值</param>
        private DEV_FUNCTION_MENU CreateMenuInst(DEV_FUNCTION_MENU info, MenuSaveDTO menuSaveDTO)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            //菜单主要信息
            info.IS_SHOW = menuSaveDTO.status;
            info.NAME = menuSaveDTO.name;//
            info.PATH = menuSaveDTO.routePath;
            info.PERMISSION = menuSaveDTO.permission;
            info.CODE = "0001";
            info.IS_DELETE = 0;
            info.UPDATE_TIME = DateTime.Now;
            info.UPDATE_USERID = userId;
            info.COMPONENT = menuSaveDTO.component;
            info.PID = menuSaveDTO.parentMenu;
            info.M_TYPE = menuSaveDTO.type;
            if (menuSaveDTO.type == 0)
            {
                info.COMPONENT = "LAYOUT";
            }
            return info;

        }
        /// <summary>
        /// 返回mate
        /// </summary>
        /// <param name="routmate"></param>
        /// <param name="menuSaveDTO"></param>
        /// <returns></returns>
        private DEV_ROUTEMETA CreateMateInst(DEV_ROUTEMETA routmate, MenuSaveDTO menuSaveDTO)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            routmate.ISLINK = menuSaveDTO.isExt;//是否外连接
            routmate.TITLE = menuSaveDTO.menuName;
            routmate.ICON = menuSaveDTO.icon;
            routmate.ORDERNO = menuSaveDTO.orderNo;
            routmate.HIDEMENU = menuSaveDTO.show;
            routmate.IGNORE_KEEPALIVE = menuSaveDTO.keepalive;//是否缓存
            routmate.UPDATE_TIME = DateTime.Now;
            routmate.IS_DELETE = 0;
            routmate.UPDATE_USERID = userId;
            return routmate;
        }





    }
}
