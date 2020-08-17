using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Events.EventBus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sukt.Core.Shared.Audit;

namespace Sukt.Core.Shared
{
    public class DefaultDbContext : SuktDbContextBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventBus _bus;
        private readonly IGetChangeTracker _changeTracker;
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options, IServiceProvider serviceProvider)
          : base(options, serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _bus = _serviceProvider.GetService<IEventBus>();
            _changeTracker = _serviceProvider.GetService<IGetChangeTracker>();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var auditlist = await _changeTracker.GetChangeTrackerList(this.ChangeTracker.Entries());
            var result =  await base.SaveChangesAsync(cancellationToken);
            //await this.AfterSaveChanges();
            await _bus.PublishAsync(new AuditEvent() { AuditList = auditlist });
            return result;
        }
    }
}
