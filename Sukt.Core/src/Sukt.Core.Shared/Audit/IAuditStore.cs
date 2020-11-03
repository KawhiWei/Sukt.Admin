using MongoDB.Bson;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Extensions.ResultExtensions;
using Sukt.Core.Shared.OperationResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sukt.Core.Shared.Audit
{
    public interface IAuditStore : IScopedDependency
    {
        Task SaveAudit(AuditLog auditLog, List<AuditEntryInputDto> audit);
        Task<IPageResult<AuditLogOutputPageDto>> GetAuditLogPageAsync(PageRequest request);
        Task<OperationResponse> GetAuditEntryListByAuditLogIdAsync(ObjectId id);
        Task<OperationResponse> GetAuditEntryListByAuditEntryIdAsync(ObjectId id);
    }
}