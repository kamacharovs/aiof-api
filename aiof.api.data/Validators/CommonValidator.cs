using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace aiof.api.data
{
    public static class CommonValidator
    {
        public static string RegexPhoneNumber = @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$";

        public const decimal MinimumValue = 0M;
        public const decimal MaximumValue = 99999999M;
        public const decimal MinimumPercentageValue = 0M;
        public const decimal MaximumPercentageValue = 100M;
        public const int MinimumValueInt = 0;
        public const int MaximumValueInt = 99999999;
        public const int MinimumYears = 0;
        public const int MaximumYears = 130;

        public static string ValueMessage = $"Value must be between {MinimumValue} and {MaximumValue}";
        public static string PercentageMessage = $"Percentage value must be between {MinimumPercentageValue} and {MaximumPercentageValue}";
        public static string IntMessage = $"Value must be between {MinimumValueInt} and {MaximumValueInt}";
        public static string YearsMessage = $"Year value must be between {MinimumYears} and {MaximumYears}";

        public static IEnumerable<int> ValidCarLoanTerms = new List<int>
        {
            36,
            48,
            60,
            72
        };
        public static string ValidCarLoanTermsMessage = $"Value must be one of the following {string.Join(", ", ValidCarLoanTerms)}";

        public static string GoalTripTypesMessage = $"Invalid Type. Allowed values are {string.Join(", ", Constants.GoalTripTypes)}";
        public static string GoalCollegeTypesMessage = $"Invalid Type. Allowed values are {string.Join(", ", Constants.GoalCollegeTypes)}";

        public static bool IsValidPhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
                
            return new Regex(RegexPhoneNumber).IsMatch(value);
        }
    }
}