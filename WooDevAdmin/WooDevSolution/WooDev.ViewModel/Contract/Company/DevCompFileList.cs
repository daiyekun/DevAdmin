using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel
{
    public partial class DevCompFileList
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string? CreateUserName { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string? CateName { get; set; }

    }
    /// <summary>
    /// 文件查询
    /// </summary>
    public class DevComFileSearch
    {

        /// <summary>
        /// 合同对方ID
        /// </summary>
        public int CustId { get; set; } = 0;

    }
}
