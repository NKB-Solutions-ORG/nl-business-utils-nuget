using NLBusinessUtils;

namespace NLBusinessUtils.Tests;

public class VatCalculatorTests
{
    [Fact]
    public void VatRates_ExposesTheCurrentDutchRates()
    {
        Assert.Equal(21m, VatRates.Standard);
        Assert.Equal(9m, VatRates.Reduced);
        Assert.Equal(0m, VatRates.Zero);
    }

    [Fact]
    public void CalculateVatAmount_CalculatesTheStandardRate() =>
        Assert.Equal(21m, VatCalculator.CalculateVatAmount(100m, VatRates.Standard));

    [Fact]
    public void CalculateVatAmount_RoundsToWholeCents() =>
        Assert.Equal(4.20m, VatCalculator.CalculateVatAmount(19.99m, VatRates.Standard));

    [Fact]
    public void CalculateVatAmount_ReturnsZeroForTheZeroRate() =>
        Assert.Equal(0m, VatCalculator.CalculateVatAmount(100m, VatRates.Zero));

    [Fact]
    public void AddVat_AddsTheStandardRate() => Assert.Equal(121m, VatCalculator.AddVat(100m, VatRates.Standard));

    [Fact]
    public void AddVat_AddsTheReducedRate() => Assert.Equal(109m, VatCalculator.AddVat(100m, VatRates.Reduced));

    [Fact]
    public void RemoveVat_IsTheInverseOfAddVatForRoundAmounts() =>
        Assert.Equal(100m, VatCalculator.RemoveVat(121m, VatRates.Standard));

    [Fact]
    public void RemoveVat_RoundsToWholeCents() =>
        Assert.Equal(19.99m, VatCalculator.RemoveVat(24.19m, VatRates.Standard));
}
