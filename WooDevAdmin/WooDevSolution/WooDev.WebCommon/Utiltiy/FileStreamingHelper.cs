using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Utility;
using WooDev.ViewModel.Enums;
using WooDev.ViewModel.ExtendModel;

namespace WooDev.WebCommon.Utiltiy
{
    public static class FileStreamingHelper
    {
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        #region 目前不用，原始版本
        /// <summary>
        /// 文件流上传
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="InvalidDataException"></exception>
        public static async Task<FormValueProvider> StreamFiles(this HttpRequest request)
        {
            
            if (!MultipartRequestHelper.IsMultipartContentType(request.ContentType))
            {
                throw new Exception($"Expected a multipart request, but got {request.ContentType}");
            }
            var formAccumulator = new KeyValueAccumulator();

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, request.Body);
            var section = await reader.ReadNextSectionAsync();//用于读取Http请求中的第一个section数据
            while (section != null)
            {
                Microsoft.Net.Http.Headers.ContentDispositionHeaderValue contentDisposition;
                var hasContentDispositionHeader = Microsoft.Net.Http.Headers.ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                if (hasContentDispositionHeader)
                {

                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {

                        var loadBufferBytes = 1024;
                       // string filepath = GetCurrFilePath(request, contentDisposition);
                        //using (var targetFileStream = File.Create(filepath))
                        //{
                        //    //section.Body是System.IO.Stream类型，表示的是Http请求中一个section的数据流，从该数据流中可以读出每一个section的全部数据，所以我们下面也可以不用section.Body.CopyToAsync方法，而是在一个循环中用section.Body.Read方法自己读出数据，再将数据写入到targetFileStream
                        //    await section.Body.CopyToAsync(targetFileStream, loadBufferBytes);

                        //}



                    }
                    else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                    {

                        var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                        var encoding = GetEncoding(section);
                        using (var streamReader = new StreamReader(
                            section.Body,
                            encoding,
                            detectEncodingFromByteOrderMarks: true,
                            bufferSize: 1024,
                            leaveOpen: true))
                        {
                            // The value length limit is enforced by MultipartBodyLengthLimit
                            var value = await streamReader.ReadToEndAsync();
                            if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                            {
                                value = String.Empty;
                            }
                            formAccumulator.Append(key.Value, value); // For .NET Core <2.0 remove ".Value" from key

                            if (formAccumulator.ValueCount > _defaultFormOptions.ValueCountLimit)
                            {
                                throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded.");
                            }
                        }
                    }
                }
                try
                {
                    section = await reader.ReadNextSectionAsync();//用于读取Http请求中的下一个section数据
                }
                catch (Exception ex)
                {

                    section = null;
                }
                finally
                {
                    if (section == null || section.Headers.Count <= 0 || string.IsNullOrEmpty(section.ContentDisposition))
                    {
                        section = null;//不然死循环
                    }

                }

            }

           
            var formValueProvider = new FormValueProvider(
             BindingSource.Form,
             new FormCollection(formAccumulator.GetResults()),
             CultureInfo.CurrentCulture);
            


            return formValueProvider;
        }

        #endregion

