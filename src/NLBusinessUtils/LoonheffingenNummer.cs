using System;
using System.Text.RegularExpressions;
using NLBusinessUtils.Internal;

namespace NLBusinessUtils
{
    /// <summary>
    /// Validation for the Dutch loonheffingennummer (payroll tax number).
    /// </summary>
    public static class LoonheffingenNummer
    {
        private static readonly Regex Pattern = new Regex("^(\\d{9})L(\\d{2})$", RegexOptions.Compiled);

        private static string Normalize(string value) =>
            Regex.Replace(value.Trim().ToUpperInvariant(), "[\\s.-]", "");

        /// <summary>
        /// Validates a Dutch loonheffingennummer: the holder's BSN or RSIN
        /// (9 digits, must pass the elfproef) followed by <c>L</c> and a
        /// 2-digit sequence number (e.g. <c>123456782L01</c>).
        /// </summary>
        public static bool IsValid(string value)
        {
            var match = Pattern.Match(Normalize(value));

            if (!match.Success)
            {
                return false;
            }

            var sequence = int.Parse(match.Groups[2].Value);
            return sequence is >= 1 and <= 99 && ElfProef.Passes(match.Groups[1].Value);
        }

        /// <summary>
        /// Returns the canonical uppercase representation of a
        /// loonheffingennummer. Throws <see cref="ArgumentException"/> if
        /// the input does not pass <see cref="IsValid"/>.
        /// </summary>
        public static string Format(string value)
        {
            var normalized = Normalize(value);

            if (!IsValid(normalized))
            {
                throw new ArgumentException($"Invalid loonheffingennummer: {value}", nameof(value));
            }

            return normalized;
        }
    }
}
