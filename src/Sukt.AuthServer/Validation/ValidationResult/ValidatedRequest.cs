using Sukt.AuthServer.Domain.Models;
using Sukt.AuthServer.Generator;
using Sukt.Core.Domain.Models.IdentityServerFour;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation.ValidationResult
{
    /// <summary>
    /// 验证请求基类
    /// </summary>
    public class ValidatedRequest
    {
        public NameValueCollection Raw { get; set; }
        /// <summary>
        /// 客户端信息
        /// </summary>
        public SuktApplicationModel ClientApplication { get; set; }
        /// <summary>
        /// 用户主体信息
        /// </summary>
        public ClaimsPrincipal Subject { get; set; }
        /// <summary>
        /// AccessToken有效时长
        /// </summary>
        public int AccessTokenExpire { get; set; }
        /// <summary>
        /// 是否创建刷新AccessToken的RefreshToken
        /// </summary>
        public bool IsRefreshToken { get; set; }
        public string SessionId { get; set; }
        /// <summary>
        /// 客户端Id
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// Token 类型
        /// </summary>
        public TokenType TokenType { get; set; }
        /// <summary>
        /// 资源返回结果
        /// </summary>
        public ResourceValidationResult ResourceValidation { get; set; } = new ResourceValidationResult();
        public void SetClient(SuktApplicationModel suktApplication)
        {
            ClientApplication = suktApplication;
        }
    }
}
