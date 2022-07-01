using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.Model.Models
{

    /// <summary>
    /// 操作日志
    /// </summary>
    public partial class DEV_OPTION_LOG
    {
        [Navigate(NavigateType.OneToOne, nameof(CREATE_USERID))]//一对一
        public DEV_USER User { get; set; }

    }
}
