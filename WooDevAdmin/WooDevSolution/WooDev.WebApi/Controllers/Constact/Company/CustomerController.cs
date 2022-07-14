using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
using WooDev.ViewModel.ExtendModel;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.FilterExtend;
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
        private IDevRolePermissionService _IDevRolePermissionService;
        public CustomerController(IDevCompanyService iDevCompanyService, IMapper iMapper, IDevCompFileService iDevCompFileService
            , IDevRolePermissionService iDevRolePermissionService)
        {
            _IDevCompanyService = iDevCompanyService;
            _IMapper = iMapper;
            _IDevCompFileService = iDevCompFileService;
            _IDevRolePermissionService = iDevRolePermissionService;
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
            var pageinfo = new PageInfo<DEV_COMPANY>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = GetFilterExpress(serachParam);
            Expression<Func<DEV_COMPANY, object>> orderbyLambda = a => a.ID;
            var data = _IDevCompanyService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevCompanyList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 获取查询条件
        /// 
        /// </summary>
        /// <param name="serachParam"></param>
        /// <returns></returns>
        private Expressionable<DEV_COMPANY> GetFilterExpress(DevCompanySearch serachParam)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var deptId = HttpContext.User.Claims.GetTokenDeptId();
            var roleId = HttpContext.User.Claims.GetTokenRoleId();
            //var predicateAnd = PredicateBuilder.True<DEV_COMPANY>();
            var whereexp = Expressionable.Create<DEV_COMPANY>();
           
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
            var pession = _IDevRolePermissionService.GetCompanyListPermissionExpression(whereexp,"customerlist", userId, deptId, roleId);
            //whereexp = whereexp.And(pession);
            //predicateAnd = predicateAnd.And(predicateAnd.ToExpression());
            pession = pession.And(a => a.C_TYPE == 0);//客户
            pession = pession.And(a => a.IS_DELETE == 0);
            return pession;

        }

        /// <summary>
        /// 新增，修改保存
        /// </summary>
        /// <param name="roleDTO">角色对象</param>
        /// <returns></returns>
        [DevOptionLogActionFilter("新建客户或者修改客户", OptionLogEnum.UpdateOrAdd, "新建客户或者修改客户", true)]
        [Route("customerSave")]
        [HttpPost]
        public IActionResult CustomerSave([FromBody] DevCompanyDTO devCompanyDTO)
        {

            var userId = HttpContext.User.Claims.GetTokenUserId();
           
          //  devCompanyDTO.CREATE_ID = userId;
            
            _IDevCompanyService.CompanySave(devCompanyDTO, userId);


            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);


        }

        /// <summary>
        /// 清理数据
        /// </summary>
        /// <param name="Ids">选中ID</param>
        [Route("customerClear")]
        [HttpGet]
        public IActionResult CustomerClear()
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            _IDevCompanyService.ClearData(userId);
            return new DevResultJson(result);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Ids">选中ID</param>
        [DevOptionLogActionFilter("删除客户", OptionLogEnum.Del, "删除客户", true)]
        [Route("customerDel")]
        [HttpGet]
        public IActionResult CustomerDel(string ids)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            _IDevCompanyService.SoftDelete(ids);
            return new DevResultJson(result);
        }

        /// <summary>
        /// 查询详情
        /// </summary>
        /// <param name="id">当前ID</param>
        /// <returns></returns>
        [Route("customerView")]
        [HttpGet]
        public IActionResult CustomerView(int id)
        {


            var data = _IDevCompanyService.ShowDetail(id);

            var result = new ResultViewData<DevCompanyView>
            {
                code = 0,
                message = "ok",
                result = data
            };
            return new DevResultJson(result);


        }



    }
}
