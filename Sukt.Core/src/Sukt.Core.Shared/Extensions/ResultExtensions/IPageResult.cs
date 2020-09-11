using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Extensions.ResultExtensions
{
    public interface IPageResult<TModel>: IResultBase, IListResult<TModel>
    {
        int Total { get; }
    }
}
