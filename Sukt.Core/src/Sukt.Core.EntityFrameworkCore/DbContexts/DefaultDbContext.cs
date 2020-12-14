using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sukt.Core.Shared.Audit;
using Sukt.Core.Shared.Events.EventBus;
using Sukt.Core.Shared.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Shared
{
    public class DefaultDbContext : SuktDbContextBase
    {
        private readonly IMediatorHandler _bus;
        private readonly IGetChangeTracker _changeTracker;
        private readonly ILogger _logger;
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options, IServiceProvider serviceProvider)
          : base(options, serviceProvider)
        {
            _bus = _serviceProvider.GetService<IMediatorHandler>();
            _changeTracker = _serviceProvider.GetService<IGetChangeTracker>();
            _logger = serviceProvider.GetLogger(GetType());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var auditlist = _changeTracker.GetChangeTrackerList(this.ChangeTracker.Entries());
            var result = await base.SaveChangesAsync(cancellationToken);
            await _bus.PublishAsync(new AuditEvent() { AuditList = auditlist });
            return result;
        }
        public override int SaveChanges()
        {
            var auditlist = _changeTracker.GetChangeTrackerList(ChangeTracker.Entries());
            var result = base.SaveChanges();
            _bus.PublishAsync(new AuditEvent() { AuditList = auditlist });
            return result;
        }
    }
}
