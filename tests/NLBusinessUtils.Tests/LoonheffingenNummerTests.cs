using NLBusinessUtils;

namespace NLBusinessUtils.Tests;

public class LoonheffingenNummerTests
{
    [Fact]
    public void IsValid_AcceptsAWellFormedNumberBasedOnAValidRsinBsn() =>
        Assert.True(LoonheffingenNummer.IsValid("123456782L01"));

    [Fact]
    public void IsValid_AcceptsLowercaseAndLooselyFormattedInput() =>
        Assert.True(LoonheffingenNummer.IsValid("123456782 l01"));

    [Fact]
    public void IsValid_AcceptsHigherSequenceNumbers() => Assert.True(LoonheffingenNummer.IsValid("123456782L02"));

    [Fact]
    public void IsValid_RejectsANumberWhoseNineDigitPartFailsTheElfproef() =>
        Assert.False(LoonheffingenNummer.IsValid("123456789L01"));

    [Fact]
    public void IsValid_RejectsASequenceNumberOfZero() => Assert.False(LoonheffingenNummer.IsValid("123456782L00"));

    [Theory]
    [InlineData("12345678L01")]
    [InlineData("123456782B01")]
    public void IsValid_RejectsTheWrongOverallStructure(string value) => Assert.False(LoonheffingenNummer.IsValid(value));

    [Fact]
    public void Format_ReturnsTheCanonicalUppercaseForm() =>
        Assert.Equal("123456782L01", LoonheffingenNummer.Format("123456782l01"));

    [Fact]
    public void Format_ThrowsForAnInvalidNumber() =>
        Assert.Throws<ArgumentException>(() => LoonheffingenNummer.Format("123456789L01"));
}
