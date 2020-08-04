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
            CreateMap<UserDto, User>()
                .ForMember(x => x.Assets, o => o.MapFrom(s => s.Assets))
                .ForMember(x => x.Goals, o => o.MapFrom(s => s.Goals))
                .ForMember(x => x.Liabilities, o => o.MapFrom(s => s.Liabilities));

            CreateMap<AssetDto, Asset>()
                .ForMember(x => x.Name, o => o.Condition(s => s.Name != null))
                .ForMember(x => x.TypeName, o => o.Condition(s => s.TypeName != null))
                .ForMember(x => x.Value, o => o.Condition(s => s.Value != null))
                .ForMember(x => x.UserId, o => o.Condition(s => s.UserId != null));

            CreateMap<GoalDto, Goal>()
                .ForMember(x => x.Name, o => o.Condition(s => s.Name != null))
                .ForMember(x => x.TypeName, o => o.Condition(s => s.TypeName != null))
                .ForMember(x => x.Savings, o => o.Condition(s => s.Savings != null))
                .ForMember(x => x.UserId, o => o.Condition(s => s.UserId != null));

            CreateMap<LiabilityDto, Liability>()
                .ForMember(x => x.Name, o => o.Condition(s => s.Name != null))
                .ForMember(x => x.TypeName, o => o.Condition(s => s.TypeName != null))
                .ForMember(x => x.Value, o => o.Condition(s => s.Value != null))
                .ForMember(x => x.UserId, o => o.Condition(s => s.UserId != null));
        }
    }
}
