using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Extends.Utility;

namespace Uwl.Common.RabbitMQ
{
    public class RabbitServer: IRabbitMQ
    {
        private IConnection connection;
        private ConnectionFactory connectionFactory;
        public RabbitServer()
        {
            try
            {
                connectionFactory = new ConnectionFactory()
                {
                    UserName = "wzw",
                    Password = "wzw",
                    HostName = "localhost"
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IConnection GetConnection()
        {
            return this.connectionFactory.CreateConnection();
        }
        /// <summary>
        /// RabbitMQ指定队列名称模式发送消息
        /// </summary>
        /// <param name="queuename">队列名字</param>
        /// <param name="obj">传输数据</param>
        public void SendData(string queuename, object obj)
        {
            connection = GetConnection();
            if (obj == null)
                return;
            if (connection == null)
                return;
            if (queuename.IsNullOrEmpty())
                return;
            using (connection)
            {
                using (var channel= connection.CreateModel())
                {
                    //声明一个队列    //队列模式   一共有四种
                    channel.QueueDeclare(queuename, false, false, false, null);
                    //第一个参数：预计大小，第二个参数每次读取几个，第三个参数是否本地
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                    //交付模式
                    var prop = channel.CreateBasicProperties();
                    // 非持久性（1）或持久性（2）。
                    prop.DeliveryMode = 2;
                    //将对象转化为json字符串
                    var json = JsonConvert.SerializeObject(obj);
                    //将字符串转换为二进制
                    var bytes= Encoding.UTF8.GetBytes(json);
                    //开始传送
                    channel.BasicPublish("", queuename, prop,bytes);
                }
            }
        }
    }
}
