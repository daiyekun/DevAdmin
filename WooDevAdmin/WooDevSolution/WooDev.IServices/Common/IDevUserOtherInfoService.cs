using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;

namespace WooDev.IServices
{

    /// <summary>
    /// 用户其他信息
    /// </summary>
    public partial interface IDevUserOtherInfoService
    {

        /// <summary>
        /// 用户部分其他信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        DEV_USER_OTHER_INFO GetInfoByUserId(int userId);
    }
}
