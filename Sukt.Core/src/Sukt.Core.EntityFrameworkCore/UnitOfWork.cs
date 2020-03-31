using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukt.Core.Domain.Unitofwork;
using Sukt.Core.EntityFrameworkCore.DbContexts;
using Sukt.Core.Shared.OperationResult;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.EntityFrameworkCore
{
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : SuktDbContextBase
    {
        /// <summary>
        /// DBContext对象
        /// </summary>
        private readonly SuktDbContextBase _dbContext = null;
        public UnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext as SuktDbContextBase;
        }
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger _logger = null;
        /// <summary>
        /// 是否提交
        /// </summary>
        public bool HasCommitted { get; private set; }
        /// <summary>
        /// 事务
        /// </summary>
        private DbTransaction _dbTransaction=null;
        /// <summary>
        /// 上下文
        /// </summary>
        private DbConnection _connection = null;
        /// <summary>
        /// 
        /// </summary>
        public void BeginTransaction()
        {
            if(_dbTransaction?.Connection==null)
            {
                if(_connection.State!=System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                }
                _dbTransaction = _connection.BeginTransaction();
            }
            _dbContext.Database.UseTransaction(_dbTransaction);
            HasCommitted = false;
        }

        public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public DbContext GetDbContext()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public Task RollbackAsync()
        {
            throw new NotImplementedException();
        }

        public void UseTran(Action action)
        {
            throw new NotImplementedException();
        }

        public OperationResponse UseTran(Func<OperationResponse> func)
        {
            throw new NotImplementedException();
        }

        public Task UseTranAsync(Func<Task> func)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse> UseTranAsync(Func<Task<OperationResponse>> func)
        {
            throw new NotImplementedException();
        }
    }
}
