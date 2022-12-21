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
    /// 合同接口
    /// </summary>
    public partial interface IDevContractService
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
        ResultPageData<DevContractList> GetList(PageInfo<DEV_CONTRACT> pageInfo, Expression<Func<DEV_CONTRACT, bool>> whereLambda,
            Expression<Func<DEV_CONTRACT, object>> orderbyLambda, bool isAsc);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="devCompanyDTO"></param>
        /// <param name="userId">当前登录人ID </param>
        void ConstractSave(DevContractDTO devCompanyDTO, int userId);
        /// <summary>
        /// 清理垃圾个人数据
        /// </summary>
        void ClearData(int userId);
        /// <summary>
        /// 查看详情
        /// </summary>
        /// <param name="Id">客户ID</param>
        /// <returns></returns>
        DevContractView ShowDetail(int Id);
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="updateState">修改数据对象</param>
        /// <param name="userId">当前用户</param>
        /// <returns></returns>
        int UpdateState(UpdateStateDTO updateState, int userId);
        /// <summary>
        /// 创建历史表
        /// </summary>
        void CreateHistory(int contId, int userId);
        }
}
