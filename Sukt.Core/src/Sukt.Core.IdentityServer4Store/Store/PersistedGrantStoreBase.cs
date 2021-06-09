using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.Core.IdentityServer4Store.Store
{
    public class PersistedGrantStoreBase : IPersistedGrantStore
    {
        private readonly IEFCoreRepository<PersistedGrant, Guid> _persistedGrantRepository;
        private readonly ILogger<PersistedGrantStoreBase> _logger;

        public PersistedGrantStoreBase(IEFCoreRepository<PersistedGrant, Guid> persistedGrantRepository, ILogger<PersistedGrantStoreBase> logger)
        {
            _persistedGrantRepository = persistedGrantRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<IdentityServer4.Models.PersistedGrant>> GetAllAsync(PersistedGrantFilter filter)
        {
            Validate(filter);
            var list = await _persistedGrantRepository.TrackEntities.Where(x => (string.IsNullOrWhiteSpace(filter.SubjectId) || x.SubjectId == filter.SubjectId) &&
                                   (string.IsNullOrWhiteSpace(filter.ClientId) || x.ClientId == filter.ClientId) &&
                                   (string.IsNullOrWhiteSpace(filter.Type) || x.Type == filter.Type)).ToListAsync();
            var dto = list.Select(x => x.MapTo<IdentityServer4.Models.PersistedGrant>());
            return dto;
        }

        public async Task<IdentityServer4.Models.PersistedGrant> GetAsync(string key)
        {
            var model = await _persistedGrantRepository.TrackEntities.Where(x => x.Key == key).FirstOrDefaultAsync();
            _logger.LogDebug("{persistedGrantKey} found in database: {persistedGrantKeyFound}", key, model != null);
            var dto = model.MapTo<IdentityServer4.Models.PersistedGrant>();
            return dto;
        }

        public async Task RemoveAllAsync(PersistedGrantFilter filter)
        {
            Validate(filter);
            await _persistedGrantRepository.DeleteBatchAsync(x => (string.IsNullOrWhiteSpace(filter.SubjectId) || x.SubjectId == filter.SubjectId) &&
                     (string.IsNullOrWhiteSpace(filter.ClientId) || x.ClientId == filter.ClientId) &&
                     (string.IsNullOrWhiteSpace(filter.Type) || x.Type == filter.Type));
        }

        private void Validate(PersistedGrantFilter filter)
        {
            filter.NotNull(nameof(filter));
            if (filter.ClientId.IsNullOrEmpty() &&
                filter.SubjectId.IsNullOrEmpty() &&
                filter.Type.IsNullOrEmpty())
            {
                throw new ArgumentException("No filter values set.", nameof(filter));
            }
        }

        public async Task RemoveAsync(string key)
        {
            await _persistedGrantRepository.DeleteBatchAsync(x => x.Key == key);
        }

        public async Task StoreAsync(IdentityServer4.Models.PersistedGrant grant)
        {
            var model = grant.MapTo<PersistedGrant>();
            await _persistedGrantRepository.InsertAsync(model);
        }
    }
}
