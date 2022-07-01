
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Models;

namespace WooDev.WebCommon.Extend
{
    public static  class ClaimTokenExtend
    {

        /// <summary>
        /// 获取token所有数据
        /// </summary>
        /// <param name="Claims"></param>
        /// <returns></returns>
        public static RequestBaseData GetToken(this IEnumerable<Claim> Claims)
        {
            var data = new RequestBaseData();
            var devuserId = 0;
            var devdeptId = 0;
            if (Claims!=null&&Claims.Count() > 0)
            {
                var claimsuserId = Claims.Where(a => a.Type == "UserId").FirstOrDefault();
                var claimsName = Claims.Where(a => a.Type == "Name").FirstOrDefault();
                var claimsDeptId = Claims.Where(a => a.Type == "DeptId").FirstOrDefault();
                var claimsShowName = Claims.Where(a => a.Type == "ShowName").FirstOrDefault();
                var claimsDeptName = Claims.Where(a => a.Type == "DeptName").FirstOrDefault();
                var claimsRoleIds = Claims.Where(a => a.Type == "RoleIds").FirstOrDefault();
                int.TryParse((claimsuserId != null ? claimsuserId.Value : "0"), out devuserId);
                int.TryParse((claimsDeptId != null ? claimsDeptId.Value : "0"), out devdeptId);
                data.UserId = devuserId;
                data.Name = claimsName != null ? claimsName.Value : "";
                data.DeptId = devdeptId;
                data.ShowName = claimsShowName != null ? claimsShowName.Value : "";
                data.DeptName = claimsDeptName != null ? claimsDeptName.Value : "";
                data.RoleIds = claimsRoleIds != null ? claimsRoleIds.Value : "";

            }
            return data;
        }

        /// <summary>
        /// 获取token UserId
        /// </summary>
        /// <param name="Claims">Token对象</param>
        /// <returns></returns>

        public static int GetTokenUserId(this IEnumerable<Claim> Claims)
        {
            
            var devuserId = 0;
            if (Claims != null && Claims.Count() > 0)
            {
                var claimsuserId = Claims.Where(a => a.Type == "UserId").FirstOrDefault();
                int.TryParse((claimsuserId != null ? claimsuserId.Value : "0"), out devuserId); 

            }
            return devuserId;
        }
        /// <summary>
        /// 获取token 部门Id
        /// </summary>
        /// <param name="Claims">Token对象</param>
        /// <returns></returns>
        public static int GetTokenDeptId(this IEnumerable<Claim> Claims)
        {

            var devdeptId = 0;
            if (Claims != null && Claims.Count() > 0)
            {
                var claimsDeptId = Claims.Where(a => a.Type == "DeptId").FirstOrDefault();
                int.TryParse((claimsDeptId != null ? claimsDeptId.Value : "0"), out devdeptId);

            }
            return devdeptId;
        }

        /// <summary>
        /// 获取token 登录名称
        /// </summary>
        /// <param name="Claims">Token对象</param>
        /// <returns></returns>
        public static string GetTokenName(this IEnumerable<Claim> Claims)
        {

            string loginname = "";
            if (Claims != null && Claims.Count() > 0)
            {
                var claimsName = Claims.Where(a => a.Type == "Name").FirstOrDefault();

                loginname = claimsName != null ? claimsName.Value : "";
            }
            return loginname;
        }

        /// <summary>
        /// 获取token 登录名称
        /// </summary>
        /// <param name="Claims">Token对象</param>
        /// <returns></returns>
        public static string GetTokenLoginName(this IEnumerable<Claim> Claims)
        {

            string loginname = "";
            if (Claims != null && Claims.Count() > 0)
            {
                var claimsName = Claims.Where(a => a.Type == "LoginName").FirstOrDefault();

                loginname = claimsName != null ? claimsName.Value : "";
            }
            return loginname;
        }

        /// <summary>
        /// 获取token 登录人员角色ID
        /// </summary>
        /// <param name="Claims">Token对象</param>
        /// <returns></returns>
        public static string GetTokenRoleIds(this IEnumerable<Claim> Claims)
        {

            string roleIds = "";
            if (Claims != null && Claims.Count() > 0)
            {
                var claimsRoleIds = Claims.Where(a => a.Type == "RoleIds").FirstOrDefault();

                roleIds = claimsRoleIds != null ? claimsRoleIds.Value : "";
            }
            return roleIds;
        }
    }
}
