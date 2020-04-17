using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Extensions.ResultExtensions
{
    public abstract class ResultBaseTData<TData> : ResultBase, IResultData<TData>
    {
        public virtual IEnumerable<TData> Data { get; set ; }
    }
}
