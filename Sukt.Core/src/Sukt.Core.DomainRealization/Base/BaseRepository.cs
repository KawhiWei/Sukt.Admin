using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukt.Core.Domain.ISuktBaseRepository;
using Sukt.Core.EntityFrameworkCore;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.DomainRealization.Base
{
    public class BaseRepository<TEntity, Tkey> : IEFCoreRepository<TEntity, Tkey>
        where TEntity : class, IEntity<Tkey> where Tkey : IEquatable<Tkey>
    {
        public BaseRepository(IServiceProvider serviceProvider)
        {
            UnitOfWork = (serviceProvider.GetService(typeof(IUnitOfWork)) as IUnitOfWork);//获取工作单元实例
            _dbContext = UnitOfWork.GetDbContext();
            _dbSet = _dbContext.Set<TEntity>();

        }
        /// <summary>
        /// 表对象
        /// </summary>
        private readonly DbSet<TEntity> _dbSet = null;
        /// <summary>
        /// 上下文
        /// </summary>
        private readonly DbContext _dbContext = null;
        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger _logger = null;
        /// <summary>
        /// 
        /// </summary>
        private readonly IPrincipal _principal;
        /// <summary>
        /// 工作单元
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }
        #region Query        
        /// <summary>
        /// 获取 不跟踪数据更改（NoTracking）的查询数据源
        /// </summary>
        public virtual IQueryable<TEntity> NoTrackEntities => _dbSet.AsNoTracking();
        /// <summary>
        /// 获取 跟踪数据更改（Tracking）的查询数据源
        /// </summary>
        public virtual IQueryable<TEntity> TrackEntities => _dbSet;
        /// <summary>
        /// 根据ID得到实体
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public virtual TEntity GetById(Tkey primaryKey) => _dbSet.Find(primaryKey);
        /// <summary>
        /// 异步根据ID得到实体
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByIdAsync(Tkey primaryKey) => await _dbSet.FindAsync(primaryKey);
        /// <summary>
        /// 将查询的实体转换为DTO输出
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public virtual TDto GetByIdToDto<TDto>(Tkey primaryKey) where TDto : class, new() => this.GetById(primaryKey).MapTo<TDto>();
        /// <summary>
        /// 异步将查询的实体转换为DTO输出
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public virtual async Task<TDto> GetByIdToDtoAsync<TDto>(Tkey primaryKey) where TDto : class, new() => (await this.GetByIdAsync(primaryKey)).MapTo<TDto>();
        /// <summary>
        /// 查询不跟踪数据源
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> NoTrackQuery(Expression<Func<TEntity, bool>> predicate)
        {
            predicate.NotNull(nameof(predicate));
            return this.NoTrackEntities.Where(predicate);
        }
        /// <summary>
        /// 查询不跟踪数据源
        /// </summary>
        /// <typeparam name="TResult">返回实体</typeparam>
        /// <param name="predicate">条件</param>
        /// <param name="selector">数据筛选表达式</param>
        /// <returns>返回查询后数据源</returns>
        public virtual IQueryable<TResult> Query<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector)
        {
            predicate.NotNull(nameof(predicate));
            selector.NotNull(nameof(selector));
            return this.NoTrackEntities.Where(predicate).Select(selector);
        }
        /// <summary>
        /// 查询跟踪数据源
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>返回查询后数据源</returns>
        public virtual IQueryable<TEntity> TrackQuery(Expression<Func<TEntity, bool>> predicate)
        {
            predicate.NotNull(nameof(predicate));
            return this.TrackEntities.Where(predicate);
        }
        #endregion

        #region Insert
        /// <summary>
        /// 同步批量添加实体
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public virtual int Insert(params TEntity[] entitys)
        {
            entitys.NotNull(nameof(entitys));
            entitys = CheckInsert(entitys);
            _dbSet.AddRange(entitys);
            return _dbContext.SaveChanges();
        }
        /// <summary>
        /// 异步添加单条实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            entity.NotNull(nameof(entity));
            entity = CheckInsert(entity);
            await _dbSet.AddAsync(entity);
            return await _dbContext.SaveChangesAsync();

        }
        /// <summary>
        /// 批量异步添加实体
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(TEntity[] entitys)
        {
            entitys.NotNull(nameof(entitys));
            entitys = CheckInsert(entitys);
            await _dbSet.AddRangeAsync(entitys);
            return await _dbContext.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public virtual int Delete(params TEntity[] entitys)
        {
            throw new NotImplementedException();
        }

        public virtual Task<OperationResponse> DeleteAsync(Tkey primaryKey)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> DeleteBatchAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update
        /// <summary>
        /// 同步逐条更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Update(TEntity entity)
        {
            entity.NotNull(nameof(entity));
            entity = CheckUpdate(entity);
            _dbSet.Update(entity);
            int count = _dbContext.SaveChanges();
            return count;
        }
        /// <summary>
        /// 异步逐条更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            entity.NotNull(nameof(entity));
            entity = CheckUpdate(entity);
            _dbSet.Update(entity);
            int count = await _dbContext.SaveChangesAsync();
            return count;
        }
        /// <summary>
        /// 异步批量更新
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(TEntity[] entitys)
        {
            entitys.NotNull(nameof(entitys));
            entitys = CheckUpdate(entitys);
            _dbSet.UpdateRange(entitys);
            int count = await _dbContext.SaveChangesAsync();
            return count;
        }
        #endregion


        #region 帮助方法
        /// <summary>
        /// 检查删除
        /// </summary>
        /// <param name="entitys">实体集合</param>
        /// <returns></returns>
        private void CheckDelete(IEnumerable<TEntity> entitys)
        {
            foreach (var entity in entitys)
            {
                this.CheckDelete(entity);
            }
        }

        /// <summary>
        /// 检查删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        private void CheckDelete(TEntity entity)
        {

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                ISoftDelete softDeletabl = (ISoftDelete)entity;
                softDeletabl.IsDeleted = true;
                var entity1 = (TEntity)softDeletabl;

                this._dbContext.Update(entity1);
            }
            else
            {
                this._dbContext.Remove(entity);
            }
        }

        ///// <summary>
        ///// 检查软删除接口
        ///// </summary>
        ///// <param name="entity">要检查的实体</param>
        ///// <returns>返回检查好的实体</returns>
        //private TEntity CheckISoftDelete(TEntity entity)
        //{
        //    if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
        //    {
        //        ISoftDelete softDeletableEntity = (ISoftDelete)entity;
        //        softDeletableEntity.IsDeleted = true;
        //        var entity1 = (TEntity)softDeletableEntity;
        //        return entity1;
        //    }
        //    return entity;
        //}

        /// <summary>
        /// 检查创建
        /// </summary>
        /// <param name="entitys">实体集合</param>
        /// <returns></returns>

        private TEntity[] CheckInsert(TEntity[] entitys)
        {

            for (int i = 0; i < entitys.Length; i++)
            {
                var entity = entitys[i];
                entitys[i] = CheckInsert(entity);
            }
            return entitys;
        }


        /// <summary>
        /// 检查创建时间
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        private TEntity CheckInsert(TEntity entity)
        {

            var creationAudited = entity.GetType().GetInterface(/*$"ICreationAudited`1"*/typeof(ICreatedAudited<>).Name);
            if (creationAudited == null)
            {
                return entity;
            }

            var typeArguments = creationAudited?.GenericTypeArguments[0];
            var fullName = typeArguments?.FullName;
            if (fullName == typeof(Guid).FullName)
            {
                entity = CheckICreationAudited<Guid>(entity);

            }

            return entity;

        }

        private TEntity CheckICreationAudited<TUserKey>(TEntity entity)
           where TUserKey : struct, IEquatable<TUserKey>
        {
            if (!entity.GetType().IsBaseOn(typeof(ICreatedAudited<>)))
            {
                return entity;
            }

            ICreatedAudited<TUserKey> entity1 = (ICreatedAudited<TUserKey>)entity;
            //entity1.CreatedId = (TUserKey) Guid.NewGuid()//_principal?.Identity.GetUesrId<TUserKey>();
            entity1.CreatedAt = DateTime.Now;
            return (TEntity)entity1;
        }
        /// <summary>
        /// 检查最后修改时间
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        private TEntity[] CheckUpdate(TEntity[] entitys)
        {

            for (int i = 0; i < entitys.Length; i++)
            {
                var entity = entitys[i];
                entitys[i] = CheckUpdate(entity);
            }
            return entitys;
        }

        /// <summary>
        /// 检查最后修改时间
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        private TEntity CheckUpdate(TEntity entity)
        {

            var creationAudited = entity.GetType().GetInterface(/*$"ICreationAudited`1"*/typeof(IModifyAudited<>).Name);
            if (creationAudited == null)
            {
                return entity;
            }

            var typeArguments = creationAudited?.GenericTypeArguments[0];
            var fullName = typeArguments?.FullName;
            if (fullName == typeof(Guid).FullName)
            {
                entity = CheckIModificationAudited<Guid>(entity);

            }

            return entity;

        }
        /// <summary>
        /// 检查最后修改时间
        /// </summary>
        /// <typeparam name="TUserKey"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity CheckIModificationAudited<TUserKey>(TEntity entity)
      where TUserKey : struct, IEquatable<TUserKey>
        {
            if (!entity.GetType().IsBaseOn(typeof(IModifyAudited<>)))
            {
                return entity;
            }

            IModifyAudited<TUserKey> entity1 = (IModifyAudited<TUserKey>)entity;
            //entity1.LastModifyId = _principal?.Identity?.GetUesrId<TUserKey>();
            entity1.LastModifedAt = DateTime.Now;
            return (TEntity)entity1;
        }
        #endregion
        /// <summary>
        /// 检查最后修改时间
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        //private Expression<Func<TEntity, TEntity>> CheckUpdate(Expression<Func<TEntity, TEntity>> updateExpression)
        //{

        //    var creationAudited = typeof(TEntity).GetType().GetInterface(/*$"ICreationAudited`1"*/typeof(IModificationAudited<>).Name);
        //    if (creationAudited == null)
        //    {
        //        return updateExpression;
        //    }

        //    var typeArguments = creationAudited?.GenericTypeArguments[0];
        //    var fullName = typeArguments?.FullName;
        //    if (fullName == typeof(Guid).FullName)
        //    {
        //        return CheckIModificationAudited<Guid>(updateExpression);

        //    }

        //    return updateExpression;

        //}

        //  /// <summary>
        //  /// 检查最后修改时间
        //  /// </summary>
        //  /// <typeparam name="TUserKey"></typeparam>
        //  /// <param name="updateExpression"></param>
        //  /// <returns></returns>
        //  public Expression<Func<TEntity, TEntity>> CheckIModificationAudited<TUserKey>(Expression<Func<TEntity, TEntity>> updateExpression)
        //where TUserKey : struct, IEquatable<TUserKey>
        //  {
        //      if (!typeof(TEntity).IsBaseOn(typeof(IModificationAudited<>)))
        //      {
        //          return updateExpression;
        //      }
        //      List<MemberBinding> newMemberBindings = new List<MemberBinding>();
        //      ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntity), "o"); //参数

        //      var memberBindings = ((MemberInitExpression)updateExpression?.Body)?.Bindings;
        //      var propertyInfos = typeof(IModificationAudited<TUserKey>).GetProperties();
        //      if (memberBindings?.Count > 0)
        //      {

        //          var propertyNames = propertyInfos.Select(o => o.Name);

        //          foreach (var memberBinding in memberBindings.Where(o => !propertyNames.Contains(o.Member.Name)))
        //          {
        //              newMemberBindings.Add(memberBinding);
        //          }
        //      }
        //      foreach (var propertyInfo in propertyInfos)
        //      {
        //          var propertyName = propertyInfo.Name;
        //          ConstantExpression constant = Expression.Constant(DateTime.Now);
        //          if (propertyName == nameof(IModificationAudited<TUserKey>.LastModifierTime))
        //          {
        //              var memberAssignment = Expression.Bind(propertyInfo, constant); //绑定属性
        //              newMemberBindings.Add(memberAssignment);
        //          }
        //          else if (propertyName == nameof(IModificationAudited<TUserKey>.LastModifierUserId))
        //          {
        //              constant = Expression.Constant(_principal?.Identity?.GetUesrId<TUserKey>(), typeof(TUserKey));
        //              var memberAssignment = Expression.Bind(propertyInfo, constant); //绑定属性
        //              newMemberBindings.Add(memberAssignment);
        //          }
        //      }


        //      //创建实体
        //      var newEntity = Expression.New(typeof(TEntity));
        //      var memberInit = Expression.MemberInit(newEntity, newMemberBindings.ToArray()); //成员初始化
        //      Expression<Func<TEntity, TEntity>> updateExpression1 = Expression.Lambda<Func<TEntity, TEntity>> //生成要更新的Expression
        //      (
        //         memberInit,
        //         new ParameterExpression[] { parameterExpression }
        //      );

        //      return updateExpression1;


        //}


    }
}
