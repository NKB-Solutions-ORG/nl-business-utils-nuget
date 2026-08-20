using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace NLBusinessUtils
{
    /// <summary>
    /// Formatting and parsing of Dutch-style euro amounts.
    /// </summary>
    public static class Currency
    {
        private static string InsertThousandSeparators(long value)
        {
            var digits = value.ToString(CultureInfo.InvariantCulture);
            var result = new StringBuilder();

            for (var i = 0; i < digits.Length; i++)
            {
                if (i > 0 && (digits.Length - i) % 3 == 0)
                {
                    result.Append('.');
                }

                result.Append(digits[i]);
            }

            return result.ToString();
        }

        /// <summary>
        /// Formats an amount as a Dutch-style euro string, e.g. <c>1234.5m</c>
        /// becomes <c>"€ 1.234,50"</c> and <c>-19.99m</c> becomes
        /// <c>"-€ 19,99"</c>.
        ///
        /// This is implemented manually rather than via culture-aware
        /// formatting so the output (separators, spacing) is stable
        /// regardless of the host's globalization configuration.
        /// </summary>
        public static string FormatEuro(decimal amount)
        {
            var negative = amount < 0;
            var cents = (long)Math.Round(Math.Abs(amount) * 100m, 0, MidpointRounding.AwayFromZero);
            var whole = cents / 100;
            var centsPart = cents % 100;

            return $"{(negative ? "-" : "")}€ {InsertThousandSeparators(whole)},{centsPart:D2}";
        }

        /// <summary>
        /// Parses a Dutch-style euro string (as produced by
        /// <see cref="FormatEuro"/>, with or without the <c>€</c> sign)
        /// back into a decimal. Throws <see cref="ArgumentException"/> if
        /// the input isn't a recognizable Dutch amount.
        /// </summary>
        public static decimal ParseEuroAmount(string value)
        {
            var trimmed = value.Trim();
            var negative = trimmed.Contains("-");
            var cleaned = Regex.Replace(trimmed, "[€\\s-]", "").Replace(".", "").Replace(",", ".");

            if (!Regex.IsMatch(cleaned, "^\\d+(\\.\\d{1,2})?$"))
            {
                throw new ArgumentException($"Invalid euro amount: {value}", nameof(value));
            }

            var amount = decimal.Parse(cleaned, CultureInfo.InvariantCulture);
            return negative ? -amount : amount;
        }
    }
}
