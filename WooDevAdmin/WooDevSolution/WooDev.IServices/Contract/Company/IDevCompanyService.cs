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
    /// 合同对方操作
    /// </summary>
    public partial interface IDevCompanyService
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
         ResultPageData<DevCompanyList> GetList(PageInfo<DEV_COMPANY> pageInfo, Expression<Func<DEV_COMPANY, bool>> whereLambda,
             Expression<Func<DEV_COMPANY, object>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="devCompanyDTO">合同对方字段</param>
        void CompanySave(DevCompanyDTO devCompanyDTO);

    }
}
