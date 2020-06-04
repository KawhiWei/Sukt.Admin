using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace Sukt.Core.SeriLog
{
    public class SeriLogLogger
    {
        /// <summary>
        /// SeriLog记录日志到文件
        /// </summary>
        /// <param name="fileName"></param>
        public static void SetSeriLoggerToFile(string fileName)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Map(le => MapData(le),
                (key, log) =>
                 log.Async(o => o.File(Path.Combine(fileName, @$"{key.time:yyyy-MM-dd}\{key.level.ToString().ToLower()}.txt"))))
                .CreateLogger();

            (DateTime time, LogEventLevel level) MapData(LogEvent logEvent)
            {

                return (new DateTime(logEvent.Timestamp.Year, logEvent.Timestamp.Month, logEvent.Timestamp.Day, logEvent.Timestamp.Hour, logEvent.Timestamp.Minute, logEvent.Timestamp.Second), logEvent.Level);
            }
        }


    }
}
