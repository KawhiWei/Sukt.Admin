using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Common.Helper
{
    /// <summary>
    /// 全局获取appsettings.json
    /// </summary>
    public class Appsettings
    {
        //定义全局变量
        static IConfiguration Configuration { get; set; }
        static Appsettings()
        {
            Configuration = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true }).Build();
        }
        public static string app(params string[] sections)
        {
            try
            {
                var val = string.Empty;
                for (int i = 0; i < sections.Length; i++)
                {
                    val += sections[i] + ":";
                }
                return Configuration[val.TrimEnd(':')];//去除最后一个冒号
            }
            catch (Exception)
            {

                return "";
            }
        }
    }
}
