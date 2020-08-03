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
                .ForMember(x => x.Name, o => o.Condition(s => s.Name != null))
                .ForMember(x => x.TypeName, o => o.Condition(s => s.TypeName != null))
                .ForMember(x => x.Value, o => o.Condition(s => s.Value != null))
                .ForMember(x => x.FinanceId, o => o.Condition(s => s.FinanceId != null));

            CreateMap<GoalDto, Goal>()
                .ForMember(x => x.Name, o => o.Condition(s => s.Name != null))
                .ForMember(x => x.TypeName, o => o.Condition(s => s.TypeName != null))
                .ForMember(x => x.Savings, o => o.Condition(s => s.Savings != null))
                .ForMember(x => x.UserId, o => o.Condition(s => s.UserId != null));

            CreateMap<LiabilityDto, Liability>()
                .ForMember(x => x.Name, o => o.Condition(s => s.Name != null))
                .ForMember(x => x.TypeName, o => o.Condition(s => s.TypeName != null))
                .ForMember(x => x.Value, o => o.Condition(s => s.Value != null))
                .ForMember(x => x.FinanceId, o => o.Condition(s => s.FinanceId != null));

            CreateMap<FinanceDto, Finance>()
                .ForMember(x => x.UserId, o => o.MapFrom(s => s.UserId))
                .ForMember(x => x.Assets, o => o.Condition(s => s.AssetDtos != null))
                .ForMember(x => x.Liabilities, o => o.Condition(s => s.LiabilityDtos != null));

            CreateMap<UserDto, User>()
                .ForMember(x => x.FirstName, o => o.Condition(s => s.FirstName != null))
                .ForMember(x => x.LastName, o => o.Condition(s => s.LastName != null))
                .ForMember(x => x.Email, o => o.Condition(s => s.Email != null))
                .ForMember(x => x.Username, o => o.Condition(s => s.Username != null));
        }
    }
}
