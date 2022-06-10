using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Models;
using WooDev.Model.Models;
using WooDev.ViewModel;
using WooDev.ViewModel.ExtendModel;

namespace WooDev.IServices
{

    /// <summary>
    /// 部门-组织机构
    /// </summary>
    public partial interface IDevDepartmentService
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
        ResultPageData<DevDepartmentList> GetList(PageInfo<DEV_DEPARTMENT> pageInfo, Expression<Func<DEV_DEPARTMENT, bool>>? whereLambda,
            Expression<Func<DEV_DEPARTMENT, object>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 设置redis hash
        /// </summary>
        void SetRedisHash();
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        IList<DevDepartmentDTO> GetAll();
        /// <summary>
        /// 查询tree列表
        /// </summary>
        /// <returns></returns>
        List<DeptTreeTable> GetTableTree(PageInfo<DEV_DEPARTMENT> pageInfo, Expression<Func<DEV_DEPARTMENT, bool>>? whereLambda,
            Expression<Func<DEV_DEPARTMENT, object>> orderbyLambda, bool isAsc);
       
    }
}
