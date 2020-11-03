using Sukt.Core.Shared.Events;
using System.Collections.Generic;

namespace Sukt.Core.Shared.Audit
{
    public class AuditEvent : EventBase
    {
        public List<AuditEntryInputDto> AuditList { get; set; }
    }
}