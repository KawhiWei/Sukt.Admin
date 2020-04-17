using AutoMapper;
using Sukt.Core.Domain.Models.DataDictionary;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Dtos.DataDictionaryDto
{
    public class DictionaryProfile: Profile
    {
        public DictionaryProfile()
        {
            CreateMap<DataDictionaryEntity, TreeDictionaryOutDto>().ForMember(x => x.title, opt => opt.MapFrom(x => x.Title));
        }
    }
}
