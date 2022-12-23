using Dev.WooNet.AutoMapper.Extend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Models;
using WooDev.Common.Utility;
using WooDev.Model.Models;
using WooDev.ViewModel;
using WooDev.ViewModel.Enums;

namespace WooDev.Services
{
    /// <summary>
    /// 流程模板
    /// </summary>
    public partial class DevFlowTempService
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where 条件</param>
        /// <param name="orderbyLambda">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public ResultPageData<DevFlowTempList> GetList(PageInfo<DEV_FLOW_TEMP> pageInfo, Expression<Func<DEV_FLOW_TEMP, bool>>? whereLambda,
            Expression<Func<DEV_FLOW_TEMP, object>> orderbyLambda, bool isAsc)
        {

            var tempquery = DbClient.Queryable<DEV_FLOW_TEMP>().Where(whereLambda);
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Asc);
            }
            else
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Desc);
            }

            int totalCount = 0;
            if ((pageInfo is NoPageInfo<DEV_FLOW_TEMP>))
            { //分页
                pageInfo.PageSize = 2000;
                pageInfo.PageIndex = 0;
            }
            var list = tempquery.ToPageList(pageInfo.PageIndex, pageInfo.PageSize, ref totalCount, a => new {
                ID = a.ID,
                NAME = a.NAME,//名称
                CODE = a.CODE,//编号
                F_STATE = a.F_STATE,//状态
                OBJ_TYPE=a.OBJ_TYPE,//审批对象
                CREATE_TIME = a.CREATE_TIME,//创建时间
                CREATE_USERID = a.CREATE_USERID,//创建人
                DEPART_IDS=a.DEPART_IDS,//部门
                CATE_IDS=a.CATE_IDS,//类别IDs
                FLOW_ITEMS=a.FLOW_ITEMS,//审批事项
                MAX_MONERY=a.MAX_MONERY,
                MIN_MONERY = a.MIN_MONERY,

            });
            pageInfo.TotalCount = totalCount;
            var local = from a in list
                        select new DevFlowTempList
                        {
                            ID = a.ID,
                            NAME = a.NAME,//名称
                            CODE = a.CODE,//编号
                            OBJ_TYPE = a.OBJ_TYPE,//审批对象
                            OBJ_TYPE_Str = a.OBJ_TYPE.ToString(),
                            F_STATE = a.F_STATE,//状态
                            CREATE_TIME = a.CREATE_TIME,//创建时间
                            CREATE_USERID = a.CREATE_USERID,//创建人
                            ObjTypeDic = EmunUtility.GetDesc(typeof(FlowObjEnums), a.OBJ_TYPE),
                            DEPART_IDS_LIST=StringHelper.String2ArrayInt(a.DEPART_IDS),
                            CATE_IDS_LIST= StringHelper.Strint2ArrayString1(a.CATE_IDS),//类别
                            FLOW_ITEMS_LIST=StringHelper.Strint2ArrayString1(a.FLOW_ITEMS),
                            MAX_MONERY = a.MAX_MONERY,
                            MIN_MONERY = a.MIN_MONERY,
                        };
            return new ResultPageData<DevFlowTempList>()
            {
                items = local.ToList(),
                total = pageInfo.TotalCount,
                page = pageInfo.PageIndex,
                pageSize = pageInfo.PageSize


            };
        }
        /// <summary>
        /// 保存流程模板
        /// </summary>
        /// <param name="devFlowTempDTO">保存模板对象</param>
        /// <param name="userId">当前登录人员ID</param>
        /// <returns></returns>
        public bool SaveFlowTemp(DevFlowTempSaveInfo devFlowTempDTO,int userId)
        {
            if (devFlowTempDTO.ID > 0)
            {
                var saveinfo = DbClient.Queryable<DEV_FLOW_TEMP>().Where(a => a.ID == devFlowTempDTO.ID).Single();
                AutoMapperHelper.Map<DevFlowTempSaveInfo, DEV_FLOW_TEMP>(devFlowTempDTO, saveinfo);
                saveinfo.UPDATE_USERID = userId;
                saveinfo.UPDATE_TIME = DateTime.Now;
                saveinfo.VERSION = saveinfo.VERSION+ 1;
                saveinfo.FLOW_ITEMS = devFlowTempDTO.FLOW_ITEMS_LIST;//  StringHelper.ArrayInt2String(devFlowTempDTO.FLOW_ITEMS_LIST);
                saveinfo.CATE_IDS = devFlowTempDTO.CATE_IDS_LIST;// StringHelper.ArrayInt2String(devFlowTempDTO.CATE_IDS_LIST);
                saveinfo.DEPART_IDS = devFlowTempDTO.DEPART_IDS_LIST;//  StringHelper.ArrayInt2String(devFlowTempDTO.DEPART_IDS_LIST);
                Update(saveinfo);
                CreateFlowTempHist(saveinfo, saveinfo.ID);

            }
            else
            {
                var info = AutoMapperHelper.Map<DevFlowTempSaveInfo, DEV_FLOW_TEMP>(devFlowTempDTO);
                info.CREATE_TIME = DateTime.Now;
                info.UPDATE_TIME = DateTime.Now;
                info.CREATE_USERID = userId;
                info.UPDATE_USERID = userId;
                info.FLOW_ITEMS = devFlowTempDTO.FLOW_ITEMS_LIST;//  StringHelper.ArrayInt2String(devFlowTempDTO.FLOW_ITEMS_LIST);
                info.CATE_IDS = devFlowTempDTO.CATE_IDS_LIST;// StringHelper.ArrayInt2String(devFlowTempDTO.CATE_IDS_LIST);
                info.DEPART_IDS = devFlowTempDTO.DEPART_IDS_LIST;//  StringHelp
                info.VERSION = 1;
                var currinfo = Add(info);
                CreateFlowTempHist(info, currinfo.ID);
            }
            return true;

        }
        /// <summary>
        /// 创建历史
        /// </summary>
        public void CreateFlowTempHist(DEV_FLOW_TEMP dEV_FLOW_TEMP,int flowId)
        {
            var saveHist = new DEV_FLOW_TEMP_HIST();
            AutoMapperHelper.Map<DEV_FLOW_TEMP,DEV_FLOW_TEMP_HIST>(dEV_FLOW_TEMP, saveHist);
            saveHist.TEMP_ID = flowId;
            saveHist.CREATE_USERID = dEV_FLOW_TEMP.UPDATE_USERID;
            saveHist.CREATE_TIME = DateTime.Now;
            saveHist.UPDATE_USERID = dEV_FLOW_TEMP.UPDATE_USERID;
            saveHist.UPDATE_TIME = DateTime.Now;
            DbClient.Insertable<DEV_FLOW_TEMP_HIST>(saveHist).ExecuteReturnEntity();
        }
        /// <summary>
        /// 查看详情
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns></returns>
        public DevFlowTempView ShowDetail(int Id)
        {
            var tempquery = DbClient.Queryable<DEV_FLOW_TEMP>().Where(a => a.ID == Id);
            var query = from a in tempquery
                        select new
                        {
                            ID = a.ID,
                            NAME = a.NAME,//名称
                            CODE = a.CODE,//编号
                            F_STATE = a.F_STATE,//状态
                            DEPART_IDS=a.DEPART_IDS,
                            FLOW_ITEMS=a.FLOW_ITEMS,
                            VERSION=a.VERSION,
                            OBJ_TYPE= a.OBJ_TYPE,
                            MAX_MONERY=a.MAX_MONERY,
                            MIN_MONERY=a.MIN_MONERY,
                            CATE_IDS=a.CATE_IDS,
                            

                        };

            var list = query.ToList();
            var local = from a in list
                        select new DevFlowTempView
                        {
                            ID = a.ID,
                            NAME = a.NAME,//名称
                            CODE = a.CODE,//编号
                            F_STATE = a.F_STATE,//状态
                            DEPART_IDS = a.DEPART_IDS,
                            FLOW_ITEMS = a.FLOW_ITEMS,
                            VERSION = a.VERSION,
                            OBJ_TYPE = a.OBJ_TYPE,
                            MAX_MONERY = a.MAX_MONERY,
                            MIN_MONERY = a.MIN_MONERY,
                            CATE_IDS = a.CATE_IDS,
                        };
            return local.FirstOrDefault();

        }
        /// <summary>
        /// 根据模板ID获取流出图
        /// </summary>
        /// <param name="tempId">模板ID</param>
        /// <returns></returns>
        public FlowTempChartData GetFlowChart(int tempId)
        {
            //List<DEV_FLOWTEMP_NODE> listnodes = new List<DEV_FLOWTEMP_NODE>();
            //List<DEV_FLOWTEMP_EDGE> FlowEdges = new List<DEV_FLOWTEMP_EDGE>();
            var listnodes = DbClient.Queryable<DEV_FLOWTEMP_NODE>().Where(a => a.TEMP_ID == tempId).ToList();
            var listedges= DbClient.Queryable<DEV_FLOWTEMP_EDGE>().Where(a => a.TEMP_ID == tempId).ToList();

            return new FlowTempChartData { FlowNodes = listnodes, FlowEdges = listedges };

        }

    }
}
