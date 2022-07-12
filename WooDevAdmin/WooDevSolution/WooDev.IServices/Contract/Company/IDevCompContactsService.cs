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
    /// 联系人
    /// </summary>
    public partial interface IDevCompContactsService
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
        ResultPageData<DevCompContactsList> GetList(PageInfo<DEV_COMP_CONTACTS> pageInfo, Expression<Func<DEV_COMP_CONTACTS, bool>> whereLambda,
            Expression<Func<DEV_COMP_CONTACTS, object>> orderbyLambda, bool isAsc);
        
        }
}
