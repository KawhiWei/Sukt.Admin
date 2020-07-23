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
    public enum AjaxResultType
    {
        /// <summary>
        /// 消息结果
        /// </summary>
        [Description("消息结果")]

        Info = 203,

        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 200,

        /// <summary>
        /// 错误
        /// </summary>
        [Description("错误")]
        Error = 500,

        /// <summary>
        /// 未经授权
        /// </summary>
        [Description("未经授权")]
        Unauthorized = 401,

        /// <summary>
        /// 已登录但权限不足
        /// </summary>
        [Description("当前用户权限不足")]
        Uncertified = 403,

        /// <summary>
        /// 功能资源找不到
        /// </summary>
        [Description("当前功能资源找不到")]
        NoFound = 404
    }
}
