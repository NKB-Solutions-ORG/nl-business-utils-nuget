using System;
using System.Text.RegularExpressions;
using NLBusinessUtils.Internal;

namespace NLBusinessUtils
{
    /// <summary>
    /// Validation for the Dutch btw-identificatienummer (VAT number).
    /// </summary>
    public static class VatNumber
    {
        private static readonly Regex Pattern = new Regex("^NL(\\d{9})B(\\d{2})$", RegexOptions.Compiled);

        private static string Normalize(string value) =>
            Regex.Replace(value.Trim().ToUpperInvariant(), "[\\s.-]", "");

        /// <summary>
        /// Validates the format of a Dutch btw-identificatienummer (VAT
        /// number): <c>NL</c> + 9 digits + <c>B</c> + a 2-digit branch
        /// number from 01 to 99.
        ///
        /// This checks structure only. Use <see cref="HasValidLegacyChecksum"/>
        /// for an additional checksum check on pre-2020 numbers - see that
        /// method's documentation for an important limitation.
        /// </summary>
        public static bool IsValidFormat(string value)
        {
            var match = Pattern.Match(Normalize(value));

            if (!match.Success)
            {
                return false;
            }

            var branch = int.Parse(match.Groups[2].Value);
            return branch is >= 1 and <= 99;
        }

        /// <summary>
        /// Checks the legacy elfproef checksum on the 9-digit segment of a
        /// Dutch VAT number. This only applies to <em>pre-2020</em> VAT
        /// numbers, which were derived from the holder's BSN/RSIN and
        /// therefore satisfy the elfproef.
        ///
        /// Since 1 January 2020, VAT numbers issued to natural persons (sole
        /// traders / zzp'ers) use a new, privacy-preserving numbering scheme
        /// that is <em>expected</em> to fail this checksum by design - the
        /// Belastingdienst has not published the algorithm used to generate
        /// those numbers. A <c>false</c> result from this method therefore
        /// does <strong>not</strong> prove the number is invalid.
        ///
        /// For a definitive check, validate the format with
        /// <see cref="IsValidFormat"/> and, if certainty is required, verify
        /// the number against the EU VIES service or the Dutch tax authority.
        /// </summary>
        public static bool HasValidLegacyChecksum(string value)
        {
            var match = Pattern.Match(Normalize(value));
            return match.Success && ElfProef.Passes(match.Groups[1].Value);
        }

        /// <summary>
        /// Returns the canonical <c>NLxxxxxxxxxBxx</c> representation of a
        /// VAT number. Throws <see cref="ArgumentException"/> if the input
        /// does not pass <see cref="IsValidFormat"/>.
        /// </summary>
        public static string Format(string value)
        {
            var normalized = Normalize(value);

            if (!IsValidFormat(normalized))
            {
                throw new ArgumentException($"Invalid VAT number: {value}", nameof(value));
            }

            return normalized;
        }
    }
}
