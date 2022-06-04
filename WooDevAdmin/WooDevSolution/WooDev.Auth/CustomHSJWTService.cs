
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WooDev.Auth.Model;
using WooDev.ViewModel;

namespace WooDev.Auth
{
    public class CustomHSJWTService : ICustomJWTService
    {
        #region Option注入
        private readonly JWTTokenOptions _JWTTokenOptions;
        public CustomHSJWTService(IOptionsMonitor<ConfigInformation> configInformation)
        {
            this._JWTTokenOptions = configInformation.CurrentValue.JWTTokenOptions;
        }
        #endregion
        ///// <summary>
        ///// 用户登录成功以后，用来生成Token的方法
        ///// </summary>
        ///// <param name="UserName"></param>
        ///// <returns></returns>
        //public string GetToken(string UserName, string password, LoginUser user)
        //{
        //    var claims = new[]
        //    {
        //         new Claim("Name", user.Name),
        //         new Claim("Id", user.Id.ToString()),
        //         new Claim("DeptId", user.DeptId.ToString()),
        //         new Claim("DeptName", user.DeptName)
        //    };

        //    //需要加密：需要加密key:
        //    SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTTokenOptions.SecurityKey));
        //    SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //    JwtSecurityToken token = new JwtSecurityToken(
        //     issuer: _JWTTokenOptions.Issuer,
        //     audience: _JWTTokenOptions.Audience,
        //     claims: claims,
        //     expires: DateTime.Now.AddMinutes(5),
        //     signingCredentials: creds);
        //    string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
        //    return returnToken;
        //}

        public string GetToken(string UserName, string password, LoginUser user)
        {
            var claims = new[]
            {
                 new Claim("LoginName", user.Name),
                 new Claim("UserId", user.Id.ToString()),
                 new Claim("DeptId", user.DeptId.ToString()),
                  new Claim("Name", user.Name),
                 new Claim("DeptName",string.IsNullOrEmpty(user.DeptName)?"": user.DeptName),
                 new Claim("RoleIds", user.RoleIds)
            };

            //需要加密：需要加密key:
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTTokenOptions.SecurityKey));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
             issuer: _JWTTokenOptions.Issuer,
             audience: _JWTTokenOptions.Audience,
             claims: claims,
             expires: DateTime.Now.AddDays(1),
             signingCredentials: creds);
            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
        }

       
    }
}
