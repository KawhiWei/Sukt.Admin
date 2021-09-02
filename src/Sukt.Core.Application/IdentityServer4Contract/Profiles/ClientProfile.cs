using AutoMapper;
using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Core.Dtos.IdentityServer4Dto;
using Sukt.Module.Core.IdentityServer4;
using System;
using System.Collections.Generic;

namespace Sukt.Core.Application.IdentityServer4Contract.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<DateTime, DateTimeOffset>().ReverseMap().ConvertUsing(new DateTimeTypeConverter());

            CreateMap<ClientProperty, KeyValuePair<string, string>>()
                 .ReverseMap();

            CreateMap<Client, ClientInputDto>()
                //.ForMember(dest => dest.ProtocolType, opt => opt.Condition(srs => srs != null))
                //.ForMember(x => x.AllowedIdentityTokenSigningAlgorithms, opts => opts.ConvertUsing(AllowedSigningAlgorithmsConverter.Converter, x => x.AllowedIdentityTokenSigningAlgorithms))
                .ReverseMap();

            CreateMap<ClientCorsOrigin, string>()
                .ConstructUsing(src => src.Origin)
                .ReverseMap()
                .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src));

            CreateMap<ClientIdPRestriction, string>()
                .ConstructUsing(src => src.Provider)
                .ReverseMap()
                .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src));

            //CreateMap<ClientClaim, ClientClaim>(MemberList.None)
            //    .ConstructUsing(src => new ClientClaim(src.Type, src.Value, ClaimValueTypes.String))
            //    .ReverseMap();

            //CreateMap<ClientScope, string>()
            //    .ConstructUsing(src => src.Scope)
            //    .ReverseMap()
            //    .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src));

            CreateMap<ClientPostLogoutRedirectUri, string>()
                .ConstructUsing(src => src.PostLogoutRedirectUri)
                .ReverseMap()
                .ForMember(dest => dest.PostLogoutRedirectUri, opt => opt.MapFrom(src => src));

            CreateMap<ClientRedirectUri, string>()
                .ConstructUsing(src => src.RedirectUri)
                .ReverseMap()
                .ForMember(dest => dest.RedirectUri, opt => opt.MapFrom(src => src));

            CreateMap<ClientGrantType, string>()
                .ConstructUsing(src => src.GrantType)
                .ReverseMap()
                .ForMember(dest => dest.GrantType, opt => opt.MapFrom(src => src));

            //CreateMap<string, ClientSecret>()
            //    .ForMember(d => d.Value, opt => opt.MapFrom(s => s));

            //CreateMap<ClientSecret, Secret>()
            //    .ForMember(dest => dest.Type, opt => opt.Condition(srs => srs != null))
            //    .ReverseMap();
        }
    }
}
