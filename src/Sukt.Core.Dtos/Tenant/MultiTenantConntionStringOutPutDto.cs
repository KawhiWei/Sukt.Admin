using AutoMapper;
using Sukt.Core.Domain.Models.Tenant;
using Sukt.Module.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Dtos.Tenant
{
    [AutoMap(typeof(MultiTenantConnectionString))]
    public class MultiTenantConnectionStringOutPutDto : OutputDtoBase<Guid>
    {
        public Guid TenantId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
