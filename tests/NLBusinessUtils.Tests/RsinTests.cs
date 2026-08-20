using NLBusinessUtils;

namespace NLBusinessUtils.Tests;

public class RsinTests
{
    [Theory]
    [InlineData("123456782")]
    [InlineData("111222333")]
    public void IsValid_AcceptsKnownValidRsins(string value) => Assert.True(Rsin.IsValid(value));

    [Fact]
    public void IsValid_RejectsANumberThatFailsTheElfproef() => Assert.False(Rsin.IsValid("123456789"));

    [Theory]
    [InlineData("12345")]
    [InlineData("12345678A")]
    public void IsValid_RejectsWrongLengthsAndNonDigits(string value) => Assert.False(Rsin.IsValid(value));

    [Fact]
    public void Normalize_ReturnsTheNineDigitCanonicalForm() => Assert.Equal("123456782", Rsin.Normalize("123456782"));

    [Fact]
    public void Normalize_ThrowsForAnInvalidRsin() => Assert.Throws<ArgumentException>(() => Rsin.Normalize("123456789"));
}
