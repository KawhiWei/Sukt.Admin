using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Uwl.Data.EntityFramework.Uwl_DbContext;
using Uwl.Data.Model;
using Uwl.Domain.IRepositories;

namespace Uwl.Data.EntityFramework.RepositoriesBase
{
    //Uwl.Data.EntityFramework.RepositoriesBase为仓储层接口方法实现
    //访问数据库基类
    /// <summary>
    /// 定义一个仓储接口抽象基类，继承与仓储接口
    /// </summary>
    public abstract class UwlRepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        //定义一个数据库上下文访问对象
        protected readonly UwlDbContext _uwldbContext;
        /// <summary>
        /// 通过构造函数注入得到数据库上下文对象实例
        /// </summary>
        /// <param name="coreDbContext"></param>
        public UwlRepositoryBase(UwlDbContext uwlDbContext)
        {
            _uwldbContext = uwlDbContext;
        }
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public List<TEntity> GetAllList()
        {
            return _uwldbContext.Set<TEntity>().ToList();
        }
        /// <summary>
        /// 根据lambda表达式获取的实体集合
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public IQueryable<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return _uwldbContext.Set<TEntity>().AsNoTracking().Where(predicate);
        }
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        public TEntity GetModel(TPrimaryKey id)
        {
            return _uwldbContext.Set<TEntity>().FirstOrDefault(CreateEqualityExpressionForId(id));
        }
        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _uwldbContext.Set<TEntity>().FirstOrDefault(predicate);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public TEntity Insert(TEntity entity, bool autoSave = true)
        {
            _uwldbContext.Set<TEntity>().Add(entity);
            if (autoSave)
                Save();
            return entity;
        }
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public TEntity Update(TEntity entity, bool autoSave = true)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 新增或者更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public TEntity InsertOrUpdate(TEntity entity, bool autoSave = true)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <returns></returns>
        public void Delete(TEntity entity, bool autoSave = true)
        {
            _uwldbContext.Set<TEntity>().Remove(entity);
            if (autoSave)
                Save();
        }
        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <param name="autoSave">是否自动保存</param>
        public void Delete(TPrimaryKey id, bool autoSave = true)
        {
            _uwldbContext.Set<TEntity>().Remove(GetModel(id));
            if (autoSave)
                Save();
        }
        /// <summary>
        /// 分页方法的实现,配合IQueryable Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable(); 可以实现真分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="predicate"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IQueryable<TEntity> PageBy<T>(int pageIndex,int pageSize, Expression<Func<TEntity, bool>> predicate)
        {
            return _uwldbContext.Set<TEntity>().Where(predicate).Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
        }
        public int Count<T>(Expression<Func<TEntity, bool>> predicate)
        {
            return _uwldbContext.Set<TEntity>().Where(predicate).Count();
        }
        /// <summary>
        /// 事务性保存
        /// </summary>
        public void Save()
        {
            _uwldbContext.SaveChanges();
        }
        /// <summary>
        /// 根据主键构建判断表达式
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaparam = Expression.Parameter(typeof(TEntity));
            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaparam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );
            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaparam);
        }
        
        //public IEnumerable<TEntity> GetQueryPage(int pageIndex, int pageSize)
        //{
        //    //_uwldbContext.Set<TEntity>().AsNoTracking().;
        //    throw new NotImplementedException();
        //}
    }
    // <summary>
    /// 主键为Guid类型的仓储基类?????减少不必要的参数传递
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class CoreRepositoryBase<TEntity> : UwlRepositoryBase<TEntity, Guid> where TEntity : Entity
    {
        public CoreRepositoryBase(UwlDbContext uwlDbContext) : base(uwlDbContext)
        {

        }
    }
}
