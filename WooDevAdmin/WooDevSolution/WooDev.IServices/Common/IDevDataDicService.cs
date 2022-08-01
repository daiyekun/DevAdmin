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
    /// 数据字典
    /// </summary>
    public partial interface IDevDatadicService
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
             Expression<Func<DEV_DATADIC, object>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 设置数据字典
        /// </summary>
        /// <param name="datadic"></param>
        void SetRedisHash();
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="datadic">字典枚举值</param>
        /// <returns>返回枚举</returns>
        IList<DevDatadicDTO> GetAll();
        /// <summary>
        /// 查询字典
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="whereLambda">where 条件</param>
        /// <param name="orderbyLambda">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        List<DevDatadicList> GetDataList(Expression<Func<DEV_DATADIC, bool>>? whereLambda,
            Expression<Func<DEV_DATADIC, object>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 查询字段树
        /// </summary>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderbyLambda">排序</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
         List<DataDicTree> GetDicTree(Expression<Func<DEV_DATADIC, bool>>? whereLambda,
            Expression<Func<DEV_DATADIC, object>> orderbyLambda, bool isAsc);
    }
}
