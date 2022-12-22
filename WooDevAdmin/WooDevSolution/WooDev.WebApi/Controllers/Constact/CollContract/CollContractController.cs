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
using WooDev.Services;
using WooDev.ViewModel;
using WooDev.ViewModel.Contract.Contract;
using WooDev.ViewModel.Contract.ExcelModel;
using WooDev.ViewModel.Enums;
using WooDev.ViewModel.ExtendModel;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.FilterExtend;
using WooDev.WebCommon.Utiltiy;

namespace WooDev.WebApi.Controllers.Constact.CollContract
{

    /// <summary>
    /// 收款合同
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("default")]
    [Authorize]
    public class CollContractController : ControllerBase
    {
        private IDevContractService _IDevContractService;
        private IDevRolePermissionService _IDevRolePermissionService;
        public CollContractController(IDevContractService iDevContractService, IDevRolePermissionService iDevRolePermissionService)
        {
            _IDevContractService = iDevContractService;
            _IDevRolePermissionService= iDevRolePermissionService;
        }


        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getContractlist")]
        [HttpGet]
        [Authorize]
        public IActionResult GetList([FromQuery] PageParams pageParams, [FromQuery] DevContractSearch serachParam)
        {
            var pageinfo = new PageInfo<DEV_CONTRACT>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = GetFilterExpress(serachParam);
            Expression<Func<DEV_CONTRACT, object>> orderbyLambda = a => a.ID;
            var data = _IDevContractService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevContractList>
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
        private Expressionable<DEV_CONTRACT> GetFilterExpress(DevContractSearch serachParam)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var deptId = HttpContext.User.Claims.GetTokenDeptId();
            var roleId = HttpContext.User.Claims.GetTokenRoleId();
            //var predicateAnd = PredicateBuilder.True<DEV_COMPANY>();
            var whereexp = Expressionable.Create<DEV_CONTRACT>();

            if (!string.IsNullOrEmpty(serachParam.Name))
            {//搜索名称
                whereexp = whereexp.And(a => a.C_NAME.Contains(serachParam.Name));
            }
            if (!string.IsNullOrEmpty(serachParam.Code))
            {//搜索编号
                whereexp = whereexp.And(a => a.C_CODE.Contains(serachParam.Code));
            }
            if (serachParam.CATE_ID > 0)
            {//类别ID
                whereexp = whereexp.And(a => a.CATE_ID == serachParam.CATE_ID);
            }
            if (serachParam.IS_FRAMEWORK>=0)
            {//类别ID
                whereexp = whereexp.And(a => a.IS_FRAMEWORK == serachParam.IS_FRAMEWORK);
            }
            if (!string.IsNullOrEmpty(serachParam.SIGNING_DATE))
            {//签订日期
               var singarr= StringHelper.Strint2ArrayString(serachParam.SIGNING_DATE, "-");
                whereexp = whereexp.And(a => a.SIGNING_DATE>=Convert.ToDateTime( singarr[0]) && a.SIGNING_DATE<= Convert.ToDateTime(singarr[1]));
            }
            if (!string.IsNullOrEmpty(serachParam.EFFECTIVE_DATE))
            {//生效日期
                var singarr = StringHelper.Strint2ArrayString(serachParam.SIGNING_DATE, "-");
                whereexp = whereexp.And(a => a.EFFECTIVE_DATE >= Convert.ToDateTime(singarr[0]) && a.EFFECTIVE_DATE <= Convert.ToDateTime(singarr[1]));
            }
            var pession = _IDevRolePermissionService.GetContractListPermissionExpression(whereexp, "collcontractlist", userId, deptId, roleId);
            //whereexp = whereexp.And(pession);
            //predicateAnd = predicateAnd.And(predicateAnd.ToExpression());
            pession = pession.And(a => a.F_TYPE == 0);//收款合同
            pession = pession.And(a => a.IS_DELETE == 0);
            return pession;

        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Ids">选中ID</param>
        [DevOptionLogActionFilter("删除收款合同", OptionLogEnum.Del, "删除收款合同", true)]
        [Route("constractDel")]
        [HttpGet]
        public IActionResult ConstractDel(string ids)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            _IDevContractService.SoftDelete(ids);
            return new DevResultJson(result);
        }

        /// <summary>
        /// 新增，修改保存
        /// </summary>
        /// <param name="roleDTO">角色对象</param>
        /// <returns></returns>
        [DevOptionLogActionFilter("新建收款合同或者修改收款合同", OptionLogEnum.UpdateOrAdd, "新建收款合同或者修改收款合同", true)]
        [Route("constractSave")]
        [HttpPost]
        public IActionResult ConstractSave([FromBody] DevContractDTO devContractDTO)
        {

            var userId = HttpContext.User.Claims.GetTokenUserId();

            //  devCompanyDTO.CREATE_ID = userId;
            devContractDTO.F_TYPE = 0;
            _IDevContractService.ConstractSave(devContractDTO, userId);


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
        [Route("constractClear")]
        [HttpGet]
        public IActionResult ConstractClear()
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            _IDevContractService.ClearData(userId);
            return new DevResultJson(result);
        }


        /// <summary>
        /// 查询详情
        /// </summary>
        /// <param name="id">当前ID</param>
        /// <returns></returns>
        [Route("constractView")]
        [HttpGet]
        public IActionResult ConstractView(int id)
        {


            var data = _IDevContractService.ShowDetail(id);

            var result = new ResultViewData<DevContractView>
            {
                code = 0,
                message = "ok",
                result = data
            };
            return new DevResultJson(result);


        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        [Route("exportexcel")]
        [HttpPost]
        public IActionResult ExportExcel([FromBody] ConstractExcel Seldata)
        {

            var pageInfo = new NoPageInfo<DEV_CONTRACT>();
            
            var whereexp = GetFilterExpress(Seldata.SearData);
            if (Seldata.Seldata.Srow == 1)
            {//选择行
                whereexp = whereexp.And(p => Seldata.GetSelectListIds().Contains(p.ID));
            }
            var layPage = _IDevContractService.GetList(pageInfo, whereexp.ToExpression(), a => a.ID, true);
            var downInfo = DevExportDataHelper.ExportExcelExtend(Seldata, "收款合同列表", layPage.items);


            var excelfile = new ExportFileInfo
            {
                FileName = downInfo.FileName,
                Memi = downInfo.Memi,
                FilePath = $"{EmunUtility.GetDesc(typeof(DevFolderEnums), 10)}/{downInfo.FileName}",
                // DowIp = _Configuration["DevAppSeting:filedownIp"]


            };

            var result = new ResultObjData<ExportFileInfo>
            {
                result = excelfile
            };
            return new DevResultJson(result);


        }

        /// <summary>
        /// 新增，修改保存
        /// </summary>
        /// <param name="roleDTO">角色对象</param>
        /// <returns></returns>
        [DevOptionLogActionFilter("修改收款合同状态", OptionLogEnum.UpdateOrAdd, "修改收款合同状态", true)]
        [Route("updateState")]
        [HttpPost]
        public IActionResult UpdateState([FromBody] UpdateStateDTO updateState)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            _IDevContractService.UpdateState(updateState, userId);
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);


        }
    }
}
