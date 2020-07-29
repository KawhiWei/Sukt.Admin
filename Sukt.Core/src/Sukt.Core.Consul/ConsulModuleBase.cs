using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sukt.Core.Shared.ConsulEntity;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.Network;
using Sukt.Core.Shared.SuktAppModules;
using System;

namespace Sukt.Core.Consul
{
    public abstract class ConsulModuleBase: SuktAppModuleBase
    {
        /// <summary>
        /// 服务地址
        /// </summary>
        private string _serviceName = string.Empty;
        /// <summary>
        /// Consul服务地址
        /// </summary>
        private string _consulIp = string.Empty;
        /// <summary>
        /// Consul服务端口
        /// </summary>
        private int _consulPort = 80;
        /// <summary>
        /// docker容器内部端口
        /// </summary>
        private int _Prot = 80;

        public override IServiceCollection ConfigureServices(IServiceCollection service)
        {
            IConfiguration configuration = service.GetConfiguration();
            _consulIp = configuration["Consul:IP"];
            _consulPort = Convert.ToInt32(configuration["Consul:Port"]);
            _Prot = Convert.ToInt32(configuration["Service:Port"]);
            _serviceName = configuration["Service:Name"];
            return service;
        }
        public override void Configure(IApplicationBuilder applicationBuilder)
        {
            ConsulServiceEntity serviceEntity = new ConsulServiceEntity
            {
                IP = NetworkHelper.LocalIPAddress,
                Port = _Prot,//如果使用的是docker 进行部署这个需要和dockerfile中的端口保证一致
                ServiceName = _serviceName,
                ConsulIP = _consulIp,
                ConsulPort = _consulPort
            };
            var consulClient = new ConsulClient(x => x.Address = new Uri($"http://{serviceEntity.ConsulIP}:{serviceEntity.ConsulPort}"));//请求注册的 Consul 地址
            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                HTTP = $"http://{serviceEntity.IP}:{serviceEntity.Port}/api/health",//健康检查地址
                Timeout = TimeSpan.FromSeconds(1)
            };
            Console.WriteLine($"我是服务IP和端口：{serviceEntity.IP}:{serviceEntity.Port}");
            // Register service with consul
            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = Guid.NewGuid().ToString(),
                Name = serviceEntity.ServiceName,
                Address = serviceEntity.IP,
                Port = serviceEntity.Port,
                Tags = new[] { $"urlprefix-/{serviceEntity.ServiceName}" }//添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
            };
            consulClient.Agent.ServiceRegister(registration).Wait();//服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）
            var lifetime = applicationBuilder.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
            });
        }

    }
}
