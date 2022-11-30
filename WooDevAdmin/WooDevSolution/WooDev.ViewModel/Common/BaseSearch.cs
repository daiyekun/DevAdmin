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
        /// <summary>
        /// 查询类型
        /// 比如选择使用的时候用来过滤一些状态或者其他条件
        /// </summary>
        public int SelecType { get; set; } = 0;


    }
    /// <summary>
    /// Id
    /// </summary>
    public class BaseGetView
    {
        public int id { get; set; } = 0;
    }

    /// <summary>
    /// 筛选接口
    /// </summary>
    public class DevSearData 
    { 
    }
         
}
