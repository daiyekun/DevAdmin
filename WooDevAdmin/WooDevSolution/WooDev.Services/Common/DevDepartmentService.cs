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

    /// <summary>
    /// 组织机构
    /// </summary>
    public partial class DevDepartmentService 
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
        public ResultPageData<DevDepartmentList> GetList(PageInfo<DEV_DEPARTMENT> pageInfo, Expression<Func<DEV_DEPARTMENT, bool>>? whereLambda,
             Expression<Func<DEV_DEPARTMENT, object>> orderbyLambda, bool isAsc)
        {

            var tempquery = DbClient.Queryable<DEV_DEPARTMENT>().Where(whereLambda);
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
                            PID = a.PID,
                            ORDER_NUM = a.ORDER_NUM,//排序
                            CODE=a.CODE,//编号
                            IS_MAIN=a.IS_MAIN,
                            SORT_NAME=a.SORT_NAME,//机构类型
                            DEP_HEAD=a.DEP_HEAD,//部门负责人
                            CREATE_TIME=a.CREATE_TIME,//创建时间
                            CREATE_USERID = a.CREATE_USERID,//创建人
                           

                        };
            int totalCount = 0;
            if ((pageInfo is NoPageInfo<DEV_DEPARTMENT>))
            { //分页
                pageInfo.PageSize = 2000;
                pageInfo.PageIndex = 0;
            }
            var list = query.ToPageList(pageInfo.PageIndex, pageInfo.PageSize, ref totalCount);
            pageInfo.TotalCount = totalCount;
            var local = from a in list
                        select new DevDepartmentList
                        {
                            ID = a.ID,
                            NAME = a.NAME,//显示名称
                            PID = a.PID,
                            ORDER_NUM = a.ORDER_NUM,//排序
                            CODE = a.CODE,//编号
                            IS_MAIN = a.IS_MAIN,
                            SORT_NAME = a.SORT_NAME,//机构类型
                            DEP_HEAD = a.DEP_HEAD,//部门负责人
                            CREATE_TIME = a.CREATE_TIME,//创建时间
                            CREATE_USERID = a.CREATE_USERID,//创建人

                        };
            return new ResultPageData<DevDepartmentList>()
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
        public IList<DevDepartmentDTO> GetAll()
        {
            IList<DevDepartmentDTO> list = RedisUtility.StringGetToList<DevDepartmentDTO>(RedisKeys.DepartAllListKey);
            if (list == null)
            {
                var query = from a in DbClient.Queryable<DEV_DEPARTMENT>()
                            select new
                            {
                                ID = a.ID,
                                NAME = a.NAME,//显示名称
                                PID = a.PID,
                                ORDER_NUM = a.ORDER_NUM,//排序
                                CODE = a.CODE,//编号
                                IS_MAIN = a.IS_MAIN,
                                SORT_NAME = a.SORT_NAME,//机构类型
                                DEP_HEAD = a.DEP_HEAD,//部门负责人
                                CREATE_TIME = a.CREATE_TIME,//创建时间
                                CREATE_USERID = a.CREATE_USERID,//创建人
                                

                            };
                var local = from a in query
                            select new DevDepartmentDTO
                            {
                                ID = a.ID,
                                NAME = a.NAME,//显示名称
                                PID = a.PID,
                                ORDER_NUM = a.ORDER_NUM,//排序
                                CODE = a.CODE,//编号
                                IS_MAIN = a.IS_MAIN,
                                SORT_NAME = a.SORT_NAME,//机构类型
                                DEP_HEAD = a.DEP_HEAD,//部门负责人
                                CREATE_TIME = a.CREATE_TIME,//创建时间
                                CREATE_USERID = a.CREATE_USERID,//创建人

                            };
                list = local.ToList();
                RedisUtility.ListObjToJsonStringSetAsync(RedisKeys.DepartAllListKey, list);


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
                var curdickey = $"{RedisKeys.DepartHashKey}";
                RedisUtility.KeyDeleteAsync(RedisKeys.DepartAllListKey);
                var list = GetAll();
                foreach (var item in list)
                {
                    item.SetRedisHash<DevDepartmentDTO>($"{curdickey}", (a, c) =>
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


        #region treetable
        /// <summary>
        /// 返回vue-tabletree需要数据格式
        /// </summary>
        /// <returns></returns>
        public List<DeptTreeTable> GetTableTree(PageInfo<DEV_DEPARTMENT> pageInfo, Expression<Func<DEV_DEPARTMENT, bool>>? whereLambda,
             Expression<Func<DEV_DEPARTMENT, object>> orderbyLambda, bool isAsc)
        {
            List<DeptTreeTable> listTree = new List<DeptTreeTable>();
            var listAll = GetList(pageInfo, whereLambda,orderbyLambda, isAsc);
            var list = listAll.items;
            foreach (var item in list.Where(a => a.PID == 0))
            {
                DeptTreeTable treeInfo = new DeptTreeTable();
                treeInfo.ID = item.ID;
                treeInfo.NAME = item.NAME;
                treeInfo.CODE = item.CODE;
                treeInfo.CREATE_TIME = item.CREATE_TIME;
                treeInfo.IS_MAIN= item.IS_MAIN;
                treeInfo.ORDER_NUM= item.ORDER_NUM;
                RecursionChrenNode(list, treeInfo, item);
                listTree.Add(treeInfo);

            }
            return listTree;
        }

        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="listDepts">数据列表</param>
        /// <param name="treeInfo">Tree对象</param>
        /// <param name="item">父类对象</param>
        public void RecursionChrenNode(IList<DevDepartmentList> listDepts, DeptTreeTable treeInfo, DevDepartmentList item)
        {
            var listchren = listDepts.Where(a => a.PID == item.ID);
            var listchrennode = new List<DeptTreeTable>();
            if (listchren.Any())
            {
                var chrenlist = listchren.ToList();
                foreach (var chrenItem in chrenlist)
                {
                    DeptTreeTable treeInfotmp = new DeptTreeTable();
                    treeInfotmp.ID = chrenItem.ID;
                    treeInfotmp.NAME = chrenItem.NAME;
                    treeInfotmp.CODE = chrenItem.CODE;
                    treeInfotmp.CREATE_TIME = chrenItem.CREATE_TIME;
                    treeInfotmp.IS_MAIN = chrenItem.IS_MAIN;
                    treeInfotmp.ORDER_NUM = chrenItem.ORDER_NUM;
                    RecursionChrenNode(listDepts, treeInfotmp, chrenItem);
                    listchrennode.Add(treeInfotmp);
                }
                treeInfo.children = listchrennode;

            }



        }

        /// <summary>
        /// 查询tree列表
        /// </summary>
        /// <returns></returns>
        public ResultPageData<DeptTreeTable> GetDeptTreeList(PageInfo<DEV_DEPARTMENT> pageInfo, Expression<Func<DEV_DEPARTMENT, bool>>? whereLambda,
             Expression<Func<DEV_DEPARTMENT, object>> orderbyLambda, bool isAsc)
        {
            var treedept = GetTableTree(pageInfo,  whereLambda,orderbyLambda,isAsc);
            return new ResultPageData<DeptTreeTable>()
            {
                items = treedept,
                total = pageInfo.TotalCount,
                page = pageInfo.PageIndex,
                pageSize = pageInfo.PageSize


            };

        }
        #endregion





    }
}
