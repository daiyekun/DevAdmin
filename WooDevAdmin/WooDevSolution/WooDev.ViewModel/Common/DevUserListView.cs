using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;

namespace WooDev.ViewModel.Common
{

    /// <summary>
    /// 用户列表list
    /// </summary>
    public class DevUserListView:DEV_USER
    {
        /// <summary>
        /// 性别描述
        /// </summary>
      public string SexDic { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
      public string UstateDic { get; set; }

    }
}
