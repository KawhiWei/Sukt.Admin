using AutoMapper;
using Newtonsoft.Json;
using Sukt.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Domain.Models
{
    public class SuktApplicationProfile : Profile
    {
        public SuktApplicationProfile()
        {
            CreateMap<SuktApplication, SuktApplicationModel>()
                .ForMember(x=>x.ClientScopes,opt=>opt.MapFrom(x=>JsonConvert.DeserializeObject<ICollection<string>>(x.ClientScopes)))
                .ForMember(x => x.RedirectUris, opt => opt.MapFrom(x => JsonConvert.DeserializeObject<ICollection<string>>(x.RedirectUris)))
                .ForMember(x => x.PostLogoutRedirectUris, opt => opt.MapFrom(x => JsonConvert.DeserializeObject<ICollection<string>>(x.PostLogoutRedirectUris)));
        }
    }
}
