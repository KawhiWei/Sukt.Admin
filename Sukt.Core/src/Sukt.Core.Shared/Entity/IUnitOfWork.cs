using Microsoft.EntityFrameworkCore;
using Sukt.Core.Shared.OperationResult;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Shared.Entity
{
    public interface IUnitOfWork: IDisposable
    {
        /// <summary>
        /// 得到上下文
        /// </summary>
        /// <returns></returns>
        DbContext GetDbContext();

        /// <summary>
        /// 开启事务
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();

        /// <summary>
        /// 开启异步事务
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();

        /// <summary>
        /// 开启事务 如果成功提交事务，失败回滚事务
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <returns></returns>
        void UseTran(Action action);

        /// <summary>
        /// 异步开启事务 如果成功提交事务，失败回滚事务
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <returns></returns>
        Task UseTranAsync(Func<Task> func);

        /// <summary>
        /// 开启事务 如果成功提交事务，失败回滚事务
        /// </summary>
        /// <param name="func"></param>
        /// <returns>返回操作结果</returns>
        OperationResponse UseTran(Func<OperationResponse> func);

        /// <summary>
        /// 开启事务 如果成功提交事务，失败回滚事务
        /// </summary>
        /// <param name="func"></param>
        /// <returns>返回操作结果</returns>
        Task<OperationResponse> UseTranAsync(Func<Task<OperationResponse>> func);

        /// <summary>
        /// 异步提交事务
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();

        /// <summary>
        /// 异步回滚
        /// </summary>
        /// <returns></returns>
        Task RollbackAsync();
    }
}
