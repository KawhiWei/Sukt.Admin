using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Sukt.Core.Shared;
using Sukt.Module.Core.AjaxResult;
using Sukt.Module.Core.Audit;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.OperationResult;

namespace Sukt.Core.API.Controllers
{
    /// <summary>
    /// 功能管理
    /// </summary>
    [Description("日志审计")]
    public class AuditLogController : ApiControllerBase
    {
        private readonly IAuditStore _auditStore;

        public AuditLogController(IAuditStore auditStore)
        {
            _auditStore = auditStore;
        }
        /// <summary>
        /// 分页获取审计日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("分页获取审计日志")]
        public async Task<PageList<AuditLogOutputPageDto>> GetAuditLogPageAsync([FromBody] PageRequest request)
        {
            return (await _auditStore.GetAuditLogPageAsync(request)).PageList();
        }
        /// <summary>
        /// 获取操作实体列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("获取操作实体列表")]
        public async Task<AjaxResult> GetAuditEntryListByAuditLogIdAsync(string id)
        {
            ObjectId.TryParse(id, out ObjectId objid);
            return (await _auditStore.GetAuditEntryListByAuditLogIdAsync(objid)).ToAjaxResult();
        }
        /// <summary>
        /// 获取实体属性列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("获取实体属性列表")]
        public async Task<AjaxResult> GetAuditEntryListByAuditEntryIdAsync(string id)
        {
            ObjectId.TryParse(id, out ObjectId objid);
            return (await _auditStore.GetAuditEntryListByAuditEntryIdAsync(objid)).ToAjaxResult();
        }
    }
}
