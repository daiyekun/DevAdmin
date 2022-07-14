using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.Model.Models
{

    /// <summary>
    /// 合同对方
    /// </summary>
    public partial class DEV_COMPANY
    {
        /// <summary>
        /// 一对一
        /// 创建人
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(CREATE_USERID))]//一对一
        public DEV_USER CreateUser { get; set; }
    }
}
