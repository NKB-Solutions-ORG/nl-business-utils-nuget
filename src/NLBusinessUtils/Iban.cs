using System;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace NLBusinessUtils
{
    /// <summary>
    /// Validation and formatting for Dutch IBANs.
    /// </summary>
    public static class Iban
    {
        private static readonly Regex Pattern = new Regex("^NL\\d{2}[A-Z]{4}\\d{10}$", RegexOptions.Compiled);

        private static string Normalize(string value) =>
            Regex.Replace(value.Trim().ToUpperInvariant(), "\\s", "");

        /// <summary>
        /// ISO 7064 MOD 97-10 check as used by IBAN (ISO 13616): move the
        /// first four characters to the end, expand letters to two-digit
        /// numbers (A=10..Z=35), then the resulting numeral must be
        /// congruent to 1 mod 97.
        /// </summary>
        private static bool Mod97Check(string iban)
        {
            var rearranged = iban.Substring(4) + iban.Substring(0, 4);

            var numeric = new StringBuilder();
            foreach (var c in rearranged)
            {
                if (c is >= 'A' and <= 'Z')
                {
                    numeric.Append(c - 'A' + 10);
                }
                else
                {
                    numeric.Append(c);
                }
            }

            var value = BigInteger.Parse(numeric.ToString());
            return value % 97 == 1;
        }

        /// <summary>
        /// Validates a Dutch IBAN: correct structure (<c>NL</c> + 2 check
        /// digits + 4 letter bank code + 10 digit account number) and a
        /// passing MOD-97 checksum.
        /// </summary>
        public static bool IsValidDutch(string value)
        {
            var normalized = Normalize(value);
            return Pattern.IsMatch(normalized) && Mod97Check(normalized);
        }

        /// <summary>
        /// Returns a Dutch IBAN formatted in groups of four characters
        /// (e.g. <c>NL91 ABNA 0417 1643 00</c>). Throws
        /// <see cref="ArgumentException"/> if the input does not pass
        /// <see cref="IsValidDutch"/>.
        /// </summary>
        public static string Format(string value)
        {
            var normalized = Normalize(value);

            if (!IsValidDutch(normalized))
            {
                throw new ArgumentException($"Invalid Dutch IBAN: {value}", nameof(value));
            }

            return Regex.Replace(normalized, "(.{4})(?=.)", "$1 ");
        }
    }
}
