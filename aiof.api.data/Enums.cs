using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aiof.api.data
{
    public enum Genders
    {
        Male,
        Female,
        Other
    }

    public enum MaritalStatuses
    {
        Single,
        Married,
        Widowed,
        Divorced,
        Separated,
        RegisteredPartnership
    }

    public enum EducationLevels
    {
        None,
        HighSchool,
        Bachelors,
        Masters,
        Doctorate,
    }

    public enum ResidentialStatuses
    {
        Own,
        Rent,
        LiveWithParents
    }

    public enum UserRelationships
    {
        Child,
        Son,
        Daughter,
        Brother,
        Sister,
        Stepson,
        Stepdaughter,
        Stepbrother,
        Stepsister,
    }

    public enum AccountTypes
    {
        Retirement,
        Taxable,
    }

    public enum GoalType
    {
        [EnumMember(Value = "Generic")]
        Generic,

        [EnumMember(Value = "Trip")]
        Trip,

        [EnumMember(Value = "Buy a home")]
        BuyAHome,

        [EnumMember(Value = "Buy a car")]
        BuyACar
    }
    public enum GoalTripType
    {
        [EnumMember(Value = "Romance")]
        Romance,

        [EnumMember(Value = "Adventure")]
        Adventure,

        [EnumMember(Value = "Beach")]
        Beach,

        [EnumMember(Value = "Family")]
        Family,

        [EnumMember(Value = "Golf")]
        Golf,

        [EnumMember(Value = "Luxury")]
        Luxury,

        [EnumMember(Value = "National parks")]
        NationalParks,

        [EnumMember(Value = "Spa")]
        Spa,

        [EnumMember(Value = "Other")]
        Other
    }
}
