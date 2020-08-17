using Sukt.Core.Shared;
using Sukt.Core.Shared.Audit;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Extensions;
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
        private IMongoDBRepository<AuditLog, Guid> _auditLogRepository;
        private IMongoDBRepository<AuditEntry, Guid> _mongoDBRepository;
        private IMongoDBRepository<AuditPropertysEntry, Guid> _auditPropertyRepository;

        public AuditStoreContract(IMongoDBRepository<AuditLog, Guid> auditLogRepository, IMongoDBRepository<AuditEntry, Guid> mongoDBRepository, IMongoDBRepository<AuditPropertysEntry, Guid> auditPropertyRepository)
        {
            _auditLogRepository = auditLogRepository;
            _mongoDBRepository = mongoDBRepository;
            _auditPropertyRepository = auditPropertyRepository;
        }

        public async Task SaveAudit(List<AuditEntryInputDto> audit)
        {
            //var list= audit.MapTo<AuditEntry>();
            


            await Task.CompletedTask;

            //await _mongoDBRepository.InsertAsync(audit);
        }
    }
}
