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
                .ForMember(x => x.FinanceId, o => o.MapFrom(s => s.FinanceId));

            CreateMap<LiabilityDto, Liability>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Name))
                .ForMember(x => x.TypeName, o => o.MapFrom(s => s.TypeName))
                .ForMember(x => x.Value, o => o.MapFrom(s => s.Value))
                .ForMember(x => x.FinanceId, o => o.MapFrom(s => s.FinanceId));

            CreateMap<FinanceDto, Finance>()
                .ForMember(x => x.UserId, o => o.MapFrom(s => s.UserId))
                .ForMember(x => x.Assets, o => o.MapFrom(s => s.AssetDtos))
                .ForMember(x => x.Liabilities, o => o.MapFrom(s => s.LiabilityDtos))
                .ForMember(x => x.Goals, o => o.MapFrom(s => s.GoalDtos));
        }
    }
}
