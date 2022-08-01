using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WooDev.Common.Models;
using WooDev.Common.Utility;
using WooDev.ViewModel.Enums;
using WooDev.ViewModel.ExtendModel;
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
    }
}
