using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.OperationResult
{
  public abstract  class ResultBase
    {      
        public virtual bool Successed { get; set; }
        public virtual string Message { get; set; }
    }
}
