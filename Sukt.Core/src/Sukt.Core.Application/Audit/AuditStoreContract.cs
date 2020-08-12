using Sukt.Core.Shared;
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
        private IMongoDBRepository<AuditEntry,Guid> _mongoDBRepository;
        public AuditStoreContract(IMongoDBRepository<AuditEntry, Guid> mongoDBRepository)
        {
            _mongoDBRepository = mongoDBRepository;
        }
        public async Task SaveAudit(List<AuditEntry> audit)
        {
            await _mongoDBRepository.InsertAsync(audit);
        }
    }
}
