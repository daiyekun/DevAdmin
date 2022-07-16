using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.ViewModel.ExtendModel;

namespace WooDev.ViewModel.Contract.ExcelModel
{

    /// <summary>
    /// 客户导出条件
    /// </summary>
    public class CustomerExcel: ExportExcelInfo
    {
        /// <summary>
        /// 搜索条件
        /// </summary>
        public DevCompanySearch SearData { get; set; }
    }
}
