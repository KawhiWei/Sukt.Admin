using Microsoft.IdentityModel.Tokens;
using Sukt.Core.Shared.AppOption;
using Sukt.Core.Shared.Exceptions;
using Sukt.Core.Shared.Extensions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sukt.Core.Shared.Security.Jwt
{
    public class JwtBearerService : IJwtBearerService
    {
        private readonly IServiceProvider _provider = null;
        private readonly JwtOptions _jwtOptions = null;
        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

        public JwtBearerService(IServiceProvider provider)
        {
            _provider = provider;
            _jwtOptions = provider.GetAppSettings()?.Jwt;
        }

        public JwtResult CreateToken(Guid userId, string userName)
        {
            Claim[] claims =
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, userName),
            };
            var (token, accessExpires) = this.BuildJwtToken(claims, _jwtOptions);

            return new JwtResult()
            {
                AccessToken = token,
                AccessExpires = accessExpires.UnixTicks().AsTo<long>(),
                claims = claims,
            };
        }

        private (string, DateTime) BuildJwtToken(Claim[] claims, JwtOptions options)
        {
            options.NotNull(nameof(options));
            claims.NotNull(nameof(claims));
            if (options.SecretKey.IsNullOrEmpty())
            {
                throw new SuktAppException("密钥不能为空!!");
            }
            DateTime now = DateTime.UtcNow;
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            double minutes = options.ExpireMins <= 0 ? 5 : options.ExpireMins;
            DateTime expires = now.AddMinutes(minutes);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = options.Audience ?? "SuktCore",
                Issuer = options.Issuer ?? "SuktCore",
                SigningCredentials = credentials,
                NotBefore = now,
                IssuedAt = now,
                Expires = expires
            };
            SecurityToken token = _tokenHandler.CreateToken(descriptor);
            return (_tokenHandler.WriteToken(token), expires);
        }
    }
}