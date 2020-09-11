using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Extensions.ResultExtensions
{
    public interface IListResult<T>:IResultBase
    {
        IReadOnlyList<T> Data { get; set; }
    }
}
