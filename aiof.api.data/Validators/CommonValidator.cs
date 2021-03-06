using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace aiof.api.data
{
    public static class CommonValidator
    {
        public static string RegexPhoneNumber = @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$";
        public static string RegexZipCode = @"^\d{5}(?:[-\s]\d{4})?$";
        public static string States = "|AL|AK|AS|AZ|AR|CA|CO|CT|DE|DC|FM|FL|GA|GU|HI|ID|IL|IN|IA|KS|KY|LA|ME|MH|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|MP|OH|OK|OR|PW|PA|PR|RI|SC|SD|TN|TX|UT|VT|VI|VA|WA|WV|WI|WY|";

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

        public static string UserRelationshipsMessage = $"User relationship must be one of the following {string.Join(", ", Constants.UserRelationships)}";
        public static string GoalTypesMessage = $"Invalid Type. Allowed values are {string.Join(", ", Constants.GoalTypes)}";
        public static string GoalTripTypesMessage = $"Invalid Type. Allowed values are {string.Join(", ", Constants.GoalTripTypes)}";
        public static string GoalCollegeTypesMessage = $"Invalid Type. Allowed values are {string.Join(", ", Constants.GoalCollegeTypes)}";

        public static bool IsValidPhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
                
            return new Regex(RegexPhoneNumber).IsMatch(value);
        }

        public static bool IsValidState(string state)
        {
            if (string.IsNullOrWhiteSpace(state))
                return false;

            return state.Length == 2 
                && States.IndexOf(state.ToUpperInvariant()) > 0;
        }

        public static bool IsValidZipCode(string zipCode)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
                return false;

            return new Regex(RegexZipCode).IsMatch(zipCode);
        }
    }
}