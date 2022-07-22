using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Extend;
using WooDev.Common.Models;
using WooDev.Common.Utility;
using WooDev.Model.Models;
using WooDev.ViewModel;

namespace WooDev.Services
{

    /// <summary>
    /// 审批流程组
    /// </summary>
    public partial class DevFlowGroupService
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
        public ResultPageData<DevFlowGroupList> GetList(PageInfo<DEV_FLOW_GROUP> pageInfo, Expression<Func<DEV_FLOW_GROUP, bool>>? whereLambda,
            Expression<Func<DEV_FLOW_GROUP, object>> orderbyLambda, bool isAsc)
        {

            var tempquery = DbClient.Queryable<DEV_FLOW_GROUP>().Where(whereLambda);
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Asc);
            }
            else
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Desc);
            }

           
            int totalCount = 0;
            if ((pageInfo is NoPageInfo<DEV_FLOW_GROUP>))
            { //分页
                pageInfo.PageSize = 20000;
                pageInfo.PageIndex = 0;
            }
            var list = tempquery.ToPageList(pageInfo.PageIndex, pageInfo.PageSize, ref totalCount, a => new {
                ID = a.ID,
                NAME = a.NAME,//名称
                CODE = a.CODE,//编号
                REMARK = a.REMARK,//备注
                G_STATE = a.G_STATE,//状态
                CREATE_TIME = a.CREATE_TIME,//创建时间
                CREATE_USERID = a.CREATE_USERID,//创建人
                //menu = a.Menus.ToList()//菜单
            });
            pageInfo.TotalCount = totalCount;
            var local = from a in list
                        select new DevFlowGroupList
                        {
                            ID = a.ID,
                            NAME = a.NAME,//名称
                            CODE = a.CODE,//编号
                            G_STATE = a.G_STATE,//状态
                            REMARK = a.REMARK,//备注
                            CREATE_TIME = a.CREATE_TIME,//创建时间
                            CREATE_USERID = a.CREATE_USERID,//创建人
                            //menu = a.menu.Select(a => a.FUNCTION_ID).ToList()
                        };
            return new ResultPageData<DevFlowGroupList>()
            {
                items = local.ToList(),
                total = pageInfo.TotalCount,
                page = pageInfo.PageIndex,
                pageSize = pageInfo.PageSize


            };
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<DevFlowGroupDTO> GetAll()
        {
            List<DevFlowGroupDTO> list = RedisUtility.StringGetToList<DevFlowGroupDTO>(RedisKeys.FlowGroupAllListKey);
            if (list == null)
            {
                var query = from a in DbClient.Queryable<DevFlowGroupDTO>()
                            select new
                            {
                                ID = a.ID,
                                NAME = a.NAME,//名称
                                CODE = a.CODE,//编号
                                REMARK = a.REMARK,//备注
                                CREATE_TIME = a.CREATE_TIME,//创建时间
                                CREATE_USERID = a.CREATE_USERID,//创建人
                                G_STATE = a.G_STATE,//状态0:启用 1：禁用


                            };
                var local = from a in query
                            select new DevFlowGroupDTO
                            {
                                ID = a.ID,
                                NAME = a.NAME,//名称
                                CODE = a.CODE,//编号
                                REMARK = a.REMARK,//备注
                                CREATE_TIME = a.CREATE_TIME,//创建时间
                                CREATE_USERID = a.CREATE_USERID,//创建人
                                G_STATE = a.G_STATE,//状态0:启用 1：禁用

                            };
                list = local.ToList();
                RedisUtility.ListObjToJsonStringSetAsync(RedisKeys.RoleAllListKey, list);


            }
            return list;


        }
        /// <summary>
        /// 设置Redis
        /// </summary>
        /// <param name="datadic">角色</param>
        /// <returns></returns>
        public void SetRedisHash()
        {
            try
            {
                var curdickey = $"{RedisKeys.FlowGroupHashKey}";
                RedisUtility.KeyDeleteAsync(RedisKeys.FlowGroupAllListKey);
                var list = GetAll();
                foreach (var item in list)
                {
                    item.SetRedisHash<DevFlowGroupDTO>($"{curdickey}", (a, c) =>
                    {
                        return $"{a}:{c}";
                    });
                }
            }
            catch (Exception ex)
            {

                Log4netHelper.Error(ex.Message);
            }


        }

    }
}
