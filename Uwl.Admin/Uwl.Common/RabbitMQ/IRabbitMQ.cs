using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Common.RabbitMQ
{
    public interface IRabbitMQ
    {
        IConnection GetConnection();
        void SendData(string queuename, object obj);
    }
}
