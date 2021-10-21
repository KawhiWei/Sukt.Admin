using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Generator
{
    public interface ISuktSigningCredentialStore
    {
        Task<SigningCredentials> GetSigningCredentialsAsync();
    }
}
