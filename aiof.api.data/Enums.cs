using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        Generic,
        Trip,
        BuyAHome,
        BuyACar
    }
    public enum GoalTripType
    {
        Romance,
        Adventure,
        Beach,
        Family,
        Golf,
        Luxury,
        NationalParks,
        Spa,
        Other
    }
}
