using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Models;
using WooDev.Model.Models;
using WooDev.ViewModel;

namespace WooDev.IServices
{
    /// <summary>
    /// 标的
    /// </summary>
    public partial interface IDevContSubMatterService
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        ResultPageData<DevContSubMatterList> GetList(PageInfo<DEV_CONT_SUB_MATTER> pageInfo, Expression<Func<DEV_CONT_SUB_MATTER, bool>> whereLambda,
            Expression<Func<DEV_CONT_SUB_MATTER, object>> orderbyLambda, bool isAsc);
    }
}
