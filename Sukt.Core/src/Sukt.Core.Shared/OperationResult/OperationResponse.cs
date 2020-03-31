using Sukt.Core.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.OperationResult
{
  public  class OperationResponse: OperationResponse<object>
    {
        public OperationResponse() : base(OperationEnumType.Success)
        {


        }
        public OperationResponse(OperationEnumType type = OperationEnumType.Success) : base("", null, type)
        {


        }
        public OperationResponse(string message, OperationEnumType type) : base(message, null, type)
        {


        }
        public OperationResponse(string message, object data, OperationEnumType type) : base(message, data, type)
        {
        }
    }
}
