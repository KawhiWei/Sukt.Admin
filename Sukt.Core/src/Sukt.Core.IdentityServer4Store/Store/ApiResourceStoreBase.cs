using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.Core.IdentityServer4Store.Store
{
    public class ApiResourceStoreBase : IResourceStore
    {
        private readonly IEFCoreRepository<ApiResource, Guid> _apiResourceRepository;
        private readonly IEFCoreRepository<IdentityResource, Guid> _identityResourceRepository;
        private readonly IEFCoreRepository<ApiScope, Guid> _apiScopeRepository;
        private readonly ILogger<ApiResourceStoreBase> _logger;

        public ApiResourceStoreBase(IEFCoreRepository<ApiResource, Guid> apiResourceRepository, IEFCoreRepository<IdentityResource, Guid> identityResourceRepository, IEFCoreRepository<ApiScope, Guid> apiScopeRepository, ILogger<ApiResourceStoreBase> logger)
        {
            _apiResourceRepository = apiResourceRepository;
            _identityResourceRepository = identityResourceRepository;
            _apiScopeRepository = apiScopeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<IdentityServer4.Models.ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            var apis = await _apiResourceRepository.NoTrackEntities.Where(x => apiResourceNames.Contains(x.Name))
                .Include(x => x.Secrets)
                .Include(x => x.Scopes)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties).ToArrayAsync();
            return apis.Select(x => x.MapTo<IdentityServer4.Models.ApiResource>());
        }

        public async Task<IEnumerable<IdentityServer4.Models.ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var query = from api in _apiResourceRepository.NoTrackEntities
                        where api.Scopes.Where(x => scopeNames.Contains(x.Scope)).Any()
                        select api;
            var apis = await query.Include(x => x.Secrets)
                .Include(x => x.Scopes)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties).ToListAsync();
            var results = apis.Where(api => api.Scopes.Any(x => scopeNames.Contains(x.Scope)));
            return results.Select(x => x.MapTo<IdentityServer4.Models.ApiResource>());
        }

        public async Task<IEnumerable<IdentityServer4.Models.ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            return (await _apiScopeRepository.NoTrackEntities.Where(x => scopeNames.Contains(x.Name)).Include(x => x.UserClaims).Include(x => x.Properties).ToListAsync()).Where(x => scopeNames.Contains(x.Name)).Select(x => x.MapTo<IdentityServer4.Models.ApiScope>());
        }

        public async Task<IEnumerable<IdentityServer4.Models.IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            return (await _identityResourceRepository.NoTrackEntities.Where(x => scopeNames.Contains(x.Name)).Include(x => x.UserClaims).Include(x => x.Properties).ToListAsync()).Where(x => scopeNames.Contains(x.Name)).Select(x => x.MapTo<IdentityServer4.Models.IdentityResource>());
        }

        public async Task<IdentityServer4.Models.Resources> GetAllResourcesAsync()
        {
            var identityResources = (await _identityResourceRepository.NoTrackEntities.Include(x => x.UserClaims).Include(x => x.Properties).ToListAsync()).Select(x => x.MapTo<IdentityServer4.Models.IdentityResource>());
            var apiResources = (await _apiResourceRepository.NoTrackEntities.Include(x => x.Secrets)
                 .Include(x => x.Scopes)
                 .Include(x => x.UserClaims)
                 .Include(x => x.Properties).ToListAsync()).Select(x => x.MapTo<IdentityServer4.Models.ApiResource>());
            var apiScopes = (await _apiScopeRepository.NoTrackEntities.Include(x => x.UserClaims)
                .Include(x => x.Properties).ToListAsync()).Select(x => x.MapTo<IdentityServer4.Models.ApiScope>());
            return new IdentityServer4.Models.Resources(identityResources, apiResources, apiScopes);
        }
    }
}
