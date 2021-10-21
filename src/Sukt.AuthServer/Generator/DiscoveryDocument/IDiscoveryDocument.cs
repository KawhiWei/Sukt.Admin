using Sukt.AuthServer.EndpointHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Generator.DiscoveryDocument
{
    public interface IDiscoveryDocument
    {
        Task<Dictionary<string, object>> GetDocumentsAsync(string baseUrl, string issureUrl);

        Task<IEnumerable<SuktJsonWebKey>> GetJwkDocumentAsync();
    }
}
