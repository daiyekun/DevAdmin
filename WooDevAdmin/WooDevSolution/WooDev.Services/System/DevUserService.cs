using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.IServices;
using WooDev.Model.Models;

namespace WooDev.Services
{

    /// <summary>
    /// 系统用户
    /// </summary>
    public partial class DevUserService:BaseService<DEV_USER>, IDevUserService
    {
        public DevUserService(SqlSugarClient DbClient)
          : base(DbClient)
        {

        }

       
    }
}
