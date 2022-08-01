using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Models;
using WooDev.Model.Models;
using WooDev.ViewModel;

namespace WooDev.Services
{

    /// <summary>
    ///用户组
    /// </summary>
    public partial class DevFlowGroupuserService
    {
        /// <summary>
        /// 新增组用户
        /// </summary>
        /// <param name="flowGroupUserDTO">流程用户组</param>
        public void SaveGroupUsers(DevFlowGroupuserDTO flowGroupUserDTO)
        {
            var isexistrole = DbClient.Queryable<DEV_FLOW_GROUPUSER>().Any(a => a.GROUP_ID == flowGroupUserDTO.GROUP_ID);
            if (isexistrole)
            {//存在先删除
                this.Delete(a => a.GROUP_ID == flowGroupUserDTO.GROUP_ID);
            }
            List<DEV_FLOW_GROUPUSER> listgroupusers = new List<DEV_FLOW_GROUPUSER>();
            foreach (var uId in flowGroupUserDTO.UserIds)
            {
                var info = new DEV_FLOW_GROUPUSER();
                info.USER_ID = uId;
                info.GROUP_ID = flowGroupUserDTO.GROUP_ID;

                listgroupusers.Add(info);


            }

            this.Add(listgroupusers);

        }


       
    }
}
