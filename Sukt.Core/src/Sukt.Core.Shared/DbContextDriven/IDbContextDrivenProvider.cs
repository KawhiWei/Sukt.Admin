using Microsoft.EntityFrameworkCore;
using Sukt.Core.Shared.AppOption;
using Sukt.Core.Shared.Entity;

namespace Sukt.Core.Shared.DbContextDriven
{
    /// <summary>
    /// 上下文驱动提供者
    /// </summary>
    public interface IDbContextDrivenProvider : ISingletonDependency
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        DatabaseType DatabaseType { get; }

        /// <summary>
        /// 构建数据库驱动
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>

        DbContextOptionsBuilder Builder(DbContextOptionsBuilder builder, string connectionString, DestinyContextOptionsBuilder optionsBuilder);

    }
}
