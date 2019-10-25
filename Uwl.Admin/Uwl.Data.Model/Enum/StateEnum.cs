using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Uwl.Data.Model.Enum
{
    /// <summary>
    /// 公共枚举状态类型
    /// </summary>
    public enum StateEnum
    {
        [Display(Name = "全部")]
        All = -1,
        [Display(Name ="正常")]
        Normal=0,
        [Display(Name = "冻结")]
        Frozen = 1,
    }
}
