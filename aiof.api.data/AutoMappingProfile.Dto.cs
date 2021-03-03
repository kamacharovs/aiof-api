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

            CreateMap<UserDependent, UserDependent>()
                .ForAllMembers(x => x.Condition((source, destination, member) => member != null));
            CreateMap<UserDependentDto, UserDependent>()
                .ForMember(x => x.Age, o => o.Condition(s => s.Age != null))
                .ForMember(x => x.AmountOfSupportProvided, o => o.Condition(s => s.AmountOfSupportProvided != null))
                .ForAllOtherMembers(x => x.Condition((source, destination, member) => member != null));

            CreateMap<UserProfileDto, UserProfile>()
                .ForAllMembers(x => x.Condition((source, destination, member) => member != null));

            CreateMap<AssetDto, Asset>()
                .ForMember(x => x.Name, o => o.Condition(s => s.Name != null))
                .ForMember(x => x.TypeName, o => o.Condition(s => s.TypeName != null))
                .ForMember(x => x.Value, o => o.Condition(s => s.Value != null));

            CreateMap<GoalDto, Goal>()
                .ForMember(x => x.Name, o => o.Condition(s => s.Name != null))
                .ForMember(x => x.Type, o => o.MapFrom(s => s.Type))
                .ForMember(x => x.Amount, o => o.Condition(s => s.Amount != null))
                .ForMember(x => x.CurrentAmount, o => o.Condition(s => s.CurrentAmount != null))
                .ForMember(x => x.MonthlyContribution, o => o.Condition(s => s.MonthlyContribution != null))
                .ForMember(x => x.PlannedDate, o => o.MapFrom(s => s.PlannedDate))
                .ForMember(x => x.ProjectedDate, o => o.Condition(s => s.ProjectedDate != null));

            CreateMap<GoalTripDto, GoalTrip>()
                .ForMember(x => x.Name, o => o.Condition(s => s.Name != null))
                .ForMember(x => x.Type, o => o.MapFrom(s => s.Type))
                .ForMember(x => x.Amount, o => o.Condition(s => s.Amount != null))
                .ForMember(x => x.CurrentAmount, o => o.Condition(s => s.CurrentAmount != null))
                .ForMember(x => x.MonthlyContribution, o => o.Condition(s => s.MonthlyContribution != null))
                .ForMember(x => x.PlannedDate, o => o.MapFrom(s => s.PlannedDate))
                .ForMember(x => x.ProjectedDate, o => o.Condition(s => s.ProjectedDate != null))
                .ForMember(x => x.Destination, o => o.MapFrom(s => s.Destination))
                .ForMember(x => x.TripType, o => o.MapFrom(s => s.TripType))
                .ForMember(x => x.Duration, o => o.Condition(s => s.Duration != null))
                .ForMember(x => x.Travelers, o => o.Condition(s => s.Travelers != null))
                .ForMember(x => x.Flight, o => o.Condition(s => s.Flight != null))
                .ForMember(x => x.Hotel, o => o.Condition(s => s.Hotel != null))
                .ForMember(x => x.Car, o => o.Condition(s => s.Car != null))
                .ForMember(x => x.Food, o => o.Condition(s => s.Food != null))
                .ForMember(x => x.Activities, o => o.Condition(s => s.Activities != null))
                .ForMember(x => x.Other, o => o.Condition(s => s.Other != null));

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
