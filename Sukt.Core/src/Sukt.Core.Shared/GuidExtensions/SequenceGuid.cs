using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.GuidExtensions
{
    public static class SequenceGuid
    {
        /// <summary>
        /// 生成有序的Guid扩展
        /// </summary>
        /// <returns></returns>
        public static Guid GuidSequence()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();
            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;
            // 获取用于生成字节字符串的天数和毫秒数
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;
            // 转换为字节数组 
            // 注意，SQL Server精确到1/300毫秒，所以我们除以3.333333
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));
            // 反转字节以匹配SQL服务器顺序
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);
            // 将字节复制到guid中
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
            return new Guid(guidArray);
        }
    }
}
