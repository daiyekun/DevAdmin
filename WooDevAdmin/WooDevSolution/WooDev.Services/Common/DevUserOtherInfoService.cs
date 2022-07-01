using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;
using SqlSugar;

namespace WooDev.Services
{
    public partial class DevUserOtherInfoService
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        public DEV_USER_OTHER_INFO GetInfoByUserId(int userId)
        {
            return DbClient.Queryable<DEV_USER_OTHER_INFO>().Where(a=>a.USER_ID== userId).First();
        }
    }
}
