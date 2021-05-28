//using AutoMapper;
//using Sukt.Core.Domain.Models.IdentityServerFour;
//using System.Collections.Generic;
//using System.Security.Claims;

//namespace Sukt.Core.API.Config.IdentityServerProfile
//{
//    /// <summary>
//    /// IdentityServer4客户端实体模型转Dto
//    /// </summary>
//    public class ClientMapperProfile : Profile
//    {
//        /// <summary>
//        ///
//        /// </summary>
//        public ClientMapperProfile()
//        {
//            CreateMap<ClientProperty, KeyValuePair<string, string>>()
//                 .ReverseMap();

//            CreateMap<Client, IdentityServer4.Models.Client>()
//                .ForMember(dest => dest.ProtocolType, opt => opt.Condition(srs => srs != null))
//                .ForMember(x => x.AllowedIdentityTokenSigningAlgorithms, opts => opts.ConvertUsing(AllowedSigningAlgorithmsConverter.Converter, x => x.AllowedIdentityTokenSigningAlgorithms))
//                .ReverseMap()
//                .ForMember(x => x.AllowedIdentityTokenSigningAlgorithms, opts => opts.ConvertUsing(AllowedSigningAlgorithmsConverter.Converter, x => x.AllowedIdentityTokenSigningAlgorithms));

//            CreateMap<ClientCorsOrigin, string>()
//                .ConstructUsing(src => src.Origin)
//                .ReverseMap()
//                .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src));

//            CreateMap<ClientIdPRestriction, string>()
//                .ConstructUsing(src => src.Provider)
//                .ReverseMap()
//                .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src));

//            CreateMap<ClientClaim, IdentityServer4.Models.ClientClaim>(MemberList.None)
//                .ConstructUsing(src => new IdentityServer4.Models.ClientClaim(src.Type, src.Value, ClaimValueTypes.String))
//                .ReverseMap();

//            CreateMap<ClientScope, string>()
//                .ConstructUsing(src => src.Scope)
//                .ReverseMap()
//                .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src));

//            CreateMap<ClientPostLogoutRedirectUri, string>()
//                .ConstructUsing(src => src.PostLogoutRedirectUri)
//                .ReverseMap()
//                .ForMember(dest => dest.PostLogoutRedirectUri, opt => opt.MapFrom(src => src));

//            CreateMap<ClientRedirectUri, string>()
//                .ConstructUsing(src => src.RedirectUri)
//                .ReverseMap()
//                .ForMember(dest => dest.RedirectUri, opt => opt.MapFrom(src => src));

//            CreateMap<ClientGrantType, string>()
//                .ConstructUsing(src => src.GrantType)
//                .ReverseMap()
//                .ForMember(dest => dest.GrantType, opt => opt.MapFrom(src => src));

//            CreateMap<ClientSecret, IdentityServer4.Models.Secret>(MemberList.Destination)
//                .ForMember(dest => dest.Type, opt => opt.Condition(srs => srs != null))
//                .ReverseMap();

//        }
//    }
//}
