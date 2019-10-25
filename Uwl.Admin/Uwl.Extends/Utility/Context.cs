using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Uwl.Extends.Utility
{
    public class Context
    {
        //public static IConfiguration _configuration { get; set; }
        public static string GetCreateConnection()
        {
            var  _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                .AddJsonFile("appsettings.json", true, true).Build();
            var sqlconn = _configuration.GetConnectionString("SqlserverDefault");
            return sqlconn;
        }
    }
}
