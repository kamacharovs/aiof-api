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
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Name))
                .ForMember(x => x.TypeName, o => o.MapFrom(s => s.TypeName))
                .ForMember(x => x.Value, o => o.MapFrom(s => s.Value))
                .ForMember(x => x.FinanceId, o => o.MapFrom(s => s.FinanceId));

            CreateMap<GoalDto, Goal>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Name))
                .ForMember(x => x.TypeName, o => o.MapFrom(s => s.TypeName))
                .ForMember(x => x.Savings, o => o.MapFrom(s => s.Savings))
                .ForMember(x => x.Name, o => o.MapFrom(s => s.FinanceId));
        }
    }
}
