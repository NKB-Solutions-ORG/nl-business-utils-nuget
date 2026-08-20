using System;
using System.Text.RegularExpressions;
using NLBusinessUtils.Internal;

namespace NLBusinessUtils
{
    /// <summary>
    /// Validation for the Dutch BSN (burgerservicenummer).
    /// </summary>
    public static class Bsn
    {
        private static readonly Regex EightOrNineDigits = new Regex("^\\d{8,9}$", RegexOptions.Compiled);

        private static string? ToNineDigits(string value)
        {
            var trimmed = value.Trim();

            if (!EightOrNineDigits.IsMatch(trimmed))
            {
                return null;
            }

            return trimmed.Length == 8 ? "0" + trimmed : trimmed;
        }

        /// <summary>
        /// Validates a Dutch BSN using the elfproef checksum. Accepts 8 or 9
        /// digit input (8-digit numbers are zero-padded before checking).
        /// </summary>
        public static bool IsValid(string value)
        {
            var nineDigits = ToNineDigits(value);
            return nineDigits != null && ElfProef.Passes(nineDigits);
        }

        /// <summary>
        /// Returns the canonical 9-digit representation of a valid BSN.
        /// Throws <see cref="ArgumentException"/> if the input does not pass <see cref="IsValid"/>.
        /// </summary>
        public static string Normalize(string value)
        {
            var nineDigits = ToNineDigits(value);

            if (nineDigits == null || !ElfProef.Passes(nineDigits))
            {
                throw new ArgumentException($"Invalid BSN: {value}", nameof(value));
            }

            return nineDigits;
        }
    }
}
