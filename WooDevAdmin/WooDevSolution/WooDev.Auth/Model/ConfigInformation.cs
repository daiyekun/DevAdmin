using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooDev.Auth.Model
{
    public class ConfigInformation
    {
        public string RootUrl { get; set; }

        public string UserUrl { get; set; }

        public JWTTokenOptions JWTTokenOptions { get; set; }
    }


    
}
