using System;
using System.Text.RegularExpressions;

namespace aiof.api.data
{
    public static class CommonValidator
    {
        public static string RegexPhoneNumber = @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$";

        public const decimal MinimumValue = 1M;
        public const decimal MaximumValue = 99999999M;
        public const int MinimumValueInt = 0;
        public const int MaximumValueInt = 99999999;

        public static string ValueMessage = $"Value must be between {MinimumValue} and {MaximumValue}";

        public static bool IsValidPhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
                
            return new Regex(RegexPhoneNumber).IsMatch(value);
        }
    }
}