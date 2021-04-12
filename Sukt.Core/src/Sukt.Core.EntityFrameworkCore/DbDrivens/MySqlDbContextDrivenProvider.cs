using Microsoft.EntityFrameworkCore;
using Sukt.Core.Shared.AppOption;
using Sukt.Core.Shared.DbContextDriven;
using Sukt.Core.Shared.Entity;
using System;

namespace Sukt.Core.EntityFrameworkCore.DbDrivens
{
    /// <summary>
    /// MySql驱动提供者
    /// </summary>
    public class MySqlDbContextDrivenProvider : IDbContextDrivenProvider
    {
        public DatabaseType DatabaseType => DatabaseType.MySql;
        public DbContextOptionsBuilder Builder(DbContextOptionsBuilder builder, string connectionString, DestinyContextOptionsBuilder optionsBuilder)
        {
            builder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23)), opt => opt.MigrationsAssembly(optionsBuilder.MigrationsAssemblyName));
            return builder;
        }
    }
}
