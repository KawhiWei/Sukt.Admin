using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sukt.Core.Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Audit
{
    public class AuditEvent : EventBase
    {
        public List<AuditEntryInputDto> AuditList { get; set; }
    }
}
