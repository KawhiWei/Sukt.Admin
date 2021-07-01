using Microsoft.Extensions.Logging;
using Sukt.AuthServer.Domain.SuktAuthServer.SuktApplicationStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation
{
    public class AuthorizeRequestValidator: IAuthorizeRequestValidator
    {
        private readonly ISuktApplicationStore _suktApplicationStore;
        private readonly ILogger _logger;
    }
}
