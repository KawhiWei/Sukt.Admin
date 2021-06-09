using Sukt.Core.Dtos.Menu;
using Sukt.Module.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Application.Test
{
    public class TestEnevtRequestHandle : RequestHandlerBase<TestEnevtRequest, MenuInputDto>
    {
        public override async Task<MenuInputDto> Handle(TestEnevtRequest notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return new MenuInputDto() { Name="我是王爸爸"};
        }
    }
}
