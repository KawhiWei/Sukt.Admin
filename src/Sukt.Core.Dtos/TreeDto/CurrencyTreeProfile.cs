using AutoMapper;
using Sukt.Core.Domain.Models.Menu;

namespace Sukt.Core.Dtos.TreeDto
{
    public class CurrencyTreeProfile : Profile
    {
        public CurrencyTreeProfile()
        {
            CreateMap<MenuEntity, CurrencyTreeDto>()
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Key, opt => opt.MapFrom(x => x.Id));

        }
    }
}
