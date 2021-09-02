using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Generator
{
    /// <summary>
    /// Token生成服务接口
    /// </summary>
    public interface ITokenService
    {
        Task<TokenRequest> CreateTokenRequestAsync(TokenCreationRequest request);

        Task<string> CreateAccessTokenAsync(TokenRequest request);
    }
}
