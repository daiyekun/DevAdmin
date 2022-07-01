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
    /// 角色管理
    /// </summary>
    public partial interface IDevRoleService
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
        ResultPageData<DevRoleList> GetList(PageInfo<DEV_ROLE> pageInfo, Expression<Func<DEV_ROLE, bool>>? whereLambda,
            Expression<Func<DEV_ROLE, object>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<DevRoleDTO> GetAll();
        /// <summary>
        /// 设置Redis
        /// </summary>
        /// <param name="datadic">角色</param>
        /// <returns></returns>
        void SetRedisHash();
    }
}
