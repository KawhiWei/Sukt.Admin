using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StackExchange.Redis;
using System;
using System.Text;
using System.Threading.Tasks;
using Uwl.Common.Helper;

namespace MQTest
{
    class Program
    {
        private readonly string redisConnectionString;
        public volatile ConnectionMultiplexer redisConnection;
        private readonly object redisConnectionLock = new object();
        public Program()
        {
            var _redisConnection = Appsettings.app(new string[] { "RedisCaching", "ConnectionString" });//获取Redis链接字符串
            if (string.IsNullOrWhiteSpace(_redisConnection))
            {
                throw new ArgumentException("Redis链接字符串为空;请配置Redis链接服务器", nameof(_redisConnection));
            }
            this.redisConnectionString = _redisConnection;
            this.redisConnection = GetRedisConnection();///获取Redis链接实例服务
        }

        /// <summary>
        /// 核心代码，获取链接方式
        /// 通过双If 加 lock的方式实现单例模式
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetRedisConnection()
        {
            //如果已存在链接实例，直接返回
            if (this.redisConnection != null && this.redisConnection.IsConnected)
            {
                return this.redisConnection;
            }
            //加锁，防止异步编程中，出现单例无效的问题
            lock (redisConnectionLock)
            {
                if (this.redisConnection != null)
                {
                    this.redisConnection.Dispose();//如果存在实例链接释放Redis链接
                }
                try
                {
                    this.redisConnection = ConnectionMultiplexer.Connect(this.redisConnectionString);
                }
                catch (Exception)
                {
                    throw new Exception("Redis服务未启用，请开启该服务");
                }
            }
            return this.redisConnection;
        }
        /// <summary>
        /// 消息接收者（后台处理方法）
        /// </summary>
        /// <param name="args"></param>
        static async Task Main(string[] args)
        {
            await RabbitMQ();
            Program program = new Program();
            //await program.RedisMQ();
        }
        /// <summary>
        /// RabbitMQ消息读取
        /// </summary>
        /// <returns></returns>
        private static async Task RabbitMQ()
        {
            await Console.Out.WriteLineAsync("Start Send Msg");
            //创建连接工厂
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = Appsettings.app(new string[] { "RabbitMQConfig", "UserName" }),
                Password = Appsettings.app(new string[] { "RabbitMQConfig", "Password" }),
                HostName = Appsettings.app(new string[] { "RabbitMQConfig", "HostName" }),
                //Port = 15672,
                AutomaticRecoveryEnabled = Convert.ToBoolean(Appsettings.app(new string[] { "RabbitMQConfig", "AutomaticRecoveryEnabled" })),
                TopologyRecoveryEnabled = Convert.ToBoolean(Appsettings.app(new string[] { "RabbitMQConfig", "TopologyRecoveryEnabled" })),
            };
            //创建连接
            var connection = factory.CreateConnection();
            //创建通道
            var channel = connection.CreateModel();

            //接收到的消息处理事件
            EventingBasicConsumer Recipient = new EventingBasicConsumer(channel);
            Recipient.Received += (ch, ea) =>
            {
                var RecipientMsg = Encoding.UTF8.GetString(ea.Body);
                Console.WriteLine($"后台处理方法收到消息：{RecipientMsg}");
                //确认该消息已被处理
                channel.BasicAck(ea.DeliveryTag, false);
                Console.WriteLine($"消息已经处理【{ea.DeliveryTag}】");
            };
            channel.BasicConsume("Job1", false, Recipient);
            await Console.Out.WriteLineAsync("后台处理方法已启动");
            Console.ReadKey();
            channel.Dispose();
            connection.Close();
        }
        /// <summary>
        /// Redis消息队列读取(有问题还未解决)
        /// </summary>
        /// <returns></returns>
        private  async Task RedisMQ()
        {
            ISubscriber subcriber = redisConnection.GetSubscriber();
            string Msg = "";
            await subcriber.SubscribeAsync("redis", (channel, message) =>
            {
                Msg = message;
            });
            Console.WriteLine($"Redis消息队列收到的消息：{Msg}");


            Console.ReadKey();
        }
    }
}
