using NLBusinessUtils;

namespace NLBusinessUtils.Tests;

public class BsnTests
{
    [Theory]
    [InlineData("123456782")]
    [InlineData("111222333")]
    public void IsValid_AcceptsKnownValidBsns(string value) => Assert.True(Bsn.IsValid(value));

    [Fact]
    public void IsValid_AcceptsAnEightDigitBsnByZeroPadding()
    {
        // "012345672" passes the elfproef; the 8 digit form omits the leading zero.
        Assert.True(Bsn.IsValid("12345672"));
        Assert.True(Bsn.IsValid("012345672"));
    }

    [Fact]
    public void IsValid_RejectsANumberThatFailsTheElfproef() => Assert.False(Bsn.IsValid("123456789"));

    [Fact]
    public void IsValid_RejectsAllZeroInput() => Assert.False(Bsn.IsValid("000000000"));

    [Theory]
    [InlineData("12345")]
    [InlineData("1234567890")]
    [InlineData("12345678A")]
    public void IsValid_RejectsWrongLengthsAndNonDigits(string value) => Assert.False(Bsn.IsValid(value));

    [Fact]
    public void IsValid_ToleratesSurroundingWhitespace() => Assert.True(Bsn.IsValid("  123456782  "));

    [Fact]
    public void Normalize_ReturnsTheNineDigitCanonicalForm() => Assert.Equal("123456782", Bsn.Normalize("123456782"));

    [Fact]
    public void Normalize_ThrowsForAnInvalidBsn() => Assert.Throws<ArgumentException>(() => Bsn.Normalize("123456789"));
}
