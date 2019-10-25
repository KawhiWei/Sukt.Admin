using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Utility.Common
{
    public enum ResultType
    {
        /// <summary>
        ///     操作成功
        /// </summary>
        [Display(Name = "操作成功。")]
        Success = 200,

        /// <summary>
        ///     操作取消或操作没引发任何变化
        /// </summary>
        [Display(Name = "操作没有引发任何变化，提交取消。")]
        NoChanged = 1,

        /// <summary>
        ///     参数错误
        /// </summary>
        [Display(Name = "参数错误。")]
        ParamError = 2,

        /// <summary>
        ///     指定参数的数据不存在
        /// </summary>
        [Display(Name = "指定参数的数据不存在。")]
        QueryNull = 4,

        /// <summary>
        ///     权限不足
        /// </summary>
        [Display(Name = "当前用户权限不足，不能继续操作。")]
        PurviewLack = 8,

        /// <summary>
        ///     非法操作
        /// </summary>
        [Display(Name = "非法操作。")]
        IllegalOperation = 16,

        /// <summary>
        ///     警告
        /// </summary>
        [Display(Name = "警告")]
        Warning = 32,

        /// <summary>
        ///     操作引发错误
        /// </summary>
        [Display(Name = "操作引发错误。")]
        Error = 64,
    }
}
