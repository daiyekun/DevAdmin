using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Models;
using WooDev.Model.Models;
using WooDev.ViewModel.Common;

namespace WooDev.IServices
{

    /// <summary>
    /// 数据字典
    /// </summary>
    public interface IDevDataDicService
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
        public AjaxListResult<DevDataDicListView> GetList<s>(PageInfo<DEV_DATADIC> pageInfo, Expression<Func<DEV_DATADIC, bool>> whereLambda,
             Expression<Func<DEV_DATADIC, object>> orderbyLambda, bool isAsc);

    }
}
