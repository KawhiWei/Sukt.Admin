using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Constants
{
    public class EndpointAuthenticationMethods
    {
        public const string PostBody = "client_secret_post";
        public const string BasicAuthentication = "client_secret_basic";
        public const string PrivateKeyJwt = "private_key_jwt";
        public const string TlsClientAuth = "tls_client_auth";
        public const string SelfSignedTlsClientAuth = "self_signed_tls_client_auth";
    }
}
