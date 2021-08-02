using Sukt.AuthServer.Validation.ValidationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation
{
    public class DefaultResourceValidator: IResourceValidator
    {
        public virtual async Task<ResourceValidationResult> ValidateRequestedResourcesAsync(ResourceValidationRequest request)
        {

        }
    }
}
