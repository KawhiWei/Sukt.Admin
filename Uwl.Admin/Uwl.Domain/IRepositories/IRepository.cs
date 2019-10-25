using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model;

namespace Uwl.Domain.IRepositories
{
    /// <summary>
    /// 领域层，关注业务接口
    /// </summary>
    //创建仓储层的接口定义
    public interface IRepository
    {

    }
    /// <summary>
    /// 定义仓储层的泛型接口，包含CRUD方法
    /// </summary>
    /// <typeparam name="TEntity">实体模型</typeparam>
    /// <typeparam name="TprimaryKey">主键模型</typeparam>
    public interface IRepository<TEntity,TprimaryKey>:IRepository where TEntity:Entity<TprimaryKey>
    {
        #region 线程同步执行CRUD接口定义
        //IEnumerable<TEntity> QueryPage(int pageIndex, int pageSize);
        /// <summary>
        /// 同步执行获取所有实体集合
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();
        /// <summary>
        /// 同步执行根据查询条件获取指定条件的实体集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        //IQueryable<TEntity> GetAllList(string sql);
        /// <summary>
        /// 同步执行根据传入的主键获取一个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetModel(TprimaryKey id);
        /// <summary>
        /// 同步执行获取第一个实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 同步执行添加数据接口方法定义
        /// </summary>
        /// <param name="entity">需要添加的实体类</param>
        /// <param name="autoSave">是否自动提交到数据库</param>
        /// <returns></returns>
        bool Insert(TEntity entity, bool autoSave = true);
        /// <summary>
        /// 同步执行修改数据库接口方法定义
        /// </summary>
        /// <param name="entity">需要修改的实体类</param>
        /// <param name="autoSave">是否自动提交到数据库</param>
        /// <returns></returns>
        bool Update(TEntity entity, bool autoSave = true);
        /// <summary>
        /// 同步执行批量更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        bool Update(System.Collections.Generic.List<TEntity> entity, bool autoSave = true);
        /// <summary>
        /// 同步更新部分字段
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        int UpdateNotQuery(TEntity entity, params Expression<Func<TEntity, object>>[] properties);
        /// <summary>
        /// 同步执行根据实体删除数据方法
        /// </summary>
        /// <param name="entity">需要的实体类</param>
        /// <param name="autoSave">是否自动提交到数据库</param>
        void Delete(TEntity entity, bool autoSave = true);
        /// <summary>
        /// 同步执行根据主键删除数据方法
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <param name="autoSave">是否自动提交到数据库</param>
        void Delete(TprimaryKey id, bool autoSave = true);
        #endregion

        #region 分页接口定义
        /// <summary>
        /// 分页方法接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="predicate"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        IQueryable<TEntity> PageBy(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据查询条件获取总条数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region 异步线程等待结果执行CRUD接口定义
        /// <summary>
        /// 异步等待执行获取所有实体集合
        /// </summary>
        /// <returns></returns>
        Task<System.Collections.Generic.List<TEntity>> GetAllListAsync();
        ///// <summary>
        ///// 异步等待执行获取所有符合条件的实体集合
        ///// </summary>
        ///// <returns></returns>
        Task<System.Collections.Generic.List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 异步等待执行结果添加实体到数据库
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<bool> InsertAsync(TEntity entity, bool autoSave = true);
        /// <summary>
        /// 同步执行新增或更新数据库接口方法定义
        /// </summary>
        /// <param name="entity">需要的实体类</param>
        /// <param name="autoSave">是否自动提交到数据库</param>
        /// <returns></returns>
        Task InsertAsync(List<TEntity> entity, bool autoSave = true);
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity, bool autoSave = true);
        /// <summary>
        /// 异步执行修改数据库接口方法定义
        /// </summary>
        /// <param name="entity">需要修改的实体类</param>
        /// <param name="FilterSpecifiedColumnNotUpdated">过滤某列不更新</param>
        /// <param name="autoSave">是否自动提交到数据库</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity, bool autoSave = true, params string[] FilterSpecifiedColumnNotUpdated);
        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(System.Collections.Generic.List<TEntity> entity, bool autoSave = true);

        Task<int> UpdateNotQueryAsync(TEntity entity, params Expression<Func<TEntity, object>>[] properties);
        /// <summary>
        /// 异步批量删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task Delete(System.Collections.Generic.List<TEntity> entity, bool autoSave = true);
        /// <summary>
        /// 异步根据ID获取一个实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<TEntity> GetModelAsync(TprimaryKey Id);
        #endregion
    }
    /// <summary>
    /// 默认仓储层的Guid主键，并且约束传入的实体必须继承于Entity基类实体
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>: IRepository<TEntity,Guid> where TEntity:Entity
    {

    }
}
