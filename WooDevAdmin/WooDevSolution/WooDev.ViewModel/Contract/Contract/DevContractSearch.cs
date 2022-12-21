using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.ViewModel.Common;

namespace WooDev.ViewModel.Contract.Contract
{
    public class DevContractSearch: DevSearData
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 合同对方
        /// </summary>
        public string ComName { get; set; }

        /// <summary>
        /// 合同类别
        /// </summary>
        public int CATE_ID { get; set; }

        /// <summary>
        /// 签订日期
        /// </summary>
        public string SIGNING_DATE { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public string EFFECTIVE_DATE { get; set; }

        /// <summary>
        /// 合同属性
        /// 0：标准
        /// 1：框架
        /// </summary>
        public int IS_FRAMEWORK { get; set; } = -1;
    }
}
