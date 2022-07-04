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
using WooDev.ViewModel.ExtendModel;

namespace WooDev.Services
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public partial class DevRoleService
    {
        /// <summary>
        /// 部门列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where 条件</param>
        /// <param name="orderbyLambda">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public ResultPageData<DevRoleList> GetList(PageInfo<DEV_ROLE> pageInfo, Expression<Func<DEV_ROLE, bool>>? whereLambda,
            Expression<Func<DEV_ROLE, object>> orderbyLambda, bool isAsc)
          {

            var tempquery = DbClient.Queryable<DEV_ROLE>().Includes(a=>a.Menus).Where(whereLambda);
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Asc);
            }
            else
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Desc);
            }

            //var query = from a in tempquery
            //            select new
            //            {
            //                ID = a.ID,
            //                NAME = a.NAME,//名称
            //                CODE = a.CODE,//编号
            //                REMARK=a.REMARK,//备注
            //                RUSTATE=a.RUSTATE,//状态
            //                CREATE_TIME = a.CREATE_TIME,//创建时间
            //                CREATE_USERID = a.CREATE_USERID,//创建人
            //                menu=a.Menus.ToList()//菜单


            //            };
            int totalCount = 0;
            if ((pageInfo is NoPageInfo<DEV_ROLE>))
            { //分页
                pageInfo.PageSize = 2000;
                pageInfo.PageIndex = 0;
            }
            var list = tempquery.ToPageList(pageInfo.PageIndex, pageInfo.PageSize, ref totalCount, a => new {
                ID = a.ID,
                NAME = a.NAME,//名称
                CODE = a.CODE,//编号
                REMARK = a.REMARK,//备注
                RUSTATE = a.RUSTATE,//状态
                CREATE_TIME = a.CREATE_TIME,//创建时间
                CREATE_USERID = a.CREATE_USERID,//创建人
                menu = a.Menus.ToList()//菜单
            });
            pageInfo.TotalCount = totalCount;
            var local = from a in list
                        select new DevRoleList
                        {
                            ID = a.ID,
                            NAME = a.NAME,//名称
                            CODE = a.CODE,//编号
                            RUSTATE = a.RUSTATE,//状态
                            REMARK = a.REMARK,//备注
                            CREATE_TIME = a.CREATE_TIME,//创建时间
                            CREATE_USERID = a.CREATE_USERID,//创建人
                            menu = a.menu.Select(a=>a.FUNCTION_ID).ToList()
                        };
            return new ResultPageData<DevRoleList>()
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
        public List<DevRoleDTO> GetAll()
        {
            List<DevRoleDTO> list = RedisUtility.StringGetToList<DevRoleDTO>(RedisKeys.RoleAllListKey);
            if (list == null)
            {
                var query = from a in DbClient.Queryable<DEV_ROLE>()
                            select new
                            {
                                ID = a.ID,
                                NAME = a.NAME,//名称
                                CODE = a.CODE,//编号
                                REMARK = a.REMARK,//备注
                                CREATE_TIME = a.CREATE_TIME,//创建时间
                                CREATE_USERID = a.CREATE_USERID,//创建人
                                RUSTATE=a.RUSTATE,//状态0:启用 1：禁用


                            };
                var local = from a in query
                            select new DevRoleDTO
                            {
                                ID = a.ID,
                                NAME = a.NAME,//名称
                                CODE = a.CODE,//编号
                                REMARK = a.REMARK,//备注
                                CREATE_TIME = a.CREATE_TIME,//创建时间
                                CREATE_USERID = a.CREATE_USERID,//创建人
                                RUSTATE = a.RUSTATE,//状态0:启用 1：禁用

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
                var curdickey = $"{RedisKeys.RoleHashKey}";
                RedisUtility.KeyDeleteAsync(RedisKeys.RoleAllListKey);
                var list = GetAll();
                foreach (var item in list)
                {
                    item.SetRedisHash<DevRoleDTO>($"{curdickey}", (a, c) =>
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
