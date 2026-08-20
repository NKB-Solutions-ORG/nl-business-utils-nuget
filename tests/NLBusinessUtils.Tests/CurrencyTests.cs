using NLBusinessUtils;

namespace NLBusinessUtils.Tests;

public class CurrencyTests
{
    [Fact]
    public void FormatEuro_FormatsAPlainAmount() => Assert.Equal("€ 1.234,56", Currency.FormatEuro(1234.56m));

    [Fact]
    public void FormatEuro_FormatsZero() => Assert.Equal("€ 0,00", Currency.FormatEuro(0m));

    [Fact]
    public void FormatEuro_FormatsNegativeAmounts() => Assert.Equal("-€ 19,99", Currency.FormatEuro(-19.99m));

    [Fact]
    public void FormatEuro_InsertsMultipleThousandSeparators() =>
        Assert.Equal("€ 1.000.000,00", Currency.FormatEuro(1000000m));

    [Fact]
    public void FormatEuro_PadsAWholeAmountToTwoDecimals() => Assert.Equal("€ 5,00", Currency.FormatEuro(5m));

    [Fact]
    public void FormatEuro_RoundsToWholeCents() => Assert.Equal("€ 20,00", Currency.FormatEuro(19.995m));

    [Fact]
    public void ParseEuroAmount_ParsesAFormattedEuroString() =>
        Assert.Equal(1234.56m, Currency.ParseEuroAmount("€ 1.234,56"));

    [Fact]
    public void ParseEuroAmount_ParsesANegativeAmount() => Assert.Equal(-19.99m, Currency.ParseEuroAmount("-€ 19,99"));

    [Fact]
    public void ParseEuroAmount_ParsesInputWithoutTheEuroSignOrThousandSeparators() =>
        Assert.Equal(1234.56m, Currency.ParseEuroAmount("1234,56"));

    [Fact]
    public void ParseEuroAmount_RoundTripsWithFormatEuro() =>
        Assert.Equal(987654.32m, Currency.ParseEuroAmount(Currency.FormatEuro(987654.32m)));

    [Fact]
    public void ParseEuroAmount_ThrowsForUnparseableInput() =>
        Assert.Throws<ArgumentException>(() => Currency.ParseEuroAmount("not an amount"));
}
