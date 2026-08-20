using NLBusinessUtils;

namespace NLBusinessUtils.Tests;

public class VestigingsnummerTests
{
    [Fact]
    public void IsValid_AcceptsATwelveDigitNumber() => Assert.True(Vestigingsnummer.IsValid("123456789012"));

    [Fact]
    public void IsValid_ToleratesSurroundingWhitespace() => Assert.True(Vestigingsnummer.IsValid("  123456789012  "));

    [Theory]
    [InlineData("12345678901")]
    [InlineData("1234567890123")]
    [InlineData("12345678901A")]
    public void IsValid_RejectsWrongLengthAndNonDigits(string value) => Assert.False(Vestigingsnummer.IsValid(value));
}
