using Microsoft.EntityFrameworkCore;
using System;

namespace Sukt.Core.Shared
{
    public class DefaultDbContext : SuktDbContextBase
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options, IServiceProvider serviceProvider)
          : base(options, serviceProvider)
        {
        }
    }
}
