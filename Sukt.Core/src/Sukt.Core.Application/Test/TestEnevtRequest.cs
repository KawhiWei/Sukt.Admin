using Sukt.Core.Dtos.Menu;
using Sukt.Module.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Application.Test
{
    public class TestEnevtRequest: RequestEventBase<MenuInputDto>
    {

        public string Test { get; set; }
    }
}
