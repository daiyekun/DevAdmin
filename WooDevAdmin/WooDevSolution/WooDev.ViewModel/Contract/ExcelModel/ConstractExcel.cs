using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.ViewModel.Contract.Contract;
using WooDev.ViewModel.ExtendModel;

namespace WooDev.ViewModel.Contract.ExcelModel
{

    /// <summary>
    /// 合同excel
    /// </summary>
    public class ConstractExcel: ExportExcelInfo
    {
        /// <summary>
        /// 搜索条件
        /// </summary>
        public DevContractSearch SearData { get; set; }
    }
}
