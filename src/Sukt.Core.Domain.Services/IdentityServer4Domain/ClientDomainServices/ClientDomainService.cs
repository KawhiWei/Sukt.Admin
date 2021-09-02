using Microsoft.EntityFrameworkCore;
using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Module.Core;
using Sukt.Module.Core.OperationResult;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace Sukt.Core.Domain.Services.IdentityServer4Domain.ClientDomainServices
{
    public class ClientDomainService : IClientDomainService
    {
        private readonly IAggregateRootRepository<Client, Guid> _clientRepository;

        public ClientDomainService(IAggregateRootRepository<Client, Guid> clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<Client> GetLoadByIdAsync(Guid id)
        {
            return await _clientRepository.NoTrackEntities.Where(x => x.Id == id).Include(x => x.AllowedGrantTypes)
                .Include(x => x.RedirectUris)
                .Include(x => x.AllowedScopes)
                .Include(x => x.PostLogoutRedirectUris)
                .Include(x => x.ClientSecrets).SingleOrDefaultAsync();
        }
        public async Task<OperationResponse> CreateAsync(Client client)
        {
            return await _clientRepository.InsertAsync(client);
        }
        public async Task<OperationResponse> UpdateAsync(Client client)
        {
            return await _clientRepository.UpdateAsync(client);
        }
    }
}
