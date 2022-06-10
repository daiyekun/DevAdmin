using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.ViewModel.ExtendModel
{

    /// <summary>
    /// vben 请求页参数
    /// </summary>
     public class BasicPageParams
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int page { get; set; }=0;
        /// <summary>
        /// 页条数
        /// </summary>
        public int pageSize { get; set; } = 20;//每页条数
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string? keyword { get; set; }
      

    }
    public interface ReqPage
    {

    }

    public class PageParams: BasicPageParams
    {


    }


    /// <summary>
    /// 请求返回列表数据
    /// </summary>
    public class ResultPageData<T> where T : class, new()
    {
        /// <summary>
        /// 返回页数据
        /// </summary>
        public List<T>? items { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public int total { get; set; } = 0;
        ///// <summary>
        ///// 状态标识
        ///// </summary>
        //public int code { get; set; } = 0;
        ///// <summary>
        ///// 消息
        ///// </summary>
        //public string message { get; set; } = "ok";
        /// <summary>
        /// 页码
        /// </summary>
        public int page { get; set; } = 0;
        /// <summary>
        /// 分页条数
        /// </summary>
        public int pageSize { get; set; } = 0;


    }
    /// <summary>
    /// 返回父类
    /// </summary>
    public class BaseResult
    {
        /// <summary>
        /// 状态标识
        /// </summary>
        public int code { get; set; } = 0;
        /// <summary>
        /// 消息
        /// </summary>
        public string message { get; set; } = "ok";
    }
    /// <summary>
    /// 返回result
    /// </summary>
    /// <typeparam name="T">实体对象</typeparam>
    public class ResultData<T>: BaseResult where T : class, new()
    {
        public ResultPageData<T>?  result { get; set; }

      

    }
    /// <summary>
    /// 新增删除返回对象
    /// </summary>
    public class ResultData: BaseResult
    {
       
    }

    /// <summary>
    /// 返回列表
    /// </summary>
    public class ResultListData<T> : BaseResult where T : class, new ()
    {
        public List<T>? result { get; set; }
    }
}
