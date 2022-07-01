using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel.Common
{

    /// <summary>
    /// 搜索key
    /// </summary>
    public class BaseSearch
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string? KeyWord { get; set; }
       
    }
    /// <summary>
    /// Id
    /// </summary>
    public class BaseGetView
    {
        public int id { get; set; } = 0;
    }
         
}
