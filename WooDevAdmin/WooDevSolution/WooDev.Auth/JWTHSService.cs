using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.Auth
{

    public class JWTHSService : IJWTService
    {
        #region Option注入
        private readonly JWTTokenOptions _JWTTokenOptions;
        public JWTHSService(IOptionsMonitor<JWTTokenOptions> jwtTokenOptions)
        {
            _JWTTokenOptions = jwtTokenOptions.CurrentValue;
        }
        #endregion

        public string GetToken(CurrentUserModel userModel)
        {
            var claims = new[]
            {
                  // new Claim(ClaimTypes.Name, userModel.Name),
                   //new Claim("EMail", userModel.EMail),
                   //new Claim("Account", userModel.Account),
                   //new Claim("Age", userModel.Age.ToString()),
                   //new Claim("Id", userModel.Id.ToString()),
                   //new Claim("Mobile", userModel.Mobile),
                   //new Claim(ClaimTypes.Role,userModel.Role),
                   //new Claim("Role", userModel.Role),//这个不能角色授权
                new Claim("UserLevel", userModel.UserLevel.ToString()),
                new Claim("UserNo", userModel.UserNo),
                new Claim("UserName",userModel.UserName),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTTokenOptions.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            /**
             * Claims (Payload)
                Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:

                iss: The issuer of the token，token 是给谁的
                sub: The subject of the token，token 主题
                exp: Expiration Time。 token 过期时间，Unix 时间戳格式
                iat: Issued At。 token 创建时间， Unix 时间戳格式
                jti: JWT ID。针对当前 token 的唯一标识
                除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
             * */
            var token = new JwtSecurityToken(
                issuer: _JWTTokenOptions.Issuer,
                audience: _JWTTokenOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),//60分钟有效期
                notBefore: DateTime.Now.AddMinutes(1),//1分钟后有效
                signingCredentials: creds);
            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);

            return returnToken;
        }
    }
}
