using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Sukt.Module.Core.Audit;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.OperationResult;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sukt.MongoDB.Repositorys;
using Sukt.Module.Core.Extensions.ResultExtensions;
using Sukt.Module.Core.ExpressionUtil;
using Sukt.MongoDB;
using Sukt.Module.Core.ResultMessageConst;
using Sukt.Module.Core.Enums;
namespace Sukt.Core.Application.Audit
{
    /// <summary>
    /// 审计日志记录
    /// </summary>
    public class AuditStoreContract : IAuditStore
    {
        private IMongoDBRepository<AuditLog, ObjectId> _auditLogRepository;
        private IMongoDBRepository<AuditEntry, ObjectId> _auditEntryRepository;
        private IMongoDBRepository<AuditPropertysEntry, ObjectId> _auditPropertysEntryRepository;

        public AuditStoreContract(IMongoDBRepository<AuditLog, ObjectId> auditLogRepository, IMongoDBRepository<AuditEntry, ObjectId> auditEntryRepository, IMongoDBRepository<AuditPropertysEntry, ObjectId> auditPropertysEntryRepository)
        {
            _auditLogRepository = auditLogRepository;
            _auditEntryRepository = auditEntryRepository;
            _auditPropertysEntryRepository = auditPropertysEntryRepository;
        }

        public async Task SaveAudit(AuditChangeInputDto audit)
        {
            List<AuditEntry> auditEntry = new List<AuditEntry>();
            List<AuditPropertysEntry> auditpropertyentry = new List<AuditPropertysEntry>();
            AuditLog auditLog = new AuditLog();
            auditLog.BrowserInformation = audit.BrowserInformation;
            auditLog.Action = audit.Action;
            auditLog.Ip = audit.Ip;
            auditLog.FunctionName = audit.FunctionName;
            auditLog.ExecutionDuration = audit.ExecutionDuration;
            //auditLog.UserId = audit.UserId;
            auditLog.ResultType = audit.ResultType;
            auditLog.Message = audit.Message;
            foreach (var item in audit.AuditEntryInputDtos)
            {
                var model = item.MapTo<AuditEntry>();
                model.AuditLogId = auditLog.Id;
                foreach (var Property in item.PropertysEntryInputDto)
                {
                    var propertymodel = Property.MapTo<AuditPropertysEntry>();
                    propertymodel.AuditEntryId = model.Id;
                    auditpropertyentry.Add(propertymodel);
                }
                auditEntry.Add(model);
            }
            await _auditLogRepository.InsertAsync(auditLog);
            await _auditEntryRepository.InsertAsync(auditEntry.ToArray());
            await _auditPropertysEntryRepository.InsertAsync(auditpropertyentry.ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IPageResult<AuditLogOutputPageDto>> GetAuditLogPageAsync(PageRequest request)
        {
            var exp = FilterHelp.GetExpression<AuditLog>(request.queryFilter);

            return await _auditLogRepository.Collection.ToPageAsync(exp, request, x => new AuditLogOutputPageDto
            {
                BrowserInformation = x.BrowserInformation,
                Ip = x.Ip,
                FunctionName = x.FunctionName,
                Action = x.Action,
                ExecutionDuration = x.ExecutionDuration,
                Id = x.Id
            });
        }
        /// <summary>
        /// 获取操作实体列表
        /// </summary>
        /// <returns></returns>
        public async Task<OperationResponse> GetAuditEntryListByAuditLogIdAsync(ObjectId id)
        {
            var list = await _auditEntryRepository.Entities.Where(x => x.AuditLogId == id)
                .Select(x => new AuditEntryOutputDto
                {
                    Id = x.Id,
                    EntityAllName = x.EntityAllName,
                    EntityDisplayName = x.EntityDisplayName,
                    TableName = x.TableName,
                    KeyValues = x.KeyValues,
                    OperationType = x.OperationType
                }).ToListAsync();
            OperationResponse operationResponse = new OperationResponse(ResultMessage.DataSuccess, list, OperationEnumType.Success);
            return operationResponse;
        }
        /// <summary>
        /// 获取实体表Id获取每个属性的操作日志
        /// </summary>
        /// <returns></returns>
        public async Task<OperationResponse> GetAuditEntryListByAuditEntryIdAsync(ObjectId id)
        {
            var list = await _auditPropertysEntryRepository.Entities.Where(x => x.AuditEntryId == id)
                .Select(x => new AuditPropertyEntryOutputDto
                {
                    Properties = x.Properties,
                    OriginalValues = x.OriginalValues,
                    NewValues = x.NewValues,
                    PropertiesType = x.PropertiesType,
                    PropertieDisplayName = x.PropertieDisplayName,
                })
                .ToListAsync();
            OperationResponse operationResponse = new OperationResponse(ResultMessage.DataSuccess, list, OperationEnumType.Success);
            return operationResponse;
        }
    }
}
