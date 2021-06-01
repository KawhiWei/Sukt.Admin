using SuktCore.Shared;
using SuktCore.Shared.OperationResult;
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
