using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooDev.Auth
{
    public class JWTTokenOptions
    {
        public string Audience
        {
            get;
            set;
        }
        public string SecurityKey
        {
            get;
            set;
        }
        //public SigningCredentials Credentials
        //{
        //    get;
        //    set;
        //}
        public string Issuer
        {
            get;
            set;
        }
    }
}
