using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface IUserProfile
    {
        [JsonIgnore]
        [Required]
        int Id { get; set; }

        [JsonIgnore]
        [Required]
        Guid PublicKey { get; set; }

        [JsonIgnore]
        [Required]
        int UserId { get; set; }

        [JsonIgnore]
        [Required]
        User User { get; set; }

        string Gender { get; set; }
        DateTime? DateOfBirth { get; set; }
        int? Age { get; set; }
        string Occupation { get; set; }
        string OccupationIndustry { get; set; }
        decimal? GrossSalary { get; set; }
        string MaritalStatus { get; set; }
        string EducationLevel { get; set; }
        string ResidentialStatus { get; set; }

        decimal? HouseholdIncome { get; set; }
        int? HouseholdAdults { get; set; }
        int? HouseholdChildren { get; set; }

        decimal? RetirementContributionsPreTax { get; set; }

        Address PhysicalAddress { get; set; }
    }
}