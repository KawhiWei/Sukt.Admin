using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uwl.Data.EntityFramework.Uwl_DbContext;

namespace UwlAPI.Tools.StartupExtension
{
    /// <summary>
    /// EFCore自动迁移扩展
    /// </summary>
    public static class EntityFrameworkCoreAutoMigrate
    {
        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="app"></param>
        public static void AutoMigrate(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dc = scope.ServiceProvider.GetService<UwlDbContext>();
                if(dc.Database.GetPendingMigrations().Any())
                    dc.Database.Migrate();
            }
        }
    }
}
