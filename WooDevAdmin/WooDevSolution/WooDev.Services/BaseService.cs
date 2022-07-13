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
        protected ISqlSugarClient DbClient;
        
        public BaseService(ISqlSugarClient client)
        {
            this.DbClient = client;
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
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="t">修改对象</param>
        /// <returns>true:成功/false:失败</returns>
        public int Update(List<T> updates)
        {

            return DbClient.Updateable(updates).ExecuteCommand();

        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public ISugarQueryable<T> Query()
        {
            return DbClient.Queryable<T>();
        }
        /// <summary>
        /// 查询所有并返回集合
        /// </summary>
        /// <returns></returns>
        public List<T> AllList() 
        {
            return DbClient.Queryable<T>().ToList();
        }
        /// <summary>
        /// 根据ID获取对象
        /// </summary>
        /// <param name="Id">当前查询ID</param>
        /// <returns>实体对象</returns>
        public T InSingle(int Id)
        {
           return DbClient.Queryable<T>().InSingle(Id);
        }
        /// <summary>
        /// 执行sql语句
        /// 改麻烦，能不用就别用了
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <returns></returns>
        public int  ExecuteCommand(string sql)
        {
            return DbClient.Ado.ExecuteCommand(sql);
        }
        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns>ISugarQueryable<T></returns>
        public ISugarQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
           return DbClient.Queryable<T>().Where(predicate);
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">软删除ID字符传 “,”分开</param>
        public int SoftDelete(string Ids)
        {
            string sqlstr = $"UPDATE {typeof(T).Name} SET IS_DELETE=1 WHERE ID IN({Ids})";
            return DbClient.Ado.ExecuteCommand(sqlstr);
        }





    }
}
