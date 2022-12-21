using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;
using WooDev.ViewModel.Enums;
using WooDev.ViewModel.ExtendModel;

namespace WooDev.IServices
{

    /// <summary>
    /// 权限
    /// </summary>
    public partial interface IDevRolePermissionService
    {

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
        Expression<Func<DEV_COMPANY, bool>> GetCompanyListPermissionExpression(string funcCode, int userId, int deptId = 0, int roleId = 0);
        /// <summary>
        /// 获取合同对方列表权限表达式
        /// </summary>
        /// <param name="expre">表达式树，为了后期索引优化</param>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">当前用户所属部门ID</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="roleId">角色ID</param>
        /// 权限类型：
        /// 1类：4是/5否 ==》新建权限
        /// 2类：0个人、1机构、2全部、3本机构、-1 无权限
        /// <returns>合同对方权限表达式树</returns>
         Expressionable<DEV_COMPANY> GetCompanyListPermissionExpression(Expressionable<DEV_COMPANY> expre,string funcCode, int userId, int deptId = 0, int roleId = 0);
        /// <summary>
        /// 新增权限
        /// </summary>
        /// <param name="funcCode">权限标识</param>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">部门ID</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        bool GetCreateCompanyPerssion(string funcCode, int userId, int deptId = 0, int roleId = 0);
        /// <summary>
        /// 判断当前用户是否有修改合同对方的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <returns>PermissionDicEnum</returns>
         PermissionDicEnum GetCompanyUpdatePermission(string funcCode, int userId, int deptId, int roleId, int updateObjId);
        /// <summary>
        /// 合同对方删除权限
        /// </summary>
        /// <param name="funcCode">权限标识</param>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">部门ID</param>
        /// <param name="roleId">角色ID</param>
        /// <param name="listdelIds">删除IDs</param>
        /// <returns>权限对象</returns>
        PermissionDataInfo GetCompanyDeletePermission(string funcCode, int userId, int deptId, int roleId, IList<int> listdelIds);
        /// <summary>
        /// 判断当前用户是否有查看合同对方的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="deptId">部门ID</param>
        /// <param name="roleId">角色ID</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <returns>PermissionDicEnum</returns>
        PermissionDicEnum GetCompanyViewPermission(string funcCode, int userId, int deptId, int roleId, int updateObjId);

        #endregion 合同对方权限

        #region 合同权限
        /// <summary>
        /// 获取合同列表权限表达式
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">当前用户所属部门ID</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="roleId">角色ID</param>
        /// 权限类型：
        /// 1类：4是/5否 ==》新建权限
        /// 2类：0个人、1机构、2全部、3本机构、-1 无权限
        /// <returns>合同对方权限表达式树</returns>
         Expression<Func<DEV_CONTRACT, bool>> GetContractListPermissionExpression(string funcCode, int userId, int deptId = 0, int roleId = 0);
        /// <summary>
        /// 获取合同列表权限表达式
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
        Expressionable<DEV_CONTRACT> GetContractListPermissionExpression(Expressionable<DEV_CONTRACT> expre, string funcCode, int userId, int deptId = 0, int roleId = 0);
        #endregion

    }
}
