using System.Security.Claims;

namespace Sukt.Core.Shared.Security.Jwt
{
    public class JwtResult
    {
        public string AccessToken { get; set; }

        public long AccessExpires { get; set; }

        public Claim[] claims { get; set; } = new Claim[] { };
    }
}