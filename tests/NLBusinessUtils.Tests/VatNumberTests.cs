using NLBusinessUtils;

namespace NLBusinessUtils.Tests;

public class VatNumberTests
{
    [Fact]
    public void IsValidFormat_AcceptsAWellFormedVatNumber() => Assert.True(VatNumber.IsValidFormat("NL123456782B01"));

    [Fact]
    public void IsValidFormat_AcceptsLowercaseAndLooselyFormattedInput() =>
        Assert.True(VatNumber.IsValidFormat("nl 123456782 b01"));

    [Fact]
    public void IsValidFormat_RejectsTheWrongCountryPrefix() => Assert.False(VatNumber.IsValidFormat("DE123456782B01"));

    [Fact]
    public void IsValidFormat_RejectsABranchNumberOfZero() => Assert.False(VatNumber.IsValidFormat("NL123456782B00"));

    [Theory]
    [InlineData("NL12345678B01")]
    [InlineData("NL123456782A01")]
    public void IsValidFormat_RejectsTheWrongOverallStructure(string value) => Assert.False(VatNumber.IsValidFormat(value));

    [Fact]
    public void HasValidLegacyChecksum_PassesForAPre2020BsnDerivedNumber() =>
        Assert.True(VatNumber.HasValidLegacyChecksum("NL123456782B01"));

    [Fact]
    public void HasValidLegacyChecksum_FailsWhenDigitsDoNotSatisfyTheElfproef()
    {
        // Format-valid, but the 9 digit segment fails the elfproef - this is
        // the expected shape of a post-2020 sole-trader VAT number.
        Assert.False(VatNumber.HasValidLegacyChecksum("NL123456789B01"));
    }

    [Fact]
    public void Format_ReturnsTheCanonicalUppercaseForm() => Assert.Equal("NL123456782B01", VatNumber.Format("nl123456782b01"));

    [Fact]
    public void Format_ThrowsForAnInvalidFormat() => Assert.Throws<ArgumentException>(() => VatNumber.Format("DE123456782B01"));
}
