using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Entity
{
    public class InputDto<Tkey>:IInputDto<Tkey>
    {
        public virtual Tkey Id { get; set; }
    }
}
