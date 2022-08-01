using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.ViewModel;

namespace WooDev.IServices
{

    /// <summary>
    /// 用户组
    /// </summary>
    public partial interface IDevFlowGroupuserService
    {
        /// <summary>
        /// 新增组用户
        /// </summary>
        /// <param name="flowGroupUserDTO">流程用户组</param>
        void SaveGroupUsers(DevFlowGroupuserDTO flowGroupUserDTO);
    }
}
