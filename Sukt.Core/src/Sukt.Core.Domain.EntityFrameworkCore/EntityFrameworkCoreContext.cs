using Microsoft.EntityFrameworkCore;
using SuktCore.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Domain.Repository
{
    public class EntityFrameworkCoreContext : SuktDbContextBase
    {
        public EntityFrameworkCoreContext(DbContextOptions<EntityFrameworkCoreContext> options, IServiceProvider serviceProvider)
          : base(options, serviceProvider)
        {
        }
    }
}
