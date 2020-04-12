using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sukt.Core.Shared.Enums
{
    public enum OperationEnumType
    {
        [Description("操作成功")]
        Success = 0,

        [Description("操作引发错误")]
        Error = 5,


        [Description("系统出现异常")]
        Exp = 10,

        [Description("数据源不存在")]
        QueryNull = 15,


        [Description("操作没有引发任何变化")]
        NoChanged = 20,
    }
    /// <summary>
    /// 排序方向
    /// </summary>
    public enum SortDirection
    {

        Ascending = 0,

        Descending = 1
    }
}
