using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel.ExtendModel
{

    /// <summary>
    /// 上传文件承载对象
    /// </summary>
    public class UploadFileInfo
    {
        /// <summary>
        /// 原始文件名称
        /// </summary>
        public string? SourceFileName { get; set; }
        /// <summary>
        /// Guid后文件名称
        /// </summary>
        public string? GuidFileName { get; set; }
        /// <summary>
        /// 存储文件名称
        /// </summary>
        public string? FolderName { get; set; }
        /// <summary>
        /// 没有扩展的文件名
        /// </summary>
        public string? NotExtenFileName { get; set; }
        /// <summary>
        /// 文件扩展
        /// </summary>
        public string? Extension { get; set; }
        /// <summary>
        /// 是否使用Guid文件名称
        /// </summary>
        public bool RemGuidName { get; set; } = true;
        /// <summary>
        /// 文件夹路径
        /// </summary>
        public string ? FolderPath { get; set;}
        /// <summary>
        /// 文件夹枚举值
        /// </summary>
        public int? FolderIndex { get; set; }

    }

    /// <summary>
    /// 文件上传对象
    /// </summary>
    public class UploadData
    {
        public string? filename { get; set; }
        public string? name{get;set;}
       
        /// <summary>
        /// 请求额外数据
        /// </summary>
        public Odata? data { get; set; }
        
    }
    /// <summary>
    /// 请求额外数据
    /// </summary>
    public class Odata
    {
        public int folder { get; set; }

    }

    
}
