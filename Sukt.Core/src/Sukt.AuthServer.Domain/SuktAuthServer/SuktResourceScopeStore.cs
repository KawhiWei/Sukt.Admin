using Microsoft.EntityFrameworkCore;
using Sukt.AuthServer.Domain.Models;
using Sukt.Core.Domain.Models;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Domain.SuktAuthServer
{
    public class SuktResourceScopeStore : ISuktResourceScopeStore
    {
        private readonly IEFCoreRepository<SuktResourceScope, Guid> _suktResourceScopeRepository;

        public SuktResourceScopeStore(IEFCoreRepository<SuktResourceScope, Guid> suktResourceScopeRepository)
        {
            _suktResourceScopeRepository = suktResourceScopeRepository;
        }

        public virtual async Task<SuktResource> FindResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var resources = await _suktResourceScopeRepository.NoTrackEntities.Where(x => scopeNames.Contains(x.Resources)).ToListAsync();
            return new SuktResource { SuktResources = resources.MapTo<ICollection<SuktResourceScopeModel>>() };
        }
    }
}
