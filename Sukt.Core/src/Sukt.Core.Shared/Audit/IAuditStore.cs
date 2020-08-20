using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.SuktDependencyAppModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Shared.Audit
{
    public interface IAuditStore : IScopedDependency
    {
        Task SaveAudit(AuditLog auditLog, List<AuditEntryInputDto> audit);
    }
}
