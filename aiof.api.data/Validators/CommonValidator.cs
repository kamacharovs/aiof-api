using System;

namespace aiof.api.data
{
    public static class CommonValidator
    {
        public static string PhoneNumber = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";

        public const decimal MinimumValue = 1M;
        public const decimal MaximumValue = 99999999M;
        public const int MinimumValueInt = 0;
        public const int MaximumValueInt = 99999999;

        public static string ValueMessage = $"Value must be between {MinimumValue} and {MaximumValue}";
    }
}