using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using SqlSugar;
using System.Linq.Expressions;
using WooDev.Common.Extend;
using WooDev.Common.Models;
using WooDev.Common.Utility;
using WooDev.IServices;
using WooDev.Model.Models;
using WooDev.Services;
using WooDev.ViewModel;
using WooDev.ViewModel.Common;
using WooDev.ViewModel.Enums;
using WooDev.ViewModel.ExtendModel;
using WooDev.ViewModel.Flow;
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
        private IDevFlowtempNodeService _IDevFlowtempNodeService;
        private IDevFlowtempEdgeService _IDevFlowtempEdgeService;
        private IDevFlowtempNodeInfoService _IDevFlowtempNodeInfoService;
        public DevFlowTempController(IDevFlowTempService iDevFlowTempService, 
            IDevFlowtempNodeService iDevFlowtempNodeService,
            IDevFlowtempEdgeService iDevFlowtempEdgeService,
            IDevFlowtempNodeInfoService iDevFlowtempNodeInfoService)
        {
            _IDevFlowTempService = iDevFlowTempService;
            _IDevFlowtempNodeService = iDevFlowtempNodeService;
            _IDevFlowtempEdgeService = iDevFlowtempEdgeService;
            _IDevFlowtempNodeInfoService = iDevFlowtempNodeInfoService;
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
            var list = EmunUtility.GetAttr(typeof(FlowObjEnums)).ToList();
            var tlist = list.Select(a => new EnumItemVben {Desc=a.Desc,Value=a.Value.ToString() }).ToList();
            var result = new ResultListData<EnumItemVben>
            {
                result = tlist,
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
            var list = EmunUtility.GetExtAttr(itemObjType.TypeValue);
            List<SelectMultiple> flowItems = new List<SelectMultiple>();
            foreach (var item in list)
            {
                SelectMultiple flow = new SelectMultiple();
                flow.Name = item.Desc;
                flow.Id = item.Value.ToString();
                flow.StartSta = item.StartSta;
                flow.EndSta = item.EndSta;
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

        #region 流程图操作相关

        /// <summary>
        /// 新增，修改保存
        /// </summary>
        /// <param name="roleDTO">角色对象</param>
        /// <returns></returns>
        [DevOptionLogActionFilter("流程图保存", OptionLogEnum.UpdateOrAdd, "流程图保存", true)]
        [Route("flowChartTempSave")]
        [HttpPost]
        public IActionResult FlowChartTempSave([FromBody] FlowChatData flowChartTempDTO)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var data= FlowTempUtility.GetFlowChartData(flowChartTempDTO, userId);
            _IDevFlowtempNodeService.Delete(a => a.TEMP_ID == flowChartTempDTO.TempId);
            _IDevFlowtempEdgeService.Delete(a => a.TEMP_ID == flowChartTempDTO.TempId);
            _IDevFlowtempNodeService.Add(data.FlowNodes);
            _IDevFlowtempEdgeService.Add(data.FlowEdges);
            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);


        }
        /// <summary>
        /// 根据模板ID获取流出图数据
        /// </summary>
        /// <param name="tempId">模板ID</param>
        /// <returns></returns>
        [Route("getFlowChartData")]
        [HttpGet]
        public IActionResult GetFlowChartData(int tempId)
        {
            var data = _IDevFlowTempService.GetFlowChart(tempId);
            DevFlowChartInfo flowchartdata = FlowTempUtility.GetFlowCharData(data);
            var resultdata = new ResultViewData<DevFlowChartInfo>
            {
                code = 0,
                message = "ok",
                result= flowchartdata

            };
            return new DevResultJson(resultdata);
        }


        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [Route("getNodeInfoByNodeId")]
        [HttpGet]
        //[AllowAnonymous]//跳过授权验证
        [Authorize]
        public IActionResult GetNodeInfoByNodeId([FromQuery] PageParams pageParams, [FromQuery] DevFlowInfoSearch serachParam)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageinfo = new NoPageInfo<DEV_FLOWTEMP_NODE_INFO> { PageIndex = pageParams.page, PageSize = pageParams.pageSize };
            var whereexp = Expressionable.Create<DEV_FLOWTEMP_NODE_INFO>();
            whereexp = whereexp.And(a => a.IS_DELETE == 0&&a.NODE_STRID== serachParam.NodeStr);
            Expression<Func<DEV_FLOWTEMP_NODE_INFO, object>> orderbyLambda = a => a.ID;
            var data = _IDevFlowtempNodeInfoService.GetList(pageinfo, whereexp.ToExpression(), orderbyLambda, false);
            var result = new ResultData<DevFlowTempNodeInfoList>
            {
                result = data,
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 新增，修改保存节点信息
        /// </summary>
        /// <param name="flowInfoDTO">节点信息</param>
        /// <returns></returns>
        [DevOptionLogActionFilter("节点信息修改", OptionLogEnum.UpdateOrAdd, "节点信息修改", true)]
        [Route("flowNodeInfoSave")]
        [HttpPost]
        public IActionResult FlowNodeInfoSave([FromBody] DevFlowInfoDTO flowInfoDTO)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            List<DEV_FLOWTEMP_NODE_INFO> listNodeInfos = new List<DEV_FLOWTEMP_NODE_INFO>();
            foreach (var objid in flowInfoDTO.SpObjIds)
            {
                var nodeInfo = new DEV_FLOWTEMP_NODE_INFO();
                nodeInfo.OPT_ID = objid;
                nodeInfo.TEMP_ID = flowInfoDTO.TEMP_ID;
                nodeInfo.NODE_STRID = flowInfoDTO.NODE_STRID;
                nodeInfo.O_TYPE = flowInfoDTO.O_TYPE;
                nodeInfo.NODE_ID = 0;
                nodeInfo.INFO_STATE = 0;
                nodeInfo.RE_TEXT = 0;
                nodeInfo.NRULE = 0;
                nodeInfo.IS_DELETE = 0;
                nodeInfo.CREATE_USERID = userId;
                nodeInfo.UPDATE_USERID = userId;
                nodeInfo.CREATE_TIME = DateTime.Now;
                nodeInfo.UPDATE_TIME = DateTime.Now;
                nodeInfo.OPT_NAME = "";
                listNodeInfos.Add(nodeInfo);


            }
            _IDevFlowtempNodeInfoService.Add(listNodeInfos);
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
        [DevOptionLogActionFilter("删除审批节点审批对象", OptionLogEnum.Del, "删除审批节点审批对象", true)]
        [Route("delFlowNodeInfoObj")]
        [HttpGet]
        [Authorize]
        public IActionResult DelFlowNodeInfoObj(string Ids)
        {
            var arrIds = StringHelper.String2ArrayInt(Ids);
            _IDevFlowtempNodeInfoService.Delete(a => arrIds.Contains(a.ID));

            var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 新增，修改保存节点信息
        /// </summary>
        /// <param name="flowInfoDTO">节点信息</param>
        /// <returns></returns>
        [DevOptionLogActionFilter("节点基本信息修改", OptionLogEnum.UpdateOrAdd, "节点基本信息修改", true)]
        [Route("flowNodeUpdate")]
        [HttpPost]
        public IActionResult FlowNodeUpdate([FromBody] DevFlowTempNodeDTO devFlowTempNode)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            _IDevFlowtempNodeService.UpdateNode(devFlowTempNode, userId);
             var result = new ResultData
            {
                code = 0,
                message = "ok",
            };
            return new DevResultJson(result);


        }

        /// <summary>
        /// 根据节点ID和模板ID判断节点是否保存
        /// </summary>
        /// <param name="strId">节点ID</param>
        /// <param name="tempId">模板ID</param>
        [Route("IsExistNode")]
        [HttpGet]
        [Authorize]
        public IActionResult IsExistNode([FromQuery] ExistNodeInfo existNode)
        {
            
           var IsExist=_IDevFlowtempNodeService.IsExistNode(existNode.StrId, existNode.TempId);

            var resdata = new ResultData
            {
                code = 0,
                message = "ok",
                result= IsExist
                //resdata = IsExist

            };
           
            return new DevResultJson(resdata);
        }






        #endregion




    }
}
