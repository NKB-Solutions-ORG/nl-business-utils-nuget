using NLBusinessUtils;

namespace NLBusinessUtils.Tests;

public class PostcodeTests
{
    [Theory]
    [InlineData("1234 AB")]
    [InlineData("1234AB")]
    [InlineData("1234ab")]
    public void IsValidDutch_AcceptsAWellFormedPostcode(string value) => Assert.True(Postcode.IsValidDutch(value));

    [Fact]
    public void IsValidDutch_RejectsALeadingZero() => Assert.False(Postcode.IsValidDutch("0123 AB"));

    [Theory]
    [InlineData("1234 SS")]
    [InlineData("1234 SA")]
    [InlineData("1234 SD")]
    public void IsValidDutch_RejectsReservedLetterCombinations(string value) => Assert.False(Postcode.IsValidDutch(value));

    [Theory]
    [InlineData("1234 A")]
    [InlineData("ABCD EF")]
    public void IsValidDutch_RejectsMalformedInput(string value) => Assert.False(Postcode.IsValidDutch(value));

    [Fact]
    public void FormatDutch_NormalizesCasingAndSpacing()
    {
        Assert.Equal("1234 AB", Postcode.FormatDutch("1234ab"));
        Assert.Equal("1234 AB", Postcode.FormatDutch("1234   ab"));
    }

    [Fact]
    public void FormatDutch_ThrowsForAnInvalidPostcode() => Assert.Throws<ArgumentException>(() => Postcode.FormatDutch("1234 SS"));
}
