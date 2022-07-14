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
using WooDev.ViewModel.Contract.Enums;
using WooDev.ViewModel.Enums;
using WooDev.ViewModel.ExtendModel;

namespace WooDev.Services
{
    public partial class DevRolePermissionService
    {
        #region 公共
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
        /// 查询当前用户对应于某个权限标识的权限类型
        /// </summary>
        /// <param name="userId">当前用户，考虑未来用</param>
        /// <param name="roleId">角色ID</param>
        /// <param name="funcCode">权限标识</param>
        /// <returns>权限类型</returns>
        public PerissionEnums GetPession(int roleId, string funcCode, int userId = 0)
        {
            var temppesrinof = DbClient.Queryable<DEV_ROLE_PERMISSION>()
                .Where(a => a.R_ID == roleId && a.M_CODE == funcCode).First();

            return (PerissionEnums)(temppesrinof != null ? temppesrinof.P_IDEN : -1);
        }
        /// <summary>
        /// 根据部门ID获取所有子机构ID
        /// </summary>
        /// <param name="deptId">当前部门ID</param>
        /// <param name="IsIncoude">是否包含自己</param>
        /// <returns>子机构ID集合</returns>
        public List<int> GetDeptChids(int deptId, bool IsIncoude = true)
        {
            List<int> listdept = new List<int>();
            if (IsIncoude)
            {
                listdept.Add(deptId);
            }
            var listAll = GetAll();
            DeepChds(listAll, deptId, listdept);
            return listdept;
        }

        /// <summary>
        /// 递归获取子部门ID
        /// </summary>
        /// <param name="Alllist">所有部门</param>
        /// <param name="deptId">部门ID</param>
        /// <param name="listdept">部门ID集合</param>
        /// <returns></returns>
        public List<int> DeepChds(IList<DevDepartmentDTO> Alllist, int deptId, List<int> listdept)
        {
            var onechds = Alllist.Where(a => a.PID == deptId).ToList();
            foreach (var item in onechds)
            {
                listdept.Add(item.ID);
                var chidlist = Alllist.Where(a => a.PID == item.ID).ToList();
                if (chidlist.Count > 0)
                {
                    DeepChds(Alllist, item.ID, listdept);
                }
            }

            return listdept;
        }

        #endregion


        #region 合同对方权限
        /// <summary>
        /// 获取合同对方列表权限表达式
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">当前用户所属部门ID</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="roleId">角色ID</param>
        /// 权限类型：
        /// 1类：4是/5否 ==》新建权限
        /// 2类：0个人、1机构、2全部、3本机构、-1 无权限
        /// <returns>合同对方权限表达式树</returns>
        public Expression<Func<DEV_COMPANY, bool>> GetCompanyListPermissionExpression(string funcCode, int userId, int deptId = 0, int roleId = 0)
        {
            var predicate = PredicateBuilder.True<DEV_COMPANY>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
            }
            else
            {
                var predOr = PredicateBuilder.False<DEV_COMPANY>();
                predOr = predOr.Or(p => p.CREATE_USERID == userId);
                predOr = predOr.Or(p => p.LEAD_USERID == userId);//负责人
                predicate = predicate.And(predOr);
                //查询对应角色
                var pession = GetPession(roleId, funcCode, userId);
                switch (pession)
                {
                    case PerissionEnums.DepartTree://机构
                        var listIds = GetDeptChids(deptId);
                        predicate = predicate.And(p => listIds.Any(a => a == p.CreateUser.DEPART_ID));
                        break;
                    case PerissionEnums.All:
                        predicate = predicate.And(p => true);
                        break;
                    case PerissionEnums.Depart://本机构
                        predicate = predicate.And(p => p.CreateUser.DEPART_ID == deptId);
                        break;
                    case PerissionEnums.personal://个人
                        break;
                    default://-1 无权限
                        predicate = predicate.And(p => false);
                        break;
                }

            }
            return predicate;
        }
        /// <summary>
        /// 获取合同对方列表权限表达式
        /// </summary>
        /// <param name="expre">查询表达式对象</param>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">当前用户所属部门ID</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="roleId">角色ID</param>
        /// 权限类型：
        /// 1类：4是/5否 ==》新建权限
        /// 2类：0个人、1机构、2全部、3本机构、-1 无权限
        /// <returns>合同对方权限表达式树</returns>
        public Expressionable<DEV_COMPANY> GetCompanyListPermissionExpression(Expressionable<DEV_COMPANY> expre,string funcCode, int userId, int deptId = 0, int roleId = 0)
        {
            //var predicate = Expressionable.Create<DEV_COMPANY>();
            //var predicate = PredicateBuilder.True<DEV_COMPANY>();
            if (userId == -10000)
            {//超级管理员
                expre = expre.And(a => true);
            }
            else
            {
                
                expre = expre.And(p => p.CREATE_USERID == userId|| p.LEAD_USERID == userId);
                //查询对应角色
                var pession = GetPession(roleId, funcCode, userId);
                switch (pession)
                {
                    case PerissionEnums.DepartTree://机构
                        var listIds = GetDeptChids(deptId);
                        expre = expre.And(p => listIds.Any(a => a == p.CreateUser.DEPART_ID));
                        break;
                    case PerissionEnums.All:
                        expre = expre.And(p => true);
                        break;
                    case PerissionEnums.Depart://本机构
                        expre = expre.And(p => p.CreateUser.DEPART_ID == deptId);
                        break;
                    case PerissionEnums.personal://个人
                        break;
                    default://-1 无权限
                        expre = expre.And(p => false);
                        break;
                }

            }
            return expre;
        }


