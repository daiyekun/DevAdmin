using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.WebCommon.ServiceExtend
{

    /// <summary>
    /// 数据库连接对象
    /// </summary>
    public class DbConnOptions
    {
        /// <summary>
        /// 主库
        /// </summary>
        public string? MasterConnectionString { get; set; }
        /// <summary>
        /// 从库连接
        /// </summary>
        public List<SlaveConnectionConfig> SlaveConnectionConfigs { get; set; }
    }
}
