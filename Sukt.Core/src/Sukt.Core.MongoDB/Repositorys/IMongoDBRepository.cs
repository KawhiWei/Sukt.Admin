using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.MongoDB.Repositorys
{
    public interface IMongoDBRepository<TData, Tkey>
    {
        //Find<T> – 返回集合中与提供的搜索条件匹配的所有文档。
        //InsertOne – 插入提供的对象作为集合中的新文档。
        //ReplaceOne – 将与提供的搜索条件匹配的单个文档替换为提供的对象。
        //DeleteOne – 删除与提供的搜索条件匹配的单个文档。
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        Task InsertAsync(TData entity);
        /// <summary>
        /// 数组创建
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        Task InsertAsync(List<TData> entitys);
        /// <summary>
        /// 查询数据
        /// </summary>
        IMongoQueryable<TData> Entities { get; }
        IMongoCollection<TData> Collection { get; }
    }
}
