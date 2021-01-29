using Sukt.Core.Shared.Entity;
using System;
using System.Collections.Generic;

namespace Sukt.Core.Dtos.IdentityServer4Dto.Client
{
    public class ClientCommonInputDto : InputDto<Guid>
    {
        public List<string> Allowed { get; set; }
    }
}
