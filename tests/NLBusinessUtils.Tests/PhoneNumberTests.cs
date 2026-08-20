using NLBusinessUtils;

namespace NLBusinessUtils.Tests;

public class PhoneNumberTests
{
    [Fact]
    public void IsValidDutch_AcceptsANationalMobileNumber() => Assert.True(PhoneNumber.IsValidDutch("0612345678"));

    [Fact]
    public void IsValidDutch_AcceptsANationalLandlineNumber() => Assert.True(PhoneNumber.IsValidDutch("0201234567"));

    [Theory]
    [InlineData("06-12345678")]
    [InlineData("(020) 123 4567")]
    public void IsValidDutch_AcceptsCommonFormattingCharacters(string value) => Assert.True(PhoneNumber.IsValidDutch(value));

    [Theory]
    [InlineData("+31612345678")]
    [InlineData("0031612345678")]
    public void IsValidDutch_AcceptsInternationalPrefixes(string value) => Assert.True(PhoneNumber.IsValidDutch(value));

    [Theory]
    [InlineData("061234567")]
    [InlineData("06123456789")]
    public void IsValidDutch_RejectsTheWrongNumberOfDigits(string value) => Assert.False(PhoneNumber.IsValidDutch(value));

    [Fact]
    public void IsValidDutch_RejectsInputWithoutTheNationalTrunkPrefix() => Assert.False(PhoneNumber.IsValidDutch("612345678"));

    [Fact]
    public void IsDutchMobile_DistinguishesMobileFromLandline()
    {
        Assert.True(PhoneNumber.IsDutchMobile("0612345678"));
        Assert.False(PhoneNumber.IsDutchMobile("0201234567"));
    }

    [Fact]
    public void FormatDutch_ReturnsE164Format()
    {
        Assert.Equal("+31612345678", PhoneNumber.FormatDutch("06-12345678"));
        Assert.Equal("+31201234567", PhoneNumber.FormatDutch("020 123 4567"));
    }

    [Fact]
    public void FormatDutch_ThrowsForAnInvalidNumber() => Assert.Throws<ArgumentException>(() => PhoneNumber.FormatDutch("123"));
}
