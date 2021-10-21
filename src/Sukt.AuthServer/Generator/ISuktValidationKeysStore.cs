using Sukt.AuthServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Generator
{
    public interface ISuktValidationKeysStore
    {
        Task<IEnumerable<SuktSecurityKeyInfo>> GetValidationKeysAsync();
    }
}
