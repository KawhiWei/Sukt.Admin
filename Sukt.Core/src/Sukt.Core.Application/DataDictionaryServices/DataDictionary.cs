using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Application.Contracts.IDataDictionaryServices;
using Sukt.Core.Shared.Attributes.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Application.DataDictionaryServices
{
    [Dependency(ServiceLifetime.Scoped)]
    public class DataDictionary : IDataDictionary
    {
        public void GetDataDictionaryAsync()
        {

            Console.WriteLine("我被调用了");
        }
    }
}
