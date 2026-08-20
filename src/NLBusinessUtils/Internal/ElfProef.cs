using System.Linq;
using System.Text.RegularExpressions;

namespace NLBusinessUtils.Internal
{
    /// <summary>
    /// Generic Dutch "elfproef" (eleven-test) checksum used by BSN and RSIN.
    /// Weights 9..2 apply to the first eight digits, weight -1 applies to the last digit.
    /// A number passes when the weighted sum is a non-zero multiple of 11.
    /// </summary>
    internal static class ElfProef
    {
        private static readonly Regex NineDigits = new Regex("^\\d{9}$", RegexOptions.Compiled);
        private static readonly int[] Weights = { 9, 8, 7, 6, 5, 4, 3, 2, -1 };

        public static bool Passes(string nineDigits)
        {
            if (!NineDigits.IsMatch(nineDigits))
            {
                return false;
            }

            var sum = nineDigits
                .Select((c, i) => (c - '0') * Weights[i])
                .Sum();

            return sum != 0 && sum % 11 == 0;
        }
    }
}
