using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Generator
{
    public class SuktDefaultSigningCredentialStore : ISuktSigningCredentialStore
    {
        private readonly SigningCredentials _credential;

        public SuktDefaultSigningCredentialStore(SigningCredentials credential)
        {
            _credential = credential;
        }

        public virtual Task<SigningCredentials> GetSigningCredentialsAsync()
        {
            return Task.FromResult(_credential);
        }
    }
}
