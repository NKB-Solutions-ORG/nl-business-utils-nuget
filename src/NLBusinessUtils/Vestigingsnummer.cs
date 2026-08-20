using System.Text.RegularExpressions;

namespace NLBusinessUtils
{
    /// <summary>
    /// Validation for the Dutch vestigingsnummer (KVK branch/location number).
    /// </summary>
    public static class Vestigingsnummer
    {
        private static readonly Regex TwelveDigits = new Regex("^\\d{12}$", RegexOptions.Compiled);

        /// <summary>
        /// Validates the format of a Dutch vestigingsnummer: exactly 12
        /// digits. Like the KVK-nummer itself, no checksum algorithm is
        /// published, so this is a format check only.
        /// </summary>
        public static bool IsValid(string value) => TwelveDigits.IsMatch(value.Trim());
    }
}
