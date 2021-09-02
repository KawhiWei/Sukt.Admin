using Sukt.Module.Core;
using Sukt.Module.Core.OperationResult;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Application.Test
{
    public interface ITestIRequest : IScopedDependency
    {
        Task<OperationResponse> TestIRequset(string str);
    }
}
