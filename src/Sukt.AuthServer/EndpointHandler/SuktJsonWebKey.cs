using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.EndpointHandler
{
    public class SuktJsonWebKey
    {
        public string kty { get; set; }
        public string use { get; set; }
        public string kid { get; set; }
        public string x5t { get; set; }
        public string e { get; set; }
        public string n { get; set; }
        public string[] x5c { get; set; }
        public string alg { get; set; }

        public string x { get; set; }
        public string y { get; set; }
        public string crv { get; set; }
    }
}
