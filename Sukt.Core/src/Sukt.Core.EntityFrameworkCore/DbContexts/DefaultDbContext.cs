using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared
{
    public class DefaultDbContext:SuktDbContextBase
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options, IServiceProvider serviceProvider)
          : base(options, serviceProvider)
        {

        }
    }
}
