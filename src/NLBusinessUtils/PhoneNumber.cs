using System;
using System.Text.RegularExpressions;

namespace NLBusinessUtils
{
    /// <summary>
    /// Validation and formatting for Dutch phone numbers.
    /// </summary>
    public static class PhoneNumber
    {
        private static readonly Regex NationalFormat = new Regex("^0\\d{9}$", RegexOptions.Compiled);

        private static string? ToNationalDigits(string value)
        {
            var cleaned = Regex.Replace(value.Trim(), "[\\s().-]", "");

            if (cleaned.StartsWith("+31", StringComparison.Ordinal))
            {
                cleaned = "0" + cleaned.Substring(3);
            }
            else if (cleaned.StartsWith("0031", StringComparison.Ordinal))
            {
                cleaned = "0" + cleaned.Substring(4);
            }

            return NationalFormat.IsMatch(cleaned) ? cleaned : null;
        }

        /// <summary>
        /// Validates a Dutch phone number (mobile or landline): 10 digits in
        /// national format (<c>0...</c>), or the equivalent with a
        /// <c>+31</c> / <c>0031</c> country-calling-code prefix. This checks
        /// structure only - it does not verify the number against an
        /// area-code database or confirm the line is in service.
        /// </summary>
        public static bool IsValidDutch(string value) => ToNationalDigits(value) != null;

        /// <summary>
        /// Returns whether a valid Dutch phone number is a mobile number
        /// (national format <c>06XXXXXXXX</c>).
        /// </summary>
        public static bool IsDutchMobile(string value)
        {
            var digits = ToNationalDigits(value);
            return digits != null && digits.StartsWith("06", StringComparison.Ordinal);
        }

        /// <summary>
        /// Returns a Dutch phone number formatted in E.164 (<c>+31...</c>).
        /// Throws <see cref="ArgumentException"/> if the input does not pass
        /// <see cref="IsValidDutch"/>.
        /// </summary>
        public static string FormatDutch(string value)
        {
            var digits = ToNationalDigits(value);

            if (digits == null)
            {
                throw new ArgumentException($"Invalid Dutch phone number: {value}", nameof(value));
            }

            return "+31" + digits.Substring(1);
        }
    }
}
