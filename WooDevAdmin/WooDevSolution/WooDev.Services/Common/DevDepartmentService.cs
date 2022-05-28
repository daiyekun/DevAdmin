using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.IServices.Common;
using WooDev.Model.Models;

namespace WooDev.Services.Common
{

    /// <summary>
    /// 组织机构
    /// </summary>
    public class DevDepartmentService : BaseService<DEV_DEPARTMENT>, IDevDepartmentService
    {
        public DevDepartmentService(ISqlSugarClient DbClient): base(DbClient)
        {

        }



    }
}
