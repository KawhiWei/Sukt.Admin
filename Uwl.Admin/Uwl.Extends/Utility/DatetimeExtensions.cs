using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Extends.Utility
{
    public static class DatetimeExtensions
    {
        /// <summary>
        /// 将字符串转换为TimeSpan类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this string str)
        {
            TimeSpan span;
            if (TimeSpan.TryParse(str, out span))
            {
                return span;
            }
            else
            {
                return TimeSpan.Parse("00:00:00");
            }
        }
    }
}
