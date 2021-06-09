using Sukt.Module.Core.Entity;
using System;
using System.Collections.Generic;

namespace Sukt.Core.Dtos.IdentityServer4Dto
{
    public class CommonInputDto : InputDto<Guid>
    {
        public List<string> Allowed { get; set; }
    }
}
