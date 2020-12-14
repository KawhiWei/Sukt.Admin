using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Shared
{
    public class IdentityServer4DefaultDbContext : SuktDbContextBase
    {
        public IdentityServer4DefaultDbContext(DbContextOptions<IdentityServer4DefaultDbContext> options, IServiceProvider serviceProvider) : base(options, serviceProvider)
        {

        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
