using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class UserProfile :
        IPublicKeyId
    {
        [JsonIgnore]
        [Required]
        public int Id { get; set; }

        [JsonIgnore]
        [Required]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        [JsonIgnore]
        [Required]
        public int UserId { get; set; }

        [JsonIgnore]
        [Required]
        public User User { get; set; }

        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }
        public string OccupationIndustry { get; set; }
        public decimal GrossSalary { get; set; }
        public string MaritalStatus { get; set; }
        public string EducationLevel { get; set; }
        public string ResidentialStatus { get; set; }

        public decimal HouseholdIncome { get; set; }
        public int HouseholdAdults { get; set; }
        public int HouseholdChildren { get; set; }

        public decimal RetirementContributionsPreTax { get; set; }
    }
}