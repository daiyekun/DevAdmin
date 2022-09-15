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
using WooDev.ViewModel.Common;
using WooDev.ViewModel.Enums;
using WooDev.ViewModel.ExtendModel;
using WooDev.WebCommon.Extend;
using WooDev.WebCommon.FilterExtend;
using WooDev.WebCommon.Utiltiy;

namespace WooDev.WebApi.Controllers.Flow
{

    /// <summary>
    /// 流程模板
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("default")]
    [Authorize]
    public class DevFlowTempController : ControllerBase
    {
        private IDevFlowTempService _IDevFlowTempService;
        public DevFlowTempController(IDevFlowTempService iDevFlowTempService)
        {
            _IDevFlowTempService = iDevFlowTempService;
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
            var pageinfo = new PageInfo<DEV_FLOW_TEMP>() { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_FLOW_TEMP>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0);

            if (!string.IsNullOrEmpty(serachParam.KeyWord))
            {//搜索
                whereexp = whereexp.And(a => a.NAME.Contains(serachParam.KeyWord) || a.CODE.Contains(serachParam.KeyWord));
            }
            Expression<Func<DEV_FLOW_TEMP, object>> orderbyLambda = a => a.ID;
            var data = _IDevFlowTempService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevFlowTempList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }
        /// <summary>
        /// 获取流程对象集合
        /// </summary>
        /// <returns></returns>
        [Route("getFlowObjs")]
        [HttpGet]
        public IActionResult GetWfObjTypes()
        {
            var list = EmunUtility.GetAttr(typeof(FlowObjEnums));
            var result = new ResultListData<EnumItemAttribute>
            {
                result = list,
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 审批事项
        /// </summary>
        /// <returns></returns>
        [Route("getFlowItems")]
        [HttpGet]
        public IActionResult GetFlowItems(int objEnum)
        {

            var itemObjType = EmunUtility.GetEnumItemExAttribute(typeof(FlowObjEnums), objEnum);
            var list = EmunUtility.GetAttr(itemObjType.TypeValue);
            List<SelectMultiple> flowItems = new List<SelectMultiple>();
            foreach (var item in list)
            {
                SelectMultiple flow = new SelectMultiple();
                flow.Name = item.Desc;
                flow.Id = item.Value;
                flowItems.Add(flow);
            }
            var result = new ResultListData<SelectMultiple>
            {
                result = flowItems,
            };
            return new DevResultJson(result);

        }
        /// <summary>
        /// 新增，修改保存
        /// </summary>
        /// <param name="roleDTO">角色对象</param>
        /// <returns></returns>
        [DevOptionLogActionFilter("新增或者修改审批模板", OptionLogEnum.UpdateOrAdd, "新增或者修改审批模板", true)]
        [Route("flowTempSave")]
        [HttpPost]
        public IActionResult FlowTempSave([FromBody] DevFlowTempSaveInfo flowTempDTO)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();


            _IDevFlowTempService.SaveFlowTemp(flowTempDTO, userId);
            

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
        [DevOptionLogActionFilter("删除流程模板", OptionLogEnum.Del, "删除流程模板", true)]
        [Route("delFlowTemp")]
        [HttpGet]
        [Authorize]
        public IActionResult DelFlowTemp(string Ids)
        {
            var arrIds = StringHelper.String2ArrayInt(Ids);
            _IDevFlowTempService.Delete(a => arrIds.Contains(a.ID));

            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="status">状态对象</param>
        /// <returns></returns>
        [DevOptionLogActionFilter("修改流程模板状态", OptionLogEnum.Update, "修改流程模板状态", true)]
        [Route("setFlowTempStatus")]
        [HttpPost]
        public IActionResult SetFlowTempStatus(DevStatusInfo status)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            if (status.id > 0)
            {
                var deprinfo = _IDevFlowTempService.InSingle(status.id);
                deprinfo.F_STATE = status.status;
                deprinfo.UPDATE_TIME = DateTime.Now;
                deprinfo.UPDATE_USERID = userId;
                _IDevFlowTempService.Update(deprinfo);

            }
            

            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);


        }
        /// <summary>
        /// 查询详情
        /// </summary>
        /// <param name="id">当前ID</param>
        /// <returns></returns>
        [Route("flowTempView")]
        [HttpGet]
        public IActionResult FlowTempView(int id)
        {


            var data = _IDevFlowTempService.ShowDetail(id);

            var result = new ResultViewData<DevFlowTempView>
            {
                code = 0,
                message = "ok",
                result = data
            };
            return new DevResultJson(result);


        }




    }
}