        #region 大众文件上传
        /// <summary>
        /// 创建文件保存路径
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="contentDisposition">文件描述</param>
        /// <returns></returns>
        public static string GetDevFilePath(HttpRequest request, ContentDispositionHeaderValue contentDisposition, UploadFileInfo uploadFile)
        {
            var fdname = EmunUtility.GetDesc(typeof(DevFolderEnums),uploadFile.FolderIndex??0);
            var fileName = MultipartRequestHelper.GetFileName(contentDisposition);
            var targetDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads", fdname);
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }
            string sourceFilePath = Path.Combine(targetDir, fileName);//原始文件名拼接文件路径
            var extension = Path.GetExtension(sourceFilePath);//文件扩展
            var guistr = Guid.NewGuid().ToString();
            var guidname = $"{guistr}{extension}";
            var filepath = Path.Combine(targetDir, guidname);
            uploadFile.SourceFileName = fileName;
            uploadFile.GuidFileName = guidname;
            uploadFile.FolderPath = $"{fdname}/{guidname}";
            uploadFile.FolderName = fdname;
            uploadFile.NotExtenFileName = guistr;
            uploadFile.Extension = extension;
            return filepath;
        }

        /// <summary>
        /// 文件流上传
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="uploadFileInfo">上传文件信息</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="InvalidDataException"></exception>
        public static async Task<FormValueProvider> DevStreamFiles(this HttpRequest request, 
            UploadFileInfo uploadFileInfo)
        {

            if (!MultipartRequestHelper.IsMultipartContentType(request.ContentType))
            {
                throw new Exception($"Expected a multipart request, but got {request.ContentType}");
            }
            var formAccumulator = new KeyValueAccumulator();

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, request.Body);
            var section = await reader.ReadNextSectionAsync();//用于读取Http请求中的第一个section数据
            while (section != null)
            {
                Microsoft.Net.Http.Headers.ContentDispositionHeaderValue contentDisposition;
                var hasContentDispositionHeader = Microsoft.Net.Http.Headers.ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                if (hasContentDispositionHeader)
                {

                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {

                        var loadBufferBytes = 1024;
                        string filepath = GetDevFilePath(request, contentDisposition,uploadFileInfo);
                        using (var targetFileStream = File.Create(filepath))
                        {
                            //section.Body是System.IO.Stream类型，表示的是Http请求中一个section的数据流，从该数据流中可以读出每一个section的全部数据，所以我们下面也可以不用section.Body.CopyToAsync方法，而是在一个循环中用section.Body.Read方法自己读出数据，再将数据写入到targetFileStream
                            await section.Body.CopyToAsync(targetFileStream, loadBufferBytes);

                        }



                    }
                    else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                    {

                        var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                        var encoding = GetEncoding(section);
                        using (var streamReader = new StreamReader(
                            section.Body,
                            encoding,
                            detectEncodingFromByteOrderMarks: true,
                            bufferSize: 1024,
                            leaveOpen: true))
                        {
                            // The value length limit is enforced by MultipartBodyLengthLimit
                            var value = await streamReader.ReadToEndAsync();
                            if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                            {
                                value = String.Empty;
                            }
                            formAccumulator.Append(key.Value, value); // For .NET Core <2.0 remove ".Value" from key

                            if (formAccumulator.ValueCount > _defaultFormOptions.ValueCountLimit)
                            {
                                throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded.");
                            }
                        }
                    }
                }
                try
                {
                    section = await reader.ReadNextSectionAsync();//用于读取Http请求中的下一个section数据
                }
                catch (Exception ex)
                {

                    section = null;
                }
                finally
                {
                    if (section == null || section.Headers.Count <= 0 || string.IsNullOrEmpty(section.ContentDisposition))
                    {
                        section = null;//不然死循环
                    }

                }

            }


            var formValueProvider = new FormValueProvider(
             BindingSource.Form,
             new FormCollection(formAccumulator.GetResults()),
             CultureInfo.CurrentCulture);



            return formValueProvider;
        }
        #endregion 


        /// <summary>
        /// 头像文件上传---文件流上传
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="uploadinfo">上传对象</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="InvalidDataException"></exception>
        public static async Task<FormValueProvider> StreamFilesUploadFileInfo(this HttpRequest request, UploadFileInfo uploadinfo)
        {
           

            if (!MultipartRequestHelper.IsMultipartContentType(request.ContentType))
            {
                throw new Exception($"Expected a multipart request, but got {request.ContentType}");
            }
            var formAccumulator = new KeyValueAccumulator();

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, request.Body);
            var section = await reader.ReadNextSectionAsync();//用于读取Http请求中的第一个section数据
            while (section != null)
            {
                Microsoft.Net.Http.Headers.ContentDispositionHeaderValue contentDisposition;
                var hasContentDispositionHeader = Microsoft.Net.Http.Headers.ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                if (hasContentDispositionHeader)
                {

                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {

                        var loadBufferBytes = 1024;
                        string filepath = GetCurrFilePath(request, contentDisposition,uploadinfo);
                        using (var targetFileStream = File.Create(filepath))
                        {
                            //section.Body是System.IO.Stream类型，表示的是Http请求中一个section的数据流，从该数据流中可以读出每一个section的全部数据，所以我们下面也可以不用section.Body.CopyToAsync方法，而是在一个循环中用section.Body.Read方法自己读出数据，再将数据写入到targetFileStream
                            await section.Body.CopyToAsync(targetFileStream, loadBufferBytes);

                        }



                    }
                    else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                    {

                        var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                        var encoding = GetEncoding(section);
                        using (var streamReader = new StreamReader(
                            section.Body,
                            encoding,
                            detectEncodingFromByteOrderMarks: true,
                            bufferSize: 1024,
                            leaveOpen: true))
                        {
                            // The value length limit is enforced by MultipartBodyLengthLimit
                            var value = await streamReader.ReadToEndAsync();
                            if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                            {
                                value = String.Empty;
                            }
                            formAccumulator.Append(key.Value, value); // For .NET Core <2.0 remove ".Value" from key

                            if (formAccumulator.ValueCount > _defaultFormOptions.ValueCountLimit)
                            {
                                throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded.");
                            }
                        }
                    }
                }
                try
                {
                    section = await reader.ReadNextSectionAsync();//用于读取Http请求中的下一个section数据
                }
                catch (Exception ex)
                {

                    section = null;
                }
                finally
                {
                    if (section == null || section.Headers.Count <= 0 || string.IsNullOrEmpty(section.ContentDisposition))
                    {
                        section = null;//不然死循环
                    }

                }

            }


            var formValueProvider = new FormValueProvider(
             BindingSource.Form,
             new FormCollection(formAccumulator.GetResults()),
             CultureInfo.CurrentCulture);



            return formValueProvider;
        }


        #region 文件帮助工具类
        /// <summary>
        /// 创建文件保存路径
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="contentDisposition">文件描述</param>
        /// <returns></returns>
        public static string GetCurrFilePath(HttpRequest request, ContentDispositionHeaderValue contentDisposition, UploadFileInfo uploadFile)
        {
            var fdname = GetFolderName(request, uploadFile);
            var fileName = MultipartRequestHelper.GetFileName(contentDisposition);
            var targetDir = GetFilePath(request, uploadFile);
            string sourceFilePath = Path.Combine(targetDir, fileName);//原始文件名拼接文件路径
            var extension = Path.GetExtension(sourceFilePath);//文件扩展
            var guistr = Guid.NewGuid().ToString();
            var guidname = $"{guistr}{extension}";
            var filepath = Path.Combine(targetDir, guidname);
            uploadFile.SourceFileName = fileName;
            uploadFile.GuidFileName = guidname;
            uploadFile.FolderPath = $"{fdname}/{guidname}";
            uploadFile.FolderName = fdname;
            uploadFile.NotExtenFileName = guistr;
            return filepath;
        }

        ///// <summary>
        ///// 创建文件保存路径
        ///// </summary>
        ///// <param name="request">请求对象</param>
        ///// <param name="contentDisposition">文件描述</param>
        ///// <returns></returns>
        //public static string GetCurrFilePath(HttpRequest request, ContentDispositionHeaderValue contentDisposition)
        //{
        //    var fileName = MultipartRequestHelper.GetFileName(contentDisposition);
        //    var targetDir = GetFilePath(request);
        //    string sourceFilePath = Path.Combine(targetDir, fileName);//原始文件名拼接文件路径
        //    var extension = Path.GetExtension(sourceFilePath);//文件扩展
        //    var guistr = Guid.NewGuid().ToString();
        //    var filepath = Path.Combine(targetDir, $"{guistr}{extension}");
        //    return filepath;
        //}

        /// <summary>
        /// 获取编码
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        private static Encoding GetEncoding(MultipartSection section)
        {
            Microsoft.Net.Http.Headers.MediaTypeHeaderValue mediaType;
            var hasMediaTypeHeader = Microsoft.Net.Http.Headers.MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);
            // UTF-7 is insecure and should not be honored. UTF-8 will succeed in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }
            return mediaType.Encoding;
        }


        /// <summary>
        /// 根据传入标识获取文件夹名称
        /// </summary>
        /// <returns></returns>
        public static string GetFolderName(HttpRequest request, UploadFileInfo uploadFile)
        {
            var folder = 0;
            try
            {
                 folder = Convert.ToInt32(request.Form["folder"]);
                 
            }
            catch (Exception)
            {


            }
            folder = 1;//头像文件夹
            uploadFile.FolderIndex = folder;
            var folderName = EmunUtility.GetDesc(typeof(DevFolderEnums), folder);
            return folderName;
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        public static string GetFilePath(HttpRequest request, UploadFileInfo uploadFile)
        {
            var fdname= GetFolderName(request, uploadFile);
            var TarDirpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads", fdname);

            if (!Directory.Exists(TarDirpath))
            {
                Directory.CreateDirectory(TarDirpath);
            }
            return TarDirpath;
        }


        #endregion 文件帮助工具类
        #region 下载使用
        public static DownLoadInfo Download(string fileName)
        {
            var addrUrl = fileName;
            var stream = System.IO.File.OpenRead(addrUrl);
            string exten = Path.GetExtension(fileName);
            string fileExt = exten.Substring(exten.IndexOf('.'));//需要不含.
            //获取文件的ContentType
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];
            //return File(stream, memi, Path.GetFileName(addrUrl));

            return new DownLoadInfo
            {
                NfFileStream = stream,
                Memi = memi,
                FileName = Path.GetFileName(addrUrl)
            };
        }
        #endregion



    }
}
