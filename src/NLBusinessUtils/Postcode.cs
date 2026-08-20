using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NLBusinessUtils
{
    /// <summary>
    /// Validation and formatting for Dutch postcodes.
    /// </summary>
    public static class Postcode
    {
        private static readonly Regex Pattern = new Regex("^([1-9]\\d{3})\\s*([A-Za-z]{2})$", RegexOptions.Compiled);

        // PostNL never issues these letter combinations, regardless of the digits.
        private static readonly HashSet<string> ReservedLetters = new HashSet<string> { "SS", "SA", "SD" };

        private static (string Digits, string Letters)? Parse(string value)
        {
            var match = Pattern.Match(value.Trim());

            if (!match.Success)
            {
                return null;
            }

            var letters = match.Groups[2].Value.ToUpperInvariant();

            if (ReservedLetters.Contains(letters))
            {
                return null;
            }

            return (match.Groups[1].Value, letters);
        }

        /// <summary>
        /// Validates a Dutch postcode: 4 digits (1000-9999) followed by 2
        /// letters, with optional whitespace between them. Rejects the
        /// letter combinations PostNL never issues (SS, SA, SD).
        /// </summary>
        public static bool IsValidDutch(string value) => Parse(value) != null;

        /// <summary>
        /// Returns a Dutch postcode formatted as <c>1234 AB</c> (digits,
        /// single space, uppercase letters). Throws
        /// <see cref="ArgumentException"/> if the input does not pass
        /// <see cref="IsValidDutch"/>.
        /// </summary>
        public static string FormatDutch(string value)
        {
            var parsed = Parse(value);

            if (parsed == null)
            {
                throw new ArgumentException($"Invalid Dutch postcode: {value}", nameof(value));
            }

            return $"{parsed.Value.Digits} {parsed.Value.Letters}";
        }
    }
}
