using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel.Contract.ExcelModel
{

    /// <summary>
    ///修改数据状态
    /// </summary>
    public class UpdateStateDTO
    {
        /// <summary>
        /// 修改数据状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 修改数据的ID
        /// </summary>
        public int Id { get; set; }
    }
}
