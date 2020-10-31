﻿using System;
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

            CreateMap<UserProfileDto, UserProfile>()
                .ForAllMembers(x => x.Condition((source, destination, member) => member != null));

            CreateMap<AssetDto, Asset>()
                .ForMember(x => x.Name, o => o.Condition(s => s.Name != null))
                .ForMember(x => x.TypeName, o => o.Condition(s => s.TypeName != null))
                .ForMember(x => x.Value, o => o.Condition(s => s.Value != null));

            CreateMap<GoalDto, Goal>()
                .ForMember(x => x.Name, o => o.Condition(s => s.Name != null))
                .ForMember(x => x.TypeName, o => o.Condition(s => s.TypeName != null))
                .ForMember(x => x.Amount, o => o.MapFrom(s => s.Amount))
                .ForMember(x => x.CurrentAmount, o => o.MapFrom(s => s.CurrentAmount))
                .ForMember(x => x.Contribution, o => o.MapFrom(s => s.Contribution))
                .ForMember(x => x.ContributionFrequencyName, o => o.MapFrom(s => s.ContributionFrequencyName.ToLowerInvariant()))
                .ForMember(x => x.PlannedDate, o => o.Condition(s => s.PlannedDate != null))
                .ForMember(x => x.UserId, o => o.Condition(s => s.UserId != null));

            CreateMap<LiabilityDto, Liability>()
                .ForMember(x => x.Name, o => o.Condition(s => s.Name != null))
                .ForMember(x => x.TypeName, o => o.Condition(s => s.TypeName != null))
                .ForMember(x => x.Value, o => o.Condition(s => s.Value != null))
                .ForMember(x => x.UserId, o => o.Condition(s => s.UserId != null));

            CreateMap<Subscription, Subscription>()
                .ForAllMembers(x => x.Condition((source, destination, member) => member != null));
            CreateMap<SubscriptionDto, Subscription>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Name))
                .ForMember(x => x.Description, o => o.Condition(s => s.Description != null))
                .ForMember(x => x.Amount, o => o.MapFrom(s => s.Amount))
                .ForMember(x => x.PaymentFrequencyName, o => o.MapFrom(s => s.PaymentFrequencyName))
                .ForMember(x => x.PaymentLength, o => o.MapFrom(s => s.PaymentLength))
                .ForMember(x => x.From, o => o.Condition(s => s.From != null))
                .ForMember(x => x.Url, o => o.Condition(s => s.Url != null))
                .ForMember(x => x.UserId, o => o.MapFrom(s => s.UserId));

            CreateMap<AccountDto, Account>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Name))
                .ForMember(x => x.Description, o => o.MapFrom(s => s.Description))
                .ForMember(x => x.TypeName, o => o.MapFrom(s => s.TypeName))
                .ForMember(x => x.UserId, o => o.MapFrom(s => s.UserId));
        }
    }
}