        /// <summary>
        /// 新增权限
        /// </summary>
        /// <param name="funcCode">权限标识</param>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">部门ID</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public bool GetCreateCompanyPerssion(string funcCode, int userId, int deptId = 0, int roleId = 0)
        {
            var pession = GetPession(roleId, funcCode, userId);
            bool pers = false;
            switch (pession)
            {
                case PerissionEnums.personal:
                case PerissionEnums.Depart:
                case PerissionEnums.All:
                case PerissionEnums.Yes://是
                    pers = true;
                    break;
                default://-1 无权限
                    break;
            }

            return pers;
        }

        /// <summary>
        /// 判断当前用户是否有修改合同对方的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDicEnum GetCompanyUpdatePermission(string funcCode, int userId, int deptId, int roleId, int updateObjId)
        {
            var predicate = PredicateBuilder.True<DEV_COMPANY>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
                return PermissionDicEnum.OK;
            }
            else
            {

                var datainfo = DbClient.Queryable<DEV_COMPANY>().Includes(a => a.CreateUser).First(a => a.ID == updateObjId); //Db.Set<Company>().Find(updateObjId);
                if (datainfo == null && datainfo.C_STATE != (int)CompanyStateEnums.WSH
                    )
                {
                    return PermissionDicEnum.NotState;
                }
                var pession = GetPession(roleId, funcCode, userId);
                switch (pession)
                {
                    case PerissionEnums.All:

                        return PermissionDicEnum.OK;
                    case PerissionEnums.DepartTree:
                        {
                            var listIds = GetDeptChids(deptId);
                            if (listIds.Contains(datainfo.CreateUser.DEPART_ID))
                                return PermissionDicEnum.OK;
                            return PermissionDicEnum.None;
                        }
                    case PerissionEnums.Depart:
                        {
                            if (datainfo.CreateUser != null && deptId == datainfo.CreateUser.DEPART_ID)

                                return PermissionDicEnum.OK;
                            return PermissionDicEnum.None;
                        }
                    case PerissionEnums.personal:
                        {
                            if (datainfo.CREATE_ID == userId || datainfo.LEAD_USERID == userId)
                                return PermissionDicEnum.OK;
                            return PermissionDicEnum.None;
                        }


                }
                return PermissionDicEnum.None;
            }

        }

        /// <summary>
        /// 合同对方删除权限
        /// </summary>
        /// <param name="funcCode">权限标识</param>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">部门ID</param>
        /// <param name="roleId">角色ID</param>
        /// <param name="listdelIds">删除IDs</param>
        /// <returns>权限对象</returns>
        public PermissionDataInfo GetCompanyDeletePermission(string funcCode, int userId, int deptId, int roleId, IList<int> listdelIds)
        {

            var predicate = PredicateBuilder.True<DEV_COMPANY>();
            if (userId == -10000)
            {//超级管理员
                var permiss = new PermissionDataInfo();
                predicate = predicate.And(a => true);
                permiss.Code = 0;
                return permiss;
            }
            else
            {

                var permiss = new PermissionDataInfo();
                var querylist = DbClient.Queryable<DEV_COMPANY>().Where(a => listdelIds.Contains(a.ID)).ToList();
                var listnot = querylist.Where(a => a.C_STATE != (byte)CompanyStateEnums.WSH
                ).Select(a => a.NAME).ToList();
                if (listnot.Count() > 0)
                {
                    permiss.Code = 4;
                    permiss.noteAllow = listnot;
                    return permiss;
                }
                var pession = GetPession(roleId, funcCode, userId);
                switch (pession)
                {
                    case PerissionEnums.All:
                        permiss.Code = 0;
                        return permiss;
                    case PerissionEnums.Depart:
                        {
                            var usertempIds = querylist.Where(a => a.CreateUser.DEPART_ID == deptId).Select(a => a.ID).ToList();
                            var listnotdeptdata = querylist.Where(a => !usertempIds.Contains(a.ID)).Select(a => a.NAME).ToList();
                            if (listnotdeptdata.Count() > 0)
                            {
                                permiss.Code = 3;//部分没权限
                                permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                            }
                            return permiss;
                        }
                        
                    case PerissionEnums.DepartTree:
                        {
                            var listIds = GetDeptChids(deptId);
                            var usertempIds = querylist.Where(a => listIds.Contains(a.CreateUser.DEPART_ID)).Select(a => a.ID).ToList();
                            var listnotdeptdata = querylist.Where(a => !usertempIds.Contains(a.ID)).Select(a => a.NAME).ToList();
                            if (listnotdeptdata.Count() > 0)
                            {
                                permiss.Code = 3;//部分没权限
                                permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                            }
                            return permiss;
                        }
                    case PerissionEnums.personal:
                        {
                            var usertempIds = querylist.Where(a => a.CREATE_USERID == userId || a.LEAD_USERID == userId).Select(a => a.ID).ToList();
                            var listnotdeptdata = querylist.Where(a => !usertempIds.Contains(a.ID)).Select(a => a.NAME).ToList();
                            if (listnotdeptdata.Count() > 0)
                            {
                                permiss.Code = 3;//部分没权限
                                permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                            }
                            return permiss;
                        }



                }
                permiss.Code = 1;//无权限
                return permiss;


            }

        }

        #endregion 合同对方权限

    }
}
