using System;
using System.Collections.Generic;
using System.Text;

using AutoMapper;

namespace aiof.api.data
{
    public class AutoMappingProfileDto : Profile
    {
        public AutoMappingProfileDto()
        {
            CreateMap<AssetDto, Asset>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.TypeName, o => o.MapFrom(s => s.TypeName))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Value))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.FinanceId));
        }
    }
}
