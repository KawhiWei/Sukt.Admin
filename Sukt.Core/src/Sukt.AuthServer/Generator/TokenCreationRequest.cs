using Sukt.AuthServer.Validation.ValidationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Generator
{
    public class TokenCreationRequest
    {
        /// <summary>
        /// 用户主体信息
        /// </summary>
        public ClaimsPrincipal Subject { get; set; }
        /// <summary>
        /// 验证请求信息
        /// </summary>
        public ValidatedRequest ValidatedRequest { get; set; }
        /// <summary>
        /// 资源验证返回结构
        /// </summary>
        public ResourceValidationResult ResourceValidation { get; set; } = new ResourceValidationResult();
    }
}
