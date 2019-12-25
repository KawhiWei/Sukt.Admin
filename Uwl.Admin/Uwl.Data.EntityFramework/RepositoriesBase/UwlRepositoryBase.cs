using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

using Uwl.Data.Model;
using Uwl.Domain.IRepositories;
//using Z.EntityFramework.Plus;

namespace Uwl.Data.EntityFramework.RepositoriesBase
{
    //Uwl.Data.EntityFramework.RepositoriesBase为仓储层接口方法实现
    //访问数据库基类
    /// <summary>
    /// 定义一个仓储接口抽象基类，继承与仓储接口
    /// </summary>
    public abstract class UwlRepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {

        private readonly IUnitofWork _unitofWork;
        internal readonly DbContext _uwldbContext;

        private readonly DbSet<TEntity> _dbSet;

        public UwlRepositoryBase(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            _uwldbContext=_unitofWork.GetDbContext();
            _dbSet = _uwldbContext.Set<TEntity>();
        }

        #region 线程同步执行CRUD接口实现
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }
        /// <summary>
        /// 根据lambda表达式获取的实体集合
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        public TEntity GetModel(TPrimaryKey Id)
        {
            return _dbSet.FirstOrDefault(CreateEqualityExpressionForId(Id));
        }
        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public bool Insert(TEntity entity, bool autoSave = true)
        {
            try
            {
                _dbSet.Add(entity);
                if (autoSave)
                    Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public bool Insert(List<TEntity> entity, bool autoSave = true)
        {
            try
            {
                _dbSet.AddRange(entity);
                if (autoSave)
                    this.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public bool Update(TEntity entity, bool autoSave = true)
        {
            try
            {
                int excuter = 0;
                _dbSet.Update(entity); ;
                //_dbSet.Update<TEntity>(entity);
                if (autoSave)
                    excuter=Save();
                return excuter>0;
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
        public int UpdateNotQuery(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            var dbEntityEntry = _uwldbContext.Entry<TEntity>(entity);
            if (properties.Any())
            {
                foreach (var property in properties)
                {
                    dbEntityEntry.Property(property).IsModified = true;
                }
            }
            else
            {
                foreach (var rawProperty in dbEntityEntry.Entity.GetType().GetTypeInfo().DeclaredProperties)
                {
                    var originalValue = dbEntityEntry.Property(rawProperty.Name).OriginalValue;
                    var currentValue = dbEntityEntry.Property(rawProperty.Name).CurrentValue;
                    foreach (var property in properties)
                    {
                        if (originalValue != null && !originalValue.Equals(currentValue))
                            dbEntityEntry.Property(property).IsModified = true;
                    }

                }
            }
            return Save();
        }
        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public bool Update(System.Collections.Generic.List<TEntity> entity, bool autoSave = true)
        {
            try
            {
                _dbSet.UpdateRange(entity);
                if (autoSave)
                    Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <returns></returns>
        public void Delete(TEntity entity, bool autoSave = true)
        {
            _dbSet.Remove(entity);
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
            _dbSet.Remove(GetModel(id));
            if (autoSave)
                Save();
        }
        /// <summary>
        /// 事务性保存
        /// </summary>
        public int Save()
        {
           return _uwldbContext.SaveChanges();
        }
        #endregion

        #region 分页接口实现
        /// <summary>
        /// 分页方法的实现,配合IQueryable Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable(); 可以实现真分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="predicate"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IQueryable<TEntity> PageBy(int pageIndex,int pageSize, Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).Count();
        }
        #endregion

        #region 异步线程等待结果执行CRUD接口定义
        /// <summary>
        /// 异步等待执行结果获取所有实体集合
        /// </summary>
        /// <returns></returns>
        public async Task<System.Collections.Generic.List<TEntity>> GetAllListAsync()
        {
            return await _dbSet.ToListAsync();
        }
        /// <summary>
        /// 异步根据lambda表达式获取的实体集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<System.Collections.Generic.List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public async  Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
        /// <summary>
        /// 异步等待执行结果添加实体到数据库
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(TEntity entity, bool autoSave = true)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                if (autoSave)
                    await SaveAsync();
                return true;
            }
            catch (Exception)
            {
                return true;
            }
            
        }
        /// <summary>
        /// 异步更新单个实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(TEntity entity, bool autoSave = true)
        {
            _uwldbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            try
            {
                int excuter = 0;
                _dbSet.Update(entity).State=EntityState.Modified;
                if (autoSave)
                    excuter =await SaveAsync();
                return excuter>0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


        public  async Task<int> UpdateNotQueryAsync(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            //_dbSet.Where().BulkUpdateAsync<TEntity>(entity);

            var dbEntityEntry = _uwldbContext.Entry<TEntity>(entity);
            if (properties.Any())
            {
                foreach (var property in properties)
                {
                    dbEntityEntry.Property(property).IsModified = true;
                }
            }
            else
            {
                foreach (var rawProperty in dbEntityEntry.Entity.GetType().GetTypeInfo().DeclaredProperties)
                {
                    var originalValue = dbEntityEntry.Property(rawProperty.Name).OriginalValue;
                    var currentValue = dbEntityEntry.Property(rawProperty.Name).CurrentValue;
                    foreach (var property in properties)
                    {
                        if (originalValue != null && !originalValue.Equals(currentValue))
                            dbEntityEntry.Property(property).IsModified = true;
                    }

                }
            }
            return  await  _uwldbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 异步执行修改数据库接口方法定义
        /// </summary>
        /// <param name="entity">需要修改的实体类</param>
        /// <param name="FilterSpecifiedColumnNotUpdated">过滤某列不更新</param>
        /// <param name="autoSave">是否自动提交到数据库</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(TEntity entity, bool autoSave = true, params string[] FilterSpecifiedColumnNotUpdated)
        {

           await  Task.CompletedTask;
            return false;
            //if (FilterSpecifiedColumnNotUpdated != null && FilterSpecifiedColumnNotUpdated.Length > 0)
            //{
            //    if (_dbSet is DbContext dbContext)
            //    {
            //        var dbEntityEntry = dbContext.Entry(entity);
            //        foreach (var columnName in FilterSpecifiedColumnNotUpdated)
            //        {
            //            foreach (var property in dbEntityEntry.OriginalValues.Properties)
            //            {
            //                if (property.Name.Equals(columnName, StringComparison.CurrentCultureIgnoreCase))
            //                {
            //                    dbEntityEntry.Property(property.Name).IsModified = false;
            //                }
            //            }
            //        }
            //    }
            //}
            //try
            //{
            //    int excuter = 0;
            //    _dbSet.Update<TEntity>(entity);
            //    if (autoSave)
            //        excuter = await SaveAsync();
            //    return excuter > 0;
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }
        /// <summary>
        /// 异步批量更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(System.Collections.Generic.List<TEntity> entity, bool autoSave = true)
        {
            try
            {
                 _dbSet.UpdateRange(entity);
                if(autoSave)
                    await SaveAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(TEntity entity, bool autoSave = true)
        {
            try
            {
                _dbSet.Remove(entity);
                if (autoSave)
                    await SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            
        }
        /// <summary>
        /// 根据主键异步获取实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<TEntity> GetModelAsync(TPrimaryKey Id)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(CreateEqualityExpressionForId(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 根据主键异步删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(TPrimaryKey Id, bool autoSave = true)
        {
            try
            {
                _dbSet.Remove(await GetModelAsync(Id));
                if (autoSave)
                    await SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }

        }
        /// <summary>
        /// 新增或者更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task InsertAsync(List<TEntity> entity, bool autoSave = true)
        {
            await _dbSet.AddRangeAsync(entity);
            if (autoSave)
            {

                await SaveAsync();
            }
        }
        public async Task Delete(List<TEntity> entity, bool autoSave = true)
        {
            _dbSet.RemoveRange(entity);
            if(autoSave)
            {

                await SaveAsync();
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _uwldbContext.SaveChangesAsync();
        }
        #endregion
  
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
    }
    ///<summary>
    /// 主键为Guid类型的仓储基类?????减少不必要的参数传递
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class CoreRepositoryBase<TEntity> : UwlRepositoryBase<TEntity, Guid> where TEntity : Entity
    {
        public CoreRepositoryBase(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
    }
}
