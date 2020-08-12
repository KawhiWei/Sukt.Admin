using Sukt.Core.Shared.Audit;
using Sukt.Core.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Application.Audit
{
    /// <summary>
    /// 审计日志记录
    /// </summary>
    public class AuditStoreContract : IAuditStore
    {
        public Task SaveAudit(List<AuditEntry> audit)
        {
            return Task.CompletedTask;
        }
    }
}
