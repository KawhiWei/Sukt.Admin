using Microsoft.AspNetCore.Authentication;
using Sukt.AuthServer.Contexts;
using Sukt.AuthServer.Validation.ValidationResult;
using Sukt.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation.ResourceOwnerPassword
{
    public class DefaultResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly ISystemClock _clock;

        public DefaultResourceOwnerPasswordValidator(ISystemClock clock)
        {
            _clock = clock;
        }
        /// <summary>
        /// 验证账号密码
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            context.Result = new GrantValidationResult("", OidcConstants.AuthenticationMethods.Password, _clock.UtcNow.UtcDateTime);
            return Task.CompletedTask;
        }
    }
}
