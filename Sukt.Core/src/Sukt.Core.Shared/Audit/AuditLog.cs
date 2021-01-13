using MongoDB.Bson;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Core.Shared.Audit
{
    /// <summary>
    ///
    /// </summary>
    [MongoDBTable("AuditLog")]
    [DisplayName("审计日志主表")]
    public class AuditLog : EntityBase<ObjectId>
    {
        public AuditLog()
        {
            Id = ObjectId.GenerateNewId();
        }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        [DisplayName("浏览器信息")]
        public string BrowserInformation { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [DisplayName("IP地址")]
        public string Ip { get; set; }

        /// <summary>
        /// 功能名称
        /// </summary>
        [DisplayName("功能名称")]
        public string FunctionName { get; set; }

        /// <summary>
        /// 操作Action
        /// </summary>
        [DisplayName("操作Action")]
        public string Action { get; set; }

        /// <summary>
        /// 执行时长
        /// </summary>
        [DisplayName("执行时长")]
        public double ExecutionDuration { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [DisplayName("创建人")]
        public string UserId { get; set; }
        /// <summary>
        /// 结果类型
        /// </summary>
        [DisplayName("结果类型")]
        public AjaxResultType ResultType { get; set; }
        [DisplayName("返回消息")]
        public string Message { get; set; }
    }
    public class AuditChangeInputDto
    {
        public AuditChangeInputDto()
        {
            AuditEntryInputDtos = new List<AuditEntryInputDto>();
        }
        /// <summary>
        /// 浏览器信息
        /// </summary>
        [DisplayName("浏览器信息")]
        public string BrowserInformation { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        [DisplayName("IP地址")]
        public string Ip { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        [DisplayName("功能名称")]
        public string FunctionName { get; set; }
        /// <summary>
        /// 操作Action
        /// </summary>
        [DisplayName("操作Action")]
        public string Action { get; set; }
        /// <summary>
        /// 执行时长
        /// </summary>
        [DisplayName("执行时长")]
        public double ExecutionDuration { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// 审计实体集合
        /// </summary>
        public List<AuditEntryInputDto> AuditEntryInputDtos { get; set; }
        [DisplayName("结果类型")]
        /// <summary>
        /// 结果类型
        /// </summary>
        public AjaxResultType ResultType { get; set; }
        public DateTime StartTime { get; set; }
    }
}
