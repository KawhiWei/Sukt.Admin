using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Uwl.Domain.IRepositories
{
    public interface IUnitofWork
    {

        void Commit();
        DbContext GetDbContext();

        void BeginTransaction();


        /// <summary>
        /// sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paraDic"></param>
        /// <returns></returns>

         IEnumerable<dynamic> SqlQuery(string sql, Dictionary<string, object> paraDic = null);


        IEnumerable<T> SqlQuery<T>(string sql, Dictionary<string, object> paraDic=null) where T : class, new();
        /// <summary>
        /// 异步sql查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paraDic"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> SqlQueryAsync<T>(string sql, Dictionary<string, object> paraDic = null) where T : class, new();

        ///// <summary>
        ///// 工作单元提交
        ///// </summary>
        ///// <returns></returns>
        //bool Commit();
        ///// <summary>
        ///// 创建工作单元事务
        ///// </summary>
        //void BeginTransaction();
        ///// <summary>
        ///// 释放链接对象
        ///// </summary>
        //void Dispose();
        ///// <summary>
        ///// 添加
        ///// </summary>
        ///// <param name="entity"></param>
        //void Add<T>(T entity) where T :class;
        ///// <summary>
        ///// 修改
        ///// </summary>
        ///// <param name="entity"></param>
        //void Update<T>(T entity) where T : class;
        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="entity"></param>
        //void Detete<T>(T entity) where T : class;
        ///// <summary>
        ///// 批量添加
        ///// </summary>
        ///// <param name="entity"></param>
        //void Add<T>(List<T> entity) where T : class;
        ///// <summary>
        ///// 批量修改
        ///// </summary>
        ///// <param name="entity"></param>
        //void Update<T>(List<T> entity) where T : class;
        ///// <summary>
        ///// 批量删除
        ///// </summary>
        ///// <param name="entity"></param>
        //void Detete<T>(List<T> entity) where T : class;
    }
   

}
