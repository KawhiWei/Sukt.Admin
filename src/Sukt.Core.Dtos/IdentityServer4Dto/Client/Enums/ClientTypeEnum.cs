namespace Sukt.Core.Dtos.IdentityServer4Dto.Enums
{
    /// <summary>
    /// 客户端类型
    /// </summary>
    public enum ClientTypeEnum
    {
        Implicit = 0,
        ImplicitAndClientCredentials = 5,
        Code = 10,
        Hybrid = 15,
        HybridAndClientCredentials = 20,
        ClientCredentials = 25,
        ResourceOwnerPassword = 30,
        ResourceOwnerPasswordAndClientCredentials = 35,
        DeviceFlow = 40,
    }
}
