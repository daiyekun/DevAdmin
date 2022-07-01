using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WooDev.IServices
{

    /// <summary>
    /// 父类接口
    /// </summary>
    /// <typeparam name="T">实体类</typeparam>
   public interface IBaseService<T> where T : class, new()
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="info">增加实体</param>
        /// <returns></returns>
        T Add(T info);
        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="tList">集合对象</param>
        /// <returns></returns>
        IEnumerable<T> Add(IEnumerable<T> tList);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int Delete(T t);
        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns></returns>
        int Delete(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t">修改对象</param>
        /// <returns>true:成功/false:失败</returns>
        int Update(T t);
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="t">修改对象</param>
        /// <returns>true:成功/false:失败</returns>
        int Update(List<T> updates);
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        ISugarQueryable<T> Query();
        /// <summary>
        /// 查询所有并返回集合
        /// </summary>
        /// <returns></returns>
        List<T> AllList();
        /// <summary>
        /// 根据ID获取对象
        /// </summary>
        /// <param name="Id">当前查询ID</param>
        /// <returns>实体</returns>
        T InSingle(int Id);
        /// <summary>
        /// 执行sql语句
        /// 改麻烦，能不用就别用了
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <returns></returns>
        int ExecuteCommand(string sql);

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns>ISugarQueryable<T></returns>
        ISugarQueryable<T> Query(Expression<Func<T, bool>> predicate);
    }
}
