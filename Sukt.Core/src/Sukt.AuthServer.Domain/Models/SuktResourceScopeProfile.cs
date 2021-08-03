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
    public class SuktResourceScopeProfile : Profile
    {
        public SuktResourceScopeProfile()
        {
            CreateMap<SuktResourceScope, SuktResourceScopeModel>()
                .ForMember(x=>x.Resources,opt=>opt.MapFrom(x=> JsonConvert.DeserializeObject<ICollection<string>>(x.Resources)));
        }
    }
}
