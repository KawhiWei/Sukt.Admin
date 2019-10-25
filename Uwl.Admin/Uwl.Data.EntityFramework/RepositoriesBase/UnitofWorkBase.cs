using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Uwl.Data.EntityFramework.EFExtensionsSql;
using Uwl.Data.EntityFramework.Uwl_DbContext;
using Uwl.Data.Model;
using Uwl.Domain.IRepositories;

namespace Uwl.Data.EntityFramework.RepositoriesBase
{
    ///这里应该做成泛型
    public  class UnitofWorkBase : IUnitofWork
    {
        /// <summary>
        /// 定义数据库上下文访问对象
        /// </summary>
        private readonly DbContext _uwldbContext;

        public bool HasCommited { get; private set; }

        /// <summary>
        ///
        /// </summary>
        private IDbContextTransaction currenTtransaction { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uwlDbContext"></param>
        public UnitofWorkBase(UwlDbContext uwlDbContext)
        {
            _uwldbContext = uwlDbContext;
            HasCommited = false;
        }

  
        ///得到上下文
        public DbContext GetDbContext()
        {
            return _uwldbContext;
        }


        /// <summary>
        /// 创建事务
        /// </summary>
        public void BeginTransaction()
        {

            currenTtransaction = GetTransaction();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {

            if (HasCommited)
            {
                return;
            }

            if (currenTtransaction != null)
            {
                try
                {
                    currenTtransaction.Commit();
                }
                catch (Exception e)
                {
                    currenTtransaction.Rollback();
            
                    HasCommited = true;
                    throw e;
                }

            }
            HasCommited = true;


        }

                                            
        
        /// <summary>释放对象.</summary>
        public void Dispose()
        {
         
        }


        public IDbContextTransaction GetTransaction()
        {
            return this.Database().BeginTransaction();
        }
        public IDbContextTransaction GetTransaction(IsolationLevel isolationLevel)
        {
            return this.Database().BeginTransaction(isolationLevel);
        }

        public async Task<IDbContextTransaction> GetTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.Database().BeginTransactionAsync(cancellationToken);
        }
        public async Task<IDbContextTransaction> GetTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.Database().BeginTransactionAsync(isolationLevel, cancellationToken);
        }

        public DatabaseFacade Database()
        {
            return this.GetDbContext().Database;
        }


        /// <summary>
        /// sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paraDic"></param>
        /// <returns></returns>

        public IEnumerable<dynamic> SqlQuery(string sql, Dictionary<string, object> paraDic = null  )
        {
            if (string.IsNullOrEmpty(sql))

            {
                throw new ArgumentNullException(nameof(sql));
            }
            return this.Database().SqlQuery<dynamic>(sql, paraDic);

        }

        /// <summary>
        /// 异步sql查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paraDic"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> SqlQueryAsync<T>(string sql, Dictionary<string, object> paraDic = null) where T : class, new()
        {
            if (string.IsNullOrEmpty(sql))

            {
                throw new ArgumentNullException(nameof(sql));
            }
            return await this.Database().SqlQueryAsync<T>(sql, paraDic);
        }

        public IEnumerable<T> SqlQuery<T>(string sql, Dictionary<string, object> paraDic = null) where T : class, new()
        {

            if (string.IsNullOrEmpty(sql))

            {
                throw new ArgumentNullException(nameof(sql));
            }
            return this.Database().SqlQuery<T>(sql,paraDic);
        }
    }

    ///// <summary>
    ///// 添加
    ///// </summary>
    ///// <param name="entity"></param>
    //public void Add<T>(T entity) where T : class
    //{
    //    _uwldbContext.Set<T>().Add(entity);
    //}
    ///// <summary>
    ///// 修改
    ///// </summary>
    ///// <param name="entity"></param>
    //public void Update<T>(T entity) where T : class
    //{
    //    _uwldbContext.Update<T>(entity);
    //}
    ///// <summary>
    ///// 删除
    ///// </summary>
    ///// <param name="entity"></param>
    //public void Detete<T>(T entity) where T : class
    //{
    //    _uwldbContext.Set<T>().Remove(entity);
    //}
    ///// <summary>
    ///// 批量添加
    ///// </summary>
    ///// <param name="entity"></param>
    //public void Add<T>(List<T> entity) where T : class
    //{
    //    _uwldbContext.AddRange(entity);
    //}
    ///// <summary>
    ///// 批量修改
    ///// </summary>
    ///// <param name="entity"></param>
    //public void Update<T>(List<T> entity) where T : class
    //{
    //    _uwldbContext.UpdateRange(entity);
    //}
    ///// <summary>
    ///// 批量删除
    ///// </summary>
    ///// <param name="entity"></param>
    //public void Detete<T>(List<T> entity) where T : class
    //{
    //    _uwldbContext.RemoveRange(entity);
    //}
    ///// <summary>
    ///// 提交工作单元
    ///// </summary>
    ///// <returns></returns>
    //public bool Commit() //这个啥JB玩儿。SaveChanges这个本身就带事务了，为什么还要带一层事务呢？？？？？
    //{
    //    int result = 0;
    //    try
    //    {
    //        result = _uwldbContext.SaveChanges(); 
    //        if (_dbTransaction != null)
    //            _dbTransaction.Commit();
    //        return result > 0;
    //    }
    //    catch (Exception ex)
    //    {
    //        result = -1;
    //        _dbTransaction.Rollback();
    //        throw new Exception($"Commit 异常：{ex.InnerException}/r{ ex.Message}");
    //    }
    //}
    ///// <summary>
    ///// 创建工作单元事务
    ///// </summary>
    //public void BeginTransaction()
    //{
    //    _dbTransaction = _uwldbContext.Database.BeginTransaction();
    //}
    ///// <summary>
    ///// 释放链接对象
    ///// </summary>
    //public void Dispose()
    //{
    //    _uwldbContext.Dispose();
    //}
}

