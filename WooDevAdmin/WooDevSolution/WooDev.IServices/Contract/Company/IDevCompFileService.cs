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
    /// 合同对方附件
    /// </summary>
    public partial interface IDevCompFileService
    {

        /// <summary>
        /// 附件列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        ResultPageData<DevCompFileList> GetList(PageInfo<DEV_COMP_FILE> pageInfo, Expression<Func<DEV_COMP_FILE, bool>> whereLambda,
            Expression<Func<DEV_COMP_FILE, object>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="updateField">字段对象</param>
        /// <returns></returns>
        int UpdateField(UpdateField updateField);
     }
}
