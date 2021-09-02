using AutoMapper;
using Sukt.Core.Domain.Models.IdentityServerFour;

namespace Sukt.Core.IdentityServerFourStore.IdentityServerProfile
{
    public class PersistedGrantMapperProfile : Profile
    {
        public PersistedGrantMapperProfile()
        {
            CreateMap<PersistedGrant, IdentityServer4.Models.PersistedGrant>(MemberList.Destination)
                            .ReverseMap();
        }
    }
}
