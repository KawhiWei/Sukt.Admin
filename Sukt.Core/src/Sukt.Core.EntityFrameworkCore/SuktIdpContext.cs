using Microsoft.EntityFrameworkCore;
using Sukt.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.EntityFrameworkCore
{
    public class SuktIdpContext : SuktDbContextBase
    {
        public SuktIdpContext(DbContextOptions<SuktContext> options, IServiceProvider serviceProvider)
          : base(options, serviceProvider)
        {
        }
    }
}
