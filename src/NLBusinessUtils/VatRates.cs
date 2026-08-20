namespace NLBusinessUtils
{
    /// <summary>
    /// Current Dutch VAT (btw) rate percentages.
    /// </summary>
    public static class VatRates
    {
        /// <summary>Algemeen tarief.</summary>
        public const decimal Standard = 21m;

        /// <summary>Verlaagd tarief.</summary>
        public const decimal Reduced = 9m;

        /// <summary>Nultarief.</summary>
        public const decimal Zero = 0m;
    }
}
