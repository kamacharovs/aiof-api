using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using AutoMapper;

namespace aiof.api.data
{
    public class AutoMappingProfileDto : Profile
    {
        public AutoMappingProfileDto()
        {
            CreateMap<User, User>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.Assets, o => o.MapFrom((s, x) => s.Assets.Union(x.Assets).ToList()))
                .ForMember(x => x.Goals, o => o.MapFrom((s, x) => s.Goals.Union(x.Goals).ToList()))
                .ForMember(x => x.Liabilities, o => o.MapFrom((s, x) => s.Liabilities.Union(x.Liabilities).ToList()))
                .ForMember(x => x.Subscriptions, o => o.MapFrom((s, x) => s.Subscriptions.Union(x.Subscriptions).ToList()))
                .ForMember(x => x.Accounts, o => o.MapFrom((s, x) => s.Accounts.Union(x.Accounts).ToList()))
                .ForAllOtherMembers(x => x.Condition((source, destination, member) => member != null));
            CreateMap<UserDto, User>()
                .ForAllMembers(x => x.Condition((source, destination, member) => member != null));

            CreateMap<UserDependentDto, UserDependent>()
                .ForMember(x => x.Age, o => o.MapFrom(s => s.Age))
                .ForAllOtherMembers(x => x.Condition((source, destination, member) => member != null));

            CreateMap<UserProfileDto, UserProfile>()
                .ForAllMembers(x => x.Condition((source, destination, member) => member != null));

            CreateMap<AssetDto, Asset>()
                .ForMember(x => x.Name, o => o.Condition(s => s.Name != null))
                .ForMember(x => x.TypeName, o => o.Condition(s => s.TypeName != null))
                .ForMember(x => x.Value, o => o.Condition(s => s.Value != null));

            CreateMap<GoalDto, Goal>()
                .ForMember(x => x.Name, o => o.Condition(s => s.Name != null))
                .ForMember(x => x.TypeName, o => o.Condition(s => s.TypeName != null))
                .ForMember(x => x.Amount, o => o.Condition(s => s.Amount != null))
                .ForMember(x => x.CurrentAmount, o => o.Condition(s => s.CurrentAmount != null))
                .ForMember(x => x.Contribution, o => o.Condition(s => s.Contribution != null))
                .ForMember(x => x.ContributionFrequencyName, o => o.Condition(s => s.ContributionFrequencyName != null))
                .ForMember(x => x.PlannedDate, o => o.Condition(s => s.PlannedDate != null));

            CreateMap<LiabilityDto, Liability>()
                .ForMember(x => x.Name, o => o.Condition(s => s.Name != null))
                .ForMember(x => x.TypeName, o => o.Condition(s => s.TypeName != null))
                .ForMember(x => x.Value, o => o.Condition(s => s.Value != null))
                .ForMember(x => x.MonthlyPayment, o => o.Condition(s => s.MonthlyPayment != null))
                .ForMember(x => x.Years, o => o.Condition(s => s.Years != null));

            CreateMap<Subscription, Subscription>()
                .ForAllMembers(x => x.Condition((source, destination, member) => member != null));
            CreateMap<SubscriptionDto, Subscription>()
                .ForMember(x => x.Amount, o => o.Condition(s => s.Amount != null || s.Amount == 0))
                .ForMember(x => x.PaymentLength, o => o.Condition(s => s.PaymentLength != null || s.PaymentLength == 0))
                .ForAllOtherMembers(x => x.Condition((source, destination, member) => member != null));

            CreateMap<AccountDto, Account>()
                .ForAllMembers(x => x.Condition((source, destination, member) => member != null));
        }
    }
}
