using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Common.Performance
{
    public class Counter
    {
        /// <summary>
        /// 当前时间戳
        /// </summary>
        public double Time { get; set; }
        /// <summary>
        /// CPU负载
        /// </summary>
        public double CpuLoad { get; set; }

        /// <summary>
        /// CPU温度
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// 内存使用率
        /// </summary>
        public double MemoryUsage { get; set; }

        /// <summary>
        /// 磁盘读
        /// </summary>
        public double DiskRead { get; set; }

        /// <summary>
        /// 磁盘写
        /// </summary>
        public double DiskWrite { get; set; }

        /// <summary>
        /// 网络上行
        /// </summary>
        public double Upload { get; set; }

        /// <summary>
        /// 网络下行
        /// </summary>
        public double Download { get; set; }
    }
    public class GetCurrent
    {
        public static string CurrentPerformanceCounter()
        {
            //double time = DateTime.Now.GetTotalMilliseconds();
            //float load = SystemInfo.CpuLoad;
            //double temperature = SystemInfo.GetCPUTemperature();
            //double mem = (1 - SystemInfo.MemoryAvailable.To<double>() / SystemInfo.PhysicalMemory.To<double>()) * 100
            return "";

        }
    }
}
