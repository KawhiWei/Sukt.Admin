using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukt.Core.EntityFrameworkCore;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Enums;
using Sukt.Core.Shared.Exceptions;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.HttpContextUser;
using Sukt.Core.Shared.OperationResult;
using Sukt.Core.Shared.ResultMessageConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Sukt.Core.EntityFrameworkCore
{
    public class BaseRepository<TEntity, Tkey> : IEFCoreRepository<TEntity, Tkey>
        where TEntity : class, IEntity<Tkey> where Tkey : IEquatable<Tkey>
    {
        public BaseRepository(IServiceProvider serviceProvider)
        {
            UnitOfWork = (serviceProvider.GetService(typeof(IUnitOfWork)) as IUnitOfWork);//获取工作单元实例
            _dbContext = UnitOfWork.GetDbContext();
            _dbSet = _dbContext.Set<TEntity>();
            _suktUser= (serviceProvider.GetService(typeof(ISuktUser)) as ISuktUser);//获取用户登录存储解析Token实例
            _logger = serviceProvider.GetLogger<BaseRepository<TEntity, Tkey>>();
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
        private readonly ISuktUser _suktUser;
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
        /// <summary>
        /// 以异步DTO插入实体
        /// </summary>
        /// <typeparam name="TInputDto">添加DTO类型</typeparam>
        /// <param name="dto">添加DTO</param>
        /// <param name="checkFunc">添加信息合法性检查委托</param>
        /// <param name="insertFunc">由DTO到实体的转换委托</param>
        /// <returns>操作结果</returns>
        public virtual async Task<OperationResponse> InsertAsync<TInputDto>(TInputDto dto, Func<TInputDto, Task> checkFunc = null, Func<TInputDto, TEntity, Task<TEntity>> insertFunc = null, Func<TEntity, TInputDto> completeFunc = null) where TInputDto : IInputDto<Tkey>
        {
            dto.NotNull(nameof(dto));
            try
            {
                if (checkFunc.IsNotNull())
                {
                    await checkFunc(dto);
                }
                TEntity entity = dto.MapTo<TEntity>();

                if (!insertFunc.IsNull())
                {
                    entity = await insertFunc(dto, entity);
                }
                entity = CheckInsert(entity);
                await _dbSet.AddAsync(entity);

                if (completeFunc.IsNotNull())
                {
                    dto = completeFunc(entity);
                }
                int count = await _dbContext.SaveChangesAsync();
                return new OperationResponse(count > 0 ? ResultMessage.InsertSuccess : ResultMessage.NoChangeInOperation, count > 0 ? OperationEnumType.Success : OperationEnumType.NoChanged);
            }
            catch (SuktAppException e)
            {
                return new OperationResponse(e.Message, OperationEnumType.Error);
            }
            catch (Exception ex)
            {
                return new OperationResponse(ex.Message, OperationEnumType.Error);
            }
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
        /// <summary>
        /// 以异步DTO更新实体
        /// </summary>
        /// <typeparam name="TInputDto">更新DTO类型</typeparam>
        /// <param name="dto">更新DTO</param>
        /// <param name="checkFunc">添加信息合法性检查委托</param>
        /// <param name="updateFunc">由DTO到实体的转换委托</param>
        /// <returns>操作结果</returns>
        public virtual async Task<OperationResponse> UpdateAsync<TInputDto>(TInputDto dto, Func<TInputDto, TEntity, Task> checkFunc = null, Func<TInputDto, TEntity, Task<TEntity>> updateFunc = null) where TInputDto : class, IInputDto<Tkey>, new()
        {
            dto.NotNull(nameof(dto));
            try
            {
                TEntity entity = await this.GetByIdAsync(dto.Id);

                if (entity.IsNull())
                {
                    return new OperationResponse($"该{dto.Id}键的数据不存在", OperationEnumType.QueryNull);
                }
                if (checkFunc.IsNotNull())
                {
                    await checkFunc(dto, entity);
                }
                entity = dto.MapTo(entity);
                if (!updateFunc.IsNull())
                {
                    entity = await updateFunc(dto, entity);
                }
                entity = CheckUpdate(entity);
                _dbSet.Update(entity);
                int count = await _dbContext.SaveChangesAsync();
                return new OperationResponse(count > 0 ? ResultMessage.UpdateSuccess : ResultMessage.NoChangeInOperation, count > 0 ? OperationEnumType.Success : OperationEnumType.NoChanged);
            }
            catch (SuktAppException e)
            {
                return new OperationResponse(e.Message, OperationEnumType.Error);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Delete
        public virtual int Delete(params TEntity[] entitys)
        {
            foreach (var entity in entitys)
            {
                CheckDelete(entity);
            }
            return _dbContext.SaveChanges();
        }
        public virtual async Task<OperationResponse> DeleteAsync(Tkey primaryKey)
        {
            TEntity entity = await this.GetByIdAsync(primaryKey);
            if (entity.IsNull())
            {
                return new OperationResponse($"该{primaryKey}键的数据不存在", OperationEnumType.QueryNull);
            }
            int count = await this.DeleteAsync(entity);
            return new OperationResponse(count > 0 ? ResultMessage.DeleteSuccess : ResultMessage.NoChangeInOperation, count > 0 ? OperationEnumType.Success : OperationEnumType.NoChanged);
        }
        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            entity = await this.GetByIdAsync(entity.Id);
            if (entity.IsNull())
            {
                throw new SuktAppException($"该{entity.Id}键的数据不存在");
            }
            CheckDelete(entity);
            return await _dbContext.SaveChangesAsync();
        }
        public virtual async Task<int> DeleteBatchAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            predicate.NotNull(nameof(predicate));
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                List<MemberBinding> newMemberBindings = new List<MemberBinding>();
                ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntity), "o"); //参数

                ConstantExpression constant = Expression.Constant(true);
                var propertyName = nameof(ISoftDelete.IsDeleted);
                var propertyInfo = typeof(TEntity).GetProperty(propertyName);
                var memberAssignment = Expression.Bind(propertyInfo, constant); //绑定属性
                newMemberBindings.Add(memberAssignment);

                //创建实体
                var newEntity = Expression.New(typeof(TEntity));
                var memberInit = Expression.MemberInit(newEntity, newMemberBindings.ToArray()); //成员初始化
                Expression<Func<TEntity, TEntity>> updateExpression = Expression.Lambda<Func<TEntity, TEntity>> //生成要更新的Expression
                (
                   memberInit,
                   new ParameterExpression[] { parameterExpression }
                );

                return await NoTrackEntities.Where(predicate).UpdateAsync(updateExpression, cancellationToken);
            }
            return await NoTrackEntities.Where(predicate).DeleteAsync(cancellationToken);
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
            //entity1.LastModifyId = _suktUser.Id a;
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
