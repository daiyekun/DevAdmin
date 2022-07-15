using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel.ExtendModel
{
    public class DownLoadInfo
    {
        /// <summary>
        /// 文件流
        /// </summary>
        public FileStream NfFileStream { get; set; }
        public string Memi { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
    }
    /// <summary>
    /// 上传文件返回相关信息
    /// </summary>
    public class DownloadFileInfo
    {
        /// <summary>
        /// 原始文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Guid后文件名称
        /// </summary>
        public string GuidFileName { get; set; }
        /// <summary>
        /// 存储文件名称
        /// </summary>
        public string FolderName { get; set; }
        /// <summary>
        /// 没有扩展的文件名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件扩展
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// 是否使用Guid文件名称
        /// </summary>
        public bool IsGuidName { get; set; } = true;


    }
    /// <summary>
    /// 导出文件承载体
    /// </summary>
    public class ExportFileInfo
    {
        /// <summary>
        ///下载Ip
        /// </summary>
       public string DowIp { get; set; }
        /// <summary>
        /// 下载文件
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件夹路径
        /// </summary>

        public string FilePath { get; set; }
        /// <summary>
        /// 文件后缀
        /// </summary>
        public string Memi { get; set; }
    }
    /// <summary>
    /// 目前没有意义
    /// </summary>
    public class DevViewModel
    {
        public string Username { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 上传下载请求对象
    /// </summary>
    public class DownAndUploadInfo
    {
        /// <summary>
        /// 下载ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 下载文件夹枚举值：Dev.WooNet.Model/Enums/DevFoldersEnum
        /// </summary>
        public int Folderenum { get; set; }
        /// <summary>
        /// 特殊标识，比如合同历史文本下载
        /// </summary>
        public int Dtype { get; set; } = 0;
        /// <summary>
        /// 下载类型（主要用于合同文本下载）
        /// 0：默认值
        /// 1：下载Word
        /// 2:下载PDF
        /// </summary>
        public int DownType { get; set; } = 0;
    }


}
