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
using WooDev.IServices;
using WooDev.Model.Models;
using WooDev.ViewModel;
using WooDev.ViewModel.ExtendModel;

namespace WooDev.Services
{
    public partial class DevDatadicService
    {
       

        /// <summary>
        /// 字典列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where 条件</param>
        /// <param name="orderbyLambda">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public ResultPageData<DevDatadicList> GetList(PageInfo<DEV_DATADIC> pageInfo, Expression<Func<DEV_DATADIC, bool>>? whereLambda,
             Expression<Func<DEV_DATADIC, object>> orderbyLambda, bool isAsc)
        {

            var tempquery = DbClient.Queryable<DEV_DATADIC>().Where(whereLambda);
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Asc);
            }
            else
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Desc);
            }

            var query = from a in tempquery
                        select new
                        {
                            ID = a.ID,
                            NAME = a.NAME,//显示名称
                            ORDER_NUM = a.ORDER_NUM,//排序
                            REMARK = a.REMARK,
                            CODE = a.CODE,
                            PID = a.PID,
                            APP_TYPE = a.APP_TYPE,
                            SORT_NAME = a.SORT_NAME,//简称
                            FILED1 = a.FILED1,//
                            FILED2=a.FILED2

                        };
            int totalCount = 0;
            var list = query.ToPageList(pageInfo.PageIndex, pageInfo.PageSize, ref totalCount);
            pageInfo.TotalCount = totalCount;
            var local = from a in list
                        select new DevDatadicList
                        {
                            ID = a.ID,
                            NAME = a.NAME,//显示名称
                            ORDER_NUM = a.ORDER_NUM,//排序
                            REMARK = a.REMARK,
                            CODE = a.CODE,
                            PID = a.PID,
                            APP_TYPE = a.APP_TYPE,
                            SORT_NAME = a.SORT_NAME,//简称
                            FILED1 = a.FILED1,//
                            FILED2 = a.FILED2

                        };
            return new ResultPageData<DevDatadicList>()
            {
                items = local.ToList(),
                total= pageInfo.TotalCount,
                page = pageInfo.PageIndex,
                pageSize = pageInfo.PageSize


            };
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="datadic">字典枚举值</param>
        /// <returns>返回枚举</returns>
        public IList<DevDatadicDTO> GetAll()
        {
            IList<DevDatadicDTO> list = RedisUtility.StringGetToList<DevDatadicDTO>($"{RedisKeys.DataDicALLListKey}");
            if (list == null)
            {
                var query = from a in DbClient.Queryable<DEV_DATADIC>()
                            select new
                            {
                                ID = a.ID,
                                NAME = a.NAME,//名称
                                PID = a.PID,//pid
                                SORT_NAME = a.SORT_NAME,//简称
                                APP_TYPE = a.APP_TYPE,//类别ID
                                REMARK = a.REMARK,//备注
                                IS_DELETE = a.IS_DELETE,//移动电话
                                FILED1 = a.FILED1,//
                                FILED2 = a.FILED2
                            };
                var local = from a in query
                            select new DevDatadicDTO
                            {

                                ID = a.ID,
                                NAME = a.NAME,//名称
                                PID = a.PID,//pid
                                SORT_NAME = a.SORT_NAME,//简称
                                APP_TYPE = a.APP_TYPE,//类别ID
                                REMARK = a.REMARK,//备注
                                IS_DELETE = a.IS_DELETE,//移动电话
                                FILED1 = a.FILED1,//
                                FILED2 = a.FILED2

                            };
                list = local.ToList();
                RedisUtility.ListObjToJsonStringSetAsync($"{RedisKeys.DataDicALLListKey}", list);
            }

            return list;

        }

        /// <summary>
        /// 设置Redis
        /// </summary>
        /// <param name="datadic">字典枚举</param>
        /// <returns></returns>
        public void SetRedisHash()
        {
            try
            {
                var curdickey = $"{RedisKeys.DataDicHashKey}";
                var list = GetAll();
                foreach (var item in list)
                {
                    item.SetRedisHash<DevDatadicDTO>($"{curdickey}", (a, c) =>
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

        /// <summary>
        /// 查询字典
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="whereLambda">where 条件</param>
        /// <param name="orderbyLambda">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public List<DevDatadicList> GetDataList(Expression<Func<DEV_DATADIC, bool>>? whereLambda,
             Expression<Func<DEV_DATADIC, object>> orderbyLambda, bool isAsc)
        {

            var tempquery = DbClient.Queryable<DEV_DATADIC>().Where(whereLambda);
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Asc);
            }
            else
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Desc);
            }

            var query = from a in tempquery
                        select new
                        {
                            ID = a.ID,
                            NAME = a.NAME,//显示名称
                            ORDER_NUM = a.ORDER_NUM,//排序
                            REMARK = a.REMARK,
                            CODE = a.CODE,
                            PID = a.PID,
                            APP_TYPE = a.APP_TYPE,
                            SORT_NAME = a.SORT_NAME,//简称
                            FILED1 = a.FILED1,//
                            FILED2 = a.FILED2

                        };
            var list = query.ToList();
            var local = from a in list
                        select new DevDatadicList
                        {
                            ID = a.ID,
                            NAME = a.NAME,//显示名称
                            ORDER_NUM = a.ORDER_NUM,//排序
                            REMARK = a.REMARK,
                            CODE = a.CODE,
                            PID = a.PID,
                            APP_TYPE = a.APP_TYPE,
                            SORT_NAME = a.SORT_NAME,//简称
                            FILED1 = a.FILED1,//
                            FILED2 = a.FILED2

                        };
            return local.ToList();
        }
    }
}
