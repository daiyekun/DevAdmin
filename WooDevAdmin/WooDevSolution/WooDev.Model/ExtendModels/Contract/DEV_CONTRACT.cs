using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.Model.Models
{
    public partial class DEV_CONTRACT
    {
        /// <summary>
        /// 一对一
        /// 创建人
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(CREATE_USERID))]//一对一
        public DEV_USER CreateUser { get; set; }

        /// <summary>
        /// 一对一
        /// 负责人
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(HEAD_USER_ID))]//一对一
        public DEV_USER HeadUser { get; set; }

        /// <summary>
        /// 一对一
        /// 合同对方
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(HEAD_USER_ID))]//一对一
        public DEV_COMPANY Company { get; set; }
    }
}
