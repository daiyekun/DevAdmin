using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WooDev.Common.Models
{
    /// <summary>
    /// 分页对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageInfo<T>
        where T : class, new()

    {
        /// <summary>
        /// 页码
        /// </summary>
        private int _pageIndex;
        /// <summary>
        /// 每页条数
        /// </summary>
        private int _pageSize;
        /// <summary>
        /// 总记录数
        /// </summary>
        private int _totalCount;
        /// <summary>
        /// 数据
        /// </summary>
        private IList<T> _pageList;
        /// <summary>
        /// Dto数据
        /// </summary>
       // private IList<T> _pageDtoList;

        public virtual int PageIndex { get => _pageIndex <= 0 ? 1 : _pageIndex; set => _pageIndex = value; }
        public virtual int PageSize { get => _pageSize <= 0 ? 10: _pageSize; set => _pageSize = value; }
        public virtual int TotalCount { get => _totalCount; set => _totalCount = value; }
        public virtual IList<T> PageList { get => _pageList; set => _pageList = value; }
        

        public PageInfo()
        {


        }
        public PageInfo(int pageIndex, int pageSize)
        {
            _pageIndex = pageIndex;
            _pageSize = pageSize;
        }


    }
    /// <summary>
    /// 不分页
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class NoPageInfo<T>
        : PageInfo<T>
        where T : class, new()

    {
        public NoPageInfo()
            : base()
        {
        }

        /// <summary>
        /// 分页数
        /// </summary>
        public override int PageSize
        {
            get;
            set;
        }
        /// <summary>
        /// 分页数
        /// </summary>
        public override int PageIndex
        {
            get;
            set;
        }


    }
    /// <summary>
    /// 临时存储数据页对象
    /// </summary>
    public class TempPage<T>
    {
        /// <summary>
        /// 数据总条数
        /// </summary>
        public int DataCount { get; set; }
        /// <summary>
        /// 查询对象
        /// </summary>
        public IQueryable<T> IqueryT { get; set; }

    }


    }
