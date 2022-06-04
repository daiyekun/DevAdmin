
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooDev.Auth.Model;
using WooDev.ViewModel;

namespace WooDev.Auth
{
    public interface ICustomJWTService
    {
        string GetToken(string UserName, string password, LoginUser user);
    }
}
