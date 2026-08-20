using NLBusinessUtils;

namespace NLBusinessUtils.Tests;

public class KvkNumberTests
{
    [Fact]
    public void IsValid_AcceptsAnEightDigitNumber() => Assert.True(KvkNumber.IsValid("12345678"));

    [Fact]
    public void IsValid_ToleratesSurroundingWhitespace() => Assert.True(KvkNumber.IsValid("  12345678  "));

    [Theory]
    [InlineData("1234567")]
    [InlineData("123456789")]
    [InlineData("1234567A")]
    public void IsValid_RejectsWrongLengthAndNonDigits(string value) => Assert.False(KvkNumber.IsValid(value));
}
