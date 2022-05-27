using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace WooDev.Services
{

    /// <summary>
    /// 数据库操作父类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T>
        where T : class, new()
    {
        protected SqlSugarClient  DbClient { get; private set; }
        public BaseService(SqlSugarClient sqlSugarClient)
        {
            this.DbClient = sqlSugarClient;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t">新增实体对象</param>
        /// <returns></returns>
        public T Add(T t)
        {

          return DbClient.Insertable<T>(t).ExecuteReturnEntity();
        }
        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="tList">集合对象</param>
        /// <returns></returns>
        public IEnumerable<T> Add(IEnumerable<T> tList)
        {
            DbClient.Insertable<T>(tList).ExecuteCommand();
           
            return tList;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Delete(T t)
        {

            return DbClient.Deleteable<T>(t).ExecuteCommand();
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns></returns>
        public int Delete(Expression<Func<T, bool>> predicate)
        {

            return DbClient.Deleteable<T>(predicate).ExecuteCommand();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t">修改对象</param>
        /// <returns>true:成功/false:失败</returns>
        public int Update(T t)
        {
            
           return DbClient.Updateable<T>(t).ExecuteCommand();

        }

       

    }
}
