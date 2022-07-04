using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;
using WooDev.ViewModel;

namespace WooDev.Services
{

    /// <summary>
    /// 角色菜单权限表
    /// </summary>
    public partial class DevRoleFunctionService
    {

        /// <summary>
        /// 新增角色菜单权限
        /// </summary>
        /// <param name="roleMenuDTO">角色菜单权限对象</param>
        public void SaveRoleMenus(RoleMenuDTO roleMenuDTO)
        {
            var isexistrole = DbClient.Queryable<DEV_ROLE_FUNCTION>().Any(a => a.ROLE == roleMenuDTO.RoleId);
            if (isexistrole)
            {//存在先删除
                this.Delete(a => a.ROLE == roleMenuDTO.RoleId);
            }
            List<DEV_ROLE_FUNCTION> listfunctions = new List<DEV_ROLE_FUNCTION>();
            foreach (var mId in roleMenuDTO.MenuIds)
            {
                var info = new DEV_ROLE_FUNCTION();
                info.FUNCTION_ID = mId;
                info.ROLE = roleMenuDTO.RoleId;
                info.CREATE_TIME = DateTime.Now;
                info.CREATE_USERID = roleMenuDTO.UserId;
                info.UPDATE_TIME = DateTime.Now;
                info.UPDATE_USERID = roleMenuDTO.UserId;
                info.IS_DELETE = 0;

                listfunctions.Add(info);


            }

            this.Add(listfunctions);

        }
    }
}
