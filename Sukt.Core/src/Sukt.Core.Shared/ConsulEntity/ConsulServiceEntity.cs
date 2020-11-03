namespace Sukt.Core.Shared.ConsulEntity
{
    /// <summary>
    /// 注册到Consul模型
    /// </summary>
    public class ConsulServiceEntity
    {
        /// <summary>
        /// 服务IP(自动获取)
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 服务端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Consul注册IP
        /// </summary>
        public string ConsulIP { get; set; }

        /// <summary>
        /// Consul注册端口
        /// </summary>
        public int ConsulPort { get; set; }
    }
}