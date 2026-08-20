using System.Text.RegularExpressions;

namespace NLBusinessUtils
{
    /// <summary>
    /// Validation for the Dutch KVK-nummer (Chamber of Commerce number).
    /// </summary>
    public static class KvkNumber
    {
        private static readonly Regex EightDigits = new Regex("^\\d{8}$", RegexOptions.Compiled);

        /// <summary>
        /// Validates the format of a Dutch KVK-nummer: exactly 8 digits. The
        /// KVK does not publish a checksum algorithm, so this is a format
        /// check only - it cannot confirm the number is actually registered.
        /// Use the KVK API for definitive verification.
        /// </summary>
        public static bool IsValid(string value) => EightDigits.IsMatch(value.Trim());
    }
}
