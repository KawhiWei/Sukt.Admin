using System.Collections.Generic;

namespace Sukt.Core.Shared.Extensions.ResultExtensions
{
    public class TreeData<TData> : ResultBaseTData<TData>
    {
        public TreeData() : this(new TData[0], "查询数据成功", true)
        {
        }

        public TreeData(IEnumerable<TData> data, string message = "查询数据成功", bool success = true)
        {
            Data = data;
            Message = message;
            Success = success;
        }
    }
}