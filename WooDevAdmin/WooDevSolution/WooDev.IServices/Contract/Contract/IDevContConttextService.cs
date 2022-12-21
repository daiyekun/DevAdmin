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
    /// 合同文本
    /// </summary>
    public partial interface IDevContConttextService
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
        ResultPageData<DevContConttextList> GetList(PageInfo<DEV_CONT_CONTTEXT> pageInfo, Expression<Func<DEV_CONT_CONTTEXT, bool>> whereLambda,
            Expression<Func<DEV_CONT_CONTTEXT, object>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="updateField">字段对象</param>
        /// <returns></returns>
        int UpdateField(UpdateField updateField);
    }
}
