using System;

namespace NLBusinessUtils
{
    /// <summary>
    /// Simple Dutch VAT (btw) calculations. All amounts are rounded to whole
    /// cents, consistent with how invoices are normally rounded.
    /// </summary>
    public static class VatCalculator
    {
        /// <summary>
        /// Calculates the VAT amount over an amount excluding VAT, at the
        /// given percentage.
        /// </summary>
        public static decimal CalculateVatAmount(decimal amountExcludingVat, decimal ratePercentage) =>
            Math.Round(amountExcludingVat * ratePercentage / 100m, 2, MidpointRounding.AwayFromZero);

        /// <summary>
        /// Adds VAT to an amount excluding VAT, returning the amount
        /// including VAT.
        /// </summary>
        public static decimal AddVat(decimal amountExcludingVat, decimal ratePercentage) =>
            amountExcludingVat + CalculateVatAmount(amountExcludingVat, ratePercentage);

        /// <summary>
        /// Removes VAT from an amount including VAT, returning the amount
        /// excluding VAT.
        /// </summary>
        public static decimal RemoveVat(decimal amountIncludingVat, decimal ratePercentage) =>
            Math.Round(amountIncludingVat / (1 + ratePercentage / 100m), 2, MidpointRounding.AwayFromZero);
    }
}
