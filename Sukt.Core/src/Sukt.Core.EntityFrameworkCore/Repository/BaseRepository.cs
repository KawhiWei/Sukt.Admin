using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Sukt.Core.Shared
{
    public class BaseRepository<TEntity, Tkey> : IEFCoreRepository<TEntity, Tkey>
        where TEntity : class, IEntity<Tkey> where Tkey : IEquatable<Tkey>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BaseRepository(IServiceProvider serviceProvider)
        {
            UnitOfWork = (serviceProvider.GetService(typeof(IUnitOfWork)) as IUnitOfWork);//获取工作单元实例
            _dbContext = UnitOfWork.GetDbContext();
            _dbSet = _dbContext.Set<TEntity>();
            _suktUser = (serviceProvider.GetService(typeof(ISuktUser)) as ISuktUser);//获取用户登录存储解析Token实例
            _logger = serviceProvider.GetLogger<BaseRepository<TEntity, Tkey>>();
            _httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
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

        #endregion Query

        #region Insert

        /// <summary>
        /// 同步批量添加实体
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public virtual OperationResponse Insert(params TEntity[] entitys)
        {
            entitys.NotNull(nameof(entitys));
            entitys = entitys.CheckInsert<TEntity, Tkey>(_httpContextAccessor);// CheckInsert(entitys);
            _dbSet.AddRange(entitys);
            var count = _dbContext.SaveChanges();
            return new OperationResponse(count > 0 ? ResultMessage.InsertSuccess : ResultMessage.NoChangeInOperation, count > 0 ? OperationEnumType.Success : OperationEnumType.NoChanged);
        }

        /// <summary>
        /// 异步添加单条实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<OperationResponse> InsertAsync(TEntity entity)
        {
            entity.NotNull(nameof(entity));
            //entity = CheckInsert(entity);
            entity = entity.CheckInsert<TEntity, Tkey>(_httpContextAccessor);
            await _dbSet.AddAsync(entity);
            int count = await _dbContext.SaveChangesAsync();
            return new OperationResponse(count > 0 ? ResultMessage.InsertSuccess : ResultMessage.NoChangeInOperation, count > 0 ? OperationEnumType.Success : OperationEnumType.NoChanged);
        }

        /// <summary>
        /// 批量异步添加实体
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public virtual async Task<OperationResponse> InsertAsync(TEntity[] entitys)
        {
            entitys.NotNull(nameof(entitys));
            entitys = entitys.CheckInsert<TEntity, Tkey>(_httpContextAccessor); //CheckInsert(entitys);
            await _dbSet.AddRangeAsync(entitys);
            int count = await _dbContext.SaveChangesAsync();
            return new OperationResponse(count > 0 ? ResultMessage.InsertSuccess : ResultMessage.NoChangeInOperation, count > 0 ? OperationEnumType.Success : OperationEnumType.NoChanged);
        }
        /// <summary>
        /// 异步添加单条实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<OperationResponse> InsertAsync(TEntity entity, Func<TEntity, Task> checkFunc = null, Func<TEntity, TEntity, Task<TEntity>> insertFunc = null, Func<TEntity, TEntity> completeFunc = null)
        {
            entity.NotNull(nameof(entity));
            try
            {
                if (checkFunc.IsNotNull())
                {
                    await checkFunc(entity);
                }
                if (!insertFunc.IsNull())
                {
                    entity = await insertFunc(entity, entity);
                }
                entity = entity.CheckInsert<TEntity, Tkey>(_httpContextAccessor);//CheckInsert(entity);
                await _dbSet.AddAsync(entity);

                if (completeFunc.IsNotNull())
                {
                    entity = completeFunc(entity);
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
            //entity.NotNull(nameof(entity));
            //entity = CheckInsert(entity);
            //await _dbSet.AddAsync(entity);
            //int count = await _dbContext.SaveChangesAsync();
            //return new OperationResponse(count > 0 ? ResultMessage.InsertSuccess : ResultMessage.NoChangeInOperation, count > 0 ? OperationEnumType.Success : OperationEnumType.NoChanged);
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
                entity = entity.CheckInsert<TEntity, Tkey>(_httpContextAccessor);//CheckInsert(entity);
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

        #endregion Insert

        #region Update

        /// <summary>
        /// 同步逐条更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual OperationResponse Update(TEntity entity)
        {
            entity.NotNull(nameof(entity));
            entity = entity.CheckModification<TEntity, Tkey>(_httpContextAccessor);// CheckUpdate(entity);
            _dbSet.Update(entity);
            int count = _dbContext.SaveChanges();
            return new OperationResponse(count > 0 ? ResultMessage.UpdateSuccess : ResultMessage.NoChangeInOperation, count > 0 ? OperationEnumType.Success : OperationEnumType.NoChanged);
        }

        /// <summary>
        /// 异步逐条更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<OperationResponse> UpdateAsync(TEntity entity)
        {
            entity.NotNull(nameof(entity));
            entity = entity.CheckModification<TEntity, Tkey>(_httpContextAccessor);//CheckUpdate(entity);
            _dbSet.Update(entity);
            int count = await _dbContext.SaveChangesAsync();
            return new OperationResponse(count > 0 ? ResultMessage.UpdateSuccess : ResultMessage.NoChangeInOperation, count > 0 ? OperationEnumType.Success : OperationEnumType.NoChanged);
        }

        /// <summary>
        /// 异步批量更新
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public virtual async Task<OperationResponse> UpdateAsync(TEntity[] entitys)
        {
            entitys.NotNull(nameof(entitys));
            entitys = entitys.CheckModification<TEntity, Tkey>(_httpContextAccessor);//.CheckModification<TEntity, Tkey>(_httpContextAccessor);//CheckUpdate(entitys);
            _dbSet.UpdateRange(entitys);
            int count = await _dbContext.SaveChangesAsync();
            return new OperationResponse(count > 0 ? ResultMessage.UpdateSuccess : ResultMessage.NoChangeInOperation, count > 0 ? OperationEnumType.Success : OperationEnumType.NoChanged);
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
                entity = entity.CheckModification<TEntity, Tkey>(_httpContextAccessor); //CheckUpdate(entity);
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

        #endregion Update

        #region Delete

        public virtual OperationResponse Delete(params TEntity[] entitys)
        {
            foreach (var entity in entitys)
            {
                CheckDelete(entity);
            }
            var count = _dbContext.SaveChanges();
            return new OperationResponse(count > 0 ? ResultMessage.UpdateSuccess : ResultMessage.NoChangeInOperation, count > 0 ? OperationEnumType.Success : OperationEnumType.NoChanged);

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

        public virtual async Task<OperationResponse> DeleteBatchAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
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
                var counts = await NoTrackEntities.Where(predicate).UpdateAsync(updateExpression, cancellationToken);
                return new OperationResponse(counts > 0 ? ResultMessage.DeleteSuccess : ResultMessage.NoChangeInOperation, counts > 0 ? OperationEnumType.Success : OperationEnumType.NoChanged);
            }
            var count = await NoTrackEntities.Where(predicate).DeleteAsync(cancellationToken);
            return new OperationResponse(count > 0 ? ResultMessage.DeleteSuccess : ResultMessage.NoChangeInOperation, count > 0 ? OperationEnumType.Success : OperationEnumType.NoChanged);
        }

        #endregion Delete

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

        //  /// <summary>
        //  /// 检查创建
        //  /// </summary>
        //  /// <param name="entitys">实体集合</param>
        //  /// <returns></returns>
        //  private TEntity[] CheckInsert(TEntity[] entitys)
        //  {
        //      for (int i = 0; i < entitys.Length; i++)
        //      {
        //          var entity = entitys[i];
        //          entitys[i] = CheckInsert(entity);
        //      }
        //      return entitys;
        //  }
        //  /// <summary>
        //  /// 检查创建时间
        //  /// </summary>
        //  /// <param name="entity">实体</param>
        //  /// <returns></returns>
        //  private TEntity CheckInsert(TEntity entity)
        //  {
        //      var creationAudited = entity.GetType().GetInterface(/*$"ICreationAudited`1"*/typeof(ICreatedAudited<>).Name);
        //      if (creationAudited == null)
        //      {
        //          return entity;
        //      }

        //      var typeArguments = creationAudited?.GenericTypeArguments[0];
        //      var fullName = typeArguments?.FullName;
        //      if (fullName == typeof(Guid).FullName)
        //      {
        //          entity = entity.CheckModification<TEntity, Tkey>(_httpContextAccessor);//CheckICreationAudited<Guid>(entity);
        //      }

        //      return entity;
        //  }
        //  private TEntity CheckICreationAudited<TUserKey>(TEntity entity)
        //     where TUserKey : struct, IEquatable<TUserKey>
        //  {
        //      if (!entity.GetType().IsBaseOn(typeof(ICreatedAudited<>)))
        //      {
        //          return entity;
        //      }

        //      ICreatedAudited<TUserKey> entity1 = (ICreatedAudited<TUserKey>)entity;
        //      entity1.CreatedId = _httpContextAccessor.HttpContext?.User?.Identity.GetUesrId<TUserKey>();
        //      entity1.CreatedAt = DateTime.Now;
        //      return (TEntity)entity1;
        //  }
        //  /// <summary>
        //  /// 检查最后修改时间
        //  /// </summary>
        //  /// <param name="entitys"></param>
        //  /// <returns></returns>
        //  private TEntity[] CheckUpdate(TEntity[] entitys)
        //  {
        //      for (int i = 0; i < entitys.Length; i++)
        //      {
        //          var entity = entitys[i];
        //          entitys[i] = CheckUpdate(entity);
        //      }
        //      return entitys;
        //  }
        //  /// <summary>
        //  /// 检查最后修改时间
        //  /// </summary>
        //  /// <param name="entity">实体</param>
        //  /// <returns></returns>
        //  private TEntity CheckUpdate(TEntity entity)
        //  {
        //      var creationAudited = entity.GetType().GetInterface(/*$"ICreationAudited`1"*/typeof(IModifyAudited<>).Name);
        //      if (creationAudited == null)
        //      {
        //          return entity;
        //      }

        //      var typeArguments = creationAudited?.GenericTypeArguments[0];
        //      var fullName = typeArguments?.FullName;
        //      if (fullName == typeof(Guid).FullName)
        //      {
        //          entity = CheckIModificationAudited<Guid>(entity);
        //      }

        //      return entity;
        //  }
        //  /// <summary>
        //  /// 检查最后修改时间
        //  /// </summary>
        //  /// <typeparam name="TUserKey"></typeparam>
        //  /// <param name="entity"></param>
        //  /// <returns></returns>
        //  public TEntity CheckIModificationAudited<TUserKey>(TEntity entity)
        //where TUserKey : struct, IEquatable<TUserKey>
        //  {
        //      if (!entity.GetType().IsBaseOn(typeof(IModifyAudited<>)))
        //      {
        //          return entity;
        //      }

        //      IModifyAudited<TUserKey> entity1 = (IModifyAudited<TUserKey>)entity;
        //      //entity1.LastModifyId = _suktUser.Id a;
        //      entity1.LastModifyId = _httpContextAccessor.HttpContext?.User?.Identity.GetUesrId<TUserKey>();
        //      entity1.LastModifedAt = DateTime.Now;
        //      return (TEntity)entity1;
        //  }

        #endregion 帮助方法
    }
}
