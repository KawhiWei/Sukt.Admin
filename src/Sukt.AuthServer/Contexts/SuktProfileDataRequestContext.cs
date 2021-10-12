using Sukt.AuthServer.Domain.Models;
using Sukt.AuthServer.Validation.ValidationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Contexts
{
    /// <summary>
    /// 自定义添加用户信息传输上下文
    /// </summary>
    public class SuktProfileDataRequestContext
    {
        public SuktProfileDataRequestContext() { }
        public SuktProfileDataRequestContext(ClaimsPrincipal subject, SuktApplicationModel clientApplication, string caller, IEnumerable<string> requestedClaimTypes)
        {
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
            ClientApplication = clientApplication ?? throw new ArgumentNullException(nameof(clientApplication));
            Caller = caller ?? throw new ArgumentNullException(nameof(caller));
            RequestedClaimTypes = requestedClaimTypes ?? throw new ArgumentNullException(nameof(requestedClaimTypes));
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> RequestedClaimTypes { get; set; }
        /// <summary>
        /// 验证通过返回
        /// </summary>
        public ValidatedRequest ValidatedRequest { get; set; }
        /// <summary>
        /// 登录用户主题
        /// </summary>
        public ClaimsPrincipal Subject { get; set; }
        /// <summary>
        /// 客户端信息
        /// </summary>
        public SuktApplicationModel ClientApplication { get; set; }
        /// <summary>
        /// 调用者
        /// </summary>
        public string Caller { get; set; }
        /// <summary>
        /// 资源验证返回
        /// </summary>
        public ResourceValidationResult RequestedResources { get; set; }
        /// <summary>
        /// 添加的claim信息
        /// </summary>
        public List<Claim> IssuedClaims { get; set; } = new List<Claim>();
    }
}
