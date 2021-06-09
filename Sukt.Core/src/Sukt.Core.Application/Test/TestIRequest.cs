using Sukt.Core.Dtos.Menu;
using Sukt.Module.Core.Events.EventBus;
using Sukt.Module.Core.OperationResult;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Application.Test
{
    public class TestIRequest : ITestIRequest
    {
        private readonly IMediatorHandler _mediatorbus;

        public TestIRequest(IMediatorHandler mediatorbus)
        {
            _mediatorbus = mediatorbus;
        }

        public async Task<OperationResponse> TestIRequset(string str)
        {
            await Task.CompletedTask;

            var result = await _mediatorbus.SendAsync(new TestEnevtRequest() { Test="1as3d13asd13as"});
            return new OperationResponse();
        }
    }
}
