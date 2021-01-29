using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.OperationResult;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Shared
{
    public interface IAggregateRootRepository<TEntity, Tkey>
        where TEntity : IAggregateRoot<Tkey>
    {
        IUnitOfWork UnitOfWork { get; }

        #region 查询

        /// <summary>
        /// 获取 <typeparamref name="TEntity"/>不跟踪数据更改（NoTracking）的查询数据源
        /// </summary>
        IQueryable<TEntity> NoTrackEntities { get; }

        /// <summary>
        /// 获取 <typeparamref name="TEntity"/>跟踪数据更改（Tracking）的查询数据源
        /// </summary>
        IQueryable<TEntity> TrackEntities { get; }

        /// <summary>
        /// 根据ID得到实体
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <returns>返回查询后实体</returns>
        TEntity GetById(Tkey primaryKey);

        /// <summary>
        /// 异步根据ID得到实体
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <returns>返回查询后实体</returns>
        Task<TEntity> GetByIdAsync(Tkey primaryKey);

        /// <summary>
        /// 根据ID得到Dto实体
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <returns>返回查询后实体并转成Dto</returns>
        TDto GetByIdToDto<TDto>(Tkey primaryKey) where TDto : class, new();

        /// <summary>
        /// 异步根据ID得到Dto实体
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <returns>返回查询后实体并转成Dto</returns>
        Task<TDto> GetByIdToDtoAsync<TDto>(Tkey primaryKey) where TDto : class, new();

        #endregion 查询

        #region 添加

        /// <summary>
        /// 以异步DTO插入实体
        /// </summary>
        /// <typeparam name="TInputDto">添加DTO类型</typeparam>
        /// <param name="dto">添加DTO</param>
        /// <param name="checkFunc">添加信息合法性检查委托</param>
        /// <param name="insertFunc">由DTO到实体的转换委托</param>
        /// <returns>业务操作结果</returns>
        //Task<OperationResponse> InsertAsync<TInputDto>(TInputDto dto, Func<TInputDto, Task> checkFunc = null, Func<TInputDto, TEntity, Task<TEntity>> insertFunc = null, Func<TEntity, TInputDto> completeFunc = null) where TInputDto : IInputDto<Tkey>;
        /// <summary>
        /// 以异步插入实体
        /// </summary>
        /// <param name="entity">要插入实体</param>
        /// <returns>影响的行数</returns>
        Task<OperationResponse> InsertAsync(TEntity entity);

        /// <summary>
        /// 以异步批量插入实体
        /// </summary>
        /// <param name="entitys">要插入实体集合</param>
        /// <returns>影响的行数</returns>
        Task<OperationResponse> InsertAsync(TEntity[] entitys);

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entitys">要插入实体集合</param>
        /// <returns></returns>
        OperationResponse Insert(params TEntity[] entitys);

        Task<OperationResponse> InsertAsync<TInputDto>(TInputDto dto, Func<TInputDto, Task> checkFunc = null, Func<TInputDto, TEntity, Task<TEntity>> insertFunc = null, Func<TEntity, TInputDto> completeFunc = null) where TInputDto : IInputDto<Tkey>;

        #endregion 添加

        #region 更新

        /// <summary>
        /// 以异步DTO更新实体
        /// </summary>
        /// <typeparam name="TInputDto">更新DTO类型</typeparam>
        /// <param name="dto">更新DTO</param>
        /// <param name="checkFunc">添加信息合法性检查委托</param>
        /// <param name="updateFunc">由DTO到实体的转换委托</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResponse> UpdateAsync<TInputDto>(TInputDto dto, Func<TInputDto, TEntity, Task> checkFunc = null, Func<TInputDto, TEntity, Task<TEntity>> updateFunc = null) where TInputDto : class, IInputDto<Tkey>, new();

        /// <summary>
        /// 异步更新
        /// </summary>
        /// <param name="entity">要更新实体</param>
        /// <returns>返回更新受影响条数</returns>
        Task<OperationResponse> UpdateAsync(TEntity entity);

        /// <summary>
        /// 同步更新
        /// </summary>
        /// <param name="entity">要更新实体</param>
        /// <returns>返回更新受影响条数</returns>
        OperationResponse Update(TEntity entity);

        #endregion 更新

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        Task<OperationResponse> DeleteAsync(Tkey primaryKey);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">要删除实体</param>
        /// <returns>返回删除受影响条数</returns>
        Task<int> DeleteAsync(TEntity entity);

        /// <summary>
        /// 异步删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        Task<int> DeleteBatchAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entitys">要删除实体集合</param>
        /// <returns>操作影响的行数</returns>
        int Delete(params TEntity[] entitys);

        #endregion 删除
    }
}
