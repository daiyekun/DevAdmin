
using System;
using System.Collections.Generic;
using System.Text;
using WooDev.Common.Utility;

namespace WooDev.ViewModel.ExtendModel
{
    /// <summary>
    /// 导出请求对象
    /// </summary>
   public class ExportRequestInfo
    {
        /// <summary>
        /// 选择行时选择对象ID集合
        /// </summary>
        public string Ids { get; set; }
        /// <summary>
        /// 选择的标题
        /// </summary>
        public string SelTitle { get; set; }
        /// <summary>
        /// 选择字段
        /// </summary>
        public string SelField { get; set; }
        /// <summary>
        /// true表示是导出选择列
        /// </summary>
        public bool SelCell { get; set; }
        /// <summary>
        /// True表示是导出选择行
        /// </summary>
        public bool SelRow { get; set; }
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// 导出（收款、付款）
        /// </summary>
        public int FType { get; set; }
        /// <summary>
        /// 查询类型
        /// </summary>
        public int? search { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? beginData { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? endData { get; set; }
        
        /// <summary>
        /// 返回选择的列头
        /// </summary>
        /// <returns></returns>
        public IList<string> GetCellsTitleList()
        {
            return StringHelper.Strint2ArrayString(this.SelTitle);
        }
        /// <summary>
        /// 返回选择的ID集合
        /// </summary>
        /// <returns></returns>
        public IList<int> GetSelectListIds()
        {
            return StringHelper.String2ArrayInt(this.Ids);
        }
        /// <summary>
        /// 返回选择的字段集合
        /// </summary>
        /// <returns></returns>
        public IList<string> GetCellsFieldList()
        {
            return StringHelper.Strint2ArrayString(this.SelField);
        }




    }

    /// <summary>
    /// 导出excel新选择项
    /// </summary>
    public class ExportExcelInfo
    {
        /// <summary>
        /// 选择信息
        /// </summary>
        public ExcelInfo Seldata { get; set; }



    }

    /// <summary>
    /// 导出excel信息
    /// </summary>
    public class ExcelInfo
    {
        /// <summary>
        /// 导出选择行时的选择数据ID
        /// </summary>
        public string? Ids { get; set; }
        /// <summary>
        /// 列方式
        /// 1：导出选择列
        /// 2：导出所有列
        /// </summary>
        public int Scell { get; set; }
        /// <summary>
        /// 行方式
        /// 1：导出选择行
        /// 2：导出当前所有行
        /// </summary>
        public int Srow { get; set; }
        /// <summary>
        /// 选择列字段
        /// </summary>
        public string? CelKeys { get; set; }
        /// <summary>
        /// 选择字段名称
        /// </summary>
        public string? CellNames { get; set; }
    }
}
