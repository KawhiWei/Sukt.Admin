using Microsoft.Extensions.Logging;
using Sukt.AuthServer.Contexts;
using Sukt.AuthServer.Domain.Enums;
using Sukt.AuthServer.Validation.ValidationResult;
using Sukt.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation
{
    public class NotSupportedResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly ILogger _logger;

        public NotSupportedResourceOwnerPasswordValidator(ILogger<NotSupportedResourceOwnerPasswordValidator> logger)
        {
            _logger = logger;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.UnsupportedGrantType);
            return Task.CompletedTask;
        }
    }
}
