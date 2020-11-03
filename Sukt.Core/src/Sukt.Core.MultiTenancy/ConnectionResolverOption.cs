namespace Sukt.Core.MultiTenancy
{
    public class ConnectionResolverOption
    {
        public string Key { get; set; } = "default";

        public ConnectionResolverType Type { get; set; }

        public string ConnectinStringName { get; set; }

        public DatabaseIntegration DBType { get; set; }
    }

    public enum ConnectionResolverType
    {
        Default = 0,
        ByDatabase = 1,
        ByTabel = 2,
        BySchema = 3
    }

    public enum DatabaseIntegration
    {
        None = 0,
        Mysql = 1,
        SqlServer = 2
    }
}