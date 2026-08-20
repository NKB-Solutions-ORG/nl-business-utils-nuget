using System.Collections.Generic;

namespace NLBusinessUtils
{
    /// <summary>
    /// Looks up the bank behind a Dutch IBAN.
    /// </summary>
    public static class DutchBank
    {
        /// <summary>
        /// Known Dutch bank identifier codes (IBAN positions 5-8) mapped to
        /// their official bank name. This is a curated list of major banks,
        /// not an exhaustive registry - <see cref="GetName"/> returns
        /// <c>null</c> for any code not in this list rather than guessing.
        /// </summary>
        private static readonly Dictionary<string, string> Banks = new Dictionary<string, string>
        {
            ["ABNA"] = "ABN AMRO Bank N.V.",
            ["INGB"] = "ING Bank N.V.",
            ["RABO"] = "Rabobank",
            ["SNSB"] = "SNS Bank N.V.",
            ["ASNB"] = "ASN Bank N.V.",
            ["TRIO"] = "Triodos Bank N.V.",
            ["KNAB"] = "Knab (Aegon Bank N.V.)",
            ["BUNQ"] = "bunq B.V.",
            ["RBRB"] = "RegioBank N.V.",
            ["ARBN"] = "Achmea Bank N.V.",
        };

        /// <summary>
        /// Looks up the bank name for a valid Dutch IBAN, based on its
        /// 4-letter bank identifier code. Returns <c>null</c> if the IBAN
        /// is invalid or belongs to a bank that is not in the curated list
        /// of major Dutch banks.
        /// </summary>
        public static string? GetName(string iban)
        {
            var normalized = iban.Trim().ToUpperInvariant().Replace(" ", "");

            if (!Iban.IsValidDutch(normalized))
            {
                return null;
            }

            var bankCode = normalized.Substring(4, 4);
            return Banks.TryGetValue(bankCode, out var name) ? name : null;
        }
    }
}
