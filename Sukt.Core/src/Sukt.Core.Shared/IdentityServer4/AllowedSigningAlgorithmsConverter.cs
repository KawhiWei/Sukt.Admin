using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sukt.Core.Shared
{
    public class AllowedSigningAlgorithmsConverter :
        IValueConverter<ICollection<string>, string>,
        IValueConverter<string, ICollection<string>>
    {
        public static AllowedSigningAlgorithmsConverter Converter = new AllowedSigningAlgorithmsConverter();

        public string Convert(ICollection<string> sourceMember, ResolutionContext context)
        {
            if (sourceMember == null || !sourceMember.Any())
            {
                return null;
            }
            return string.Join(',', sourceMember);
        }

        public ICollection<string> Convert(string sourceMember, ResolutionContext context)
        {
            var list = new HashSet<string>();
            if (!String.IsNullOrWhiteSpace(sourceMember))
            {
                sourceMember = sourceMember.Trim();
                foreach (var item in sourceMember.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct())
                {
                    list.Add(item);
                }
            }
            return list;
        }
    }

}
