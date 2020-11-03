using System;

namespace Sukt.Core.Shared.Security.Jwt
{
    public interface IJwtBearerService : IScopedDependency
    {
        JwtResult CreateToken(Guid userId, string userName);
    }
}