using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Models;
using WooDev.Model.Models;
using WooDev.ViewModel;
using WooDev.ViewModel.Contract.ExcelModel;

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
        /// <param name="userId">当前登录人</param>
        void CompanySave(DevCompanyDTO devCompanyDTO, int userId);
        /// <summary>
        /// 清理垃圾个人数据
        /// </summary>
        void ClearData(int userId);
        /// <summary>
        /// 清洗数据
        /// </summary>
        /// <param name="compId">对方ID</param>
        /// <param name="userId">当前用户ID</param>
        void UpdateItemData(int compId, int userId);
        /// <summary>
        /// 查看详情
        /// </summary>
        /// <param name="Id">客户ID</param>
        /// <returns></returns>
        DevCompanyView ShowDetail(int Id);
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="updateState">修改数据对象</param>
        /// <param name="userId">当前用户</param>
        /// <returns></returns>
        int UpdateState(UpdateStateDTO updateState, int userId);

    }
}
