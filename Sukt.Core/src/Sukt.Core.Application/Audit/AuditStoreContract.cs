using Sukt.Core.MongoDB.Repositorys;
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
        private IMongoDBRepository<AuditEntry, Guid> _auditEntryRepository;
        private IMongoDBRepository<AuditPropertysEntry, Guid> _auditPropertysEntryRepository;

        public AuditStoreContract(IMongoDBRepository<AuditLog, Guid> auditLogRepository, IMongoDBRepository<AuditEntry, Guid> auditEntryRepository, IMongoDBRepository<AuditPropertysEntry, Guid> auditPropertysEntryRepository)
        {
            _auditLogRepository = auditLogRepository;
            _auditEntryRepository = auditEntryRepository;
            _auditPropertysEntryRepository = auditPropertysEntryRepository;
        }

        public async Task SaveAudit(AuditLog auditLog, List<AuditEntryInputDto> audit)
        {
            List<AuditEntry> auditEntry = new List<AuditEntry>();
            List<AuditPropertysEntry> auditpropertyentry = new List<AuditPropertysEntry>();
            foreach (var item in audit)
            {
                var model = item.MapTo<AuditEntry>();
                model.AuditLogId = auditLog.Id;
                foreach (var Property in item.PropertysEntryInputDto)
                {
                    var propertymodel= Property.MapTo<AuditPropertysEntry>();
                    propertymodel.AuditEntryId = model.Id;
                    auditpropertyentry.Add(propertymodel);
                }
                auditEntry.Add(model);
            }
            await _auditLogRepository.InsertAsync(auditLog);
            await _auditEntryRepository.InsertAsync(auditEntry);
            await _auditPropertysEntryRepository.InsertAsync(auditpropertyentry);
        }
    }
}
