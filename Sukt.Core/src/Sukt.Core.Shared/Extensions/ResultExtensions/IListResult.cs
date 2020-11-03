using System.Collections.Generic;

namespace Sukt.Core.Shared.Extensions.ResultExtensions
{
    public interface IListResult<T> : IResultBase
    {
        IReadOnlyList<T> Data { get; set; }
    }
}