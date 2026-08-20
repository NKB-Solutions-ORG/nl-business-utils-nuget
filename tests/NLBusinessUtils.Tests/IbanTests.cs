using NLBusinessUtils;

namespace NLBusinessUtils.Tests;

public class IbanTests
{
    [Fact]
    public void IsValidDutch_AcceptsAWellKnownValidDutchIban() => Assert.True(Iban.IsValidDutch("NL91ABNA0417164300"));

    [Fact]
    public void IsValidDutch_AcceptsTheSameIbanWithSpacesAndLowercaseLetters() =>
        Assert.True(Iban.IsValidDutch("nl91 abna 0417 1643 00"));

    [Fact]
    public void IsValidDutch_RejectsAMistypedChecksumDigit() => Assert.False(Iban.IsValidDutch("NL91ABNA0417164301"));

    [Fact]
    public void IsValidDutch_RejectsANonDutchIban() => Assert.False(Iban.IsValidDutch("DE89370400440532013000"));

    [Theory]
    [InlineData("NL91ABNA041716430")]
    [InlineData("not an iban")]
    public void IsValidDutch_RejectsMalformedInput(string value) => Assert.False(Iban.IsValidDutch(value));

    [Fact]
    public void Format_GroupsTheIbanIntoBlocksOfFour() => Assert.Equal("NL91 ABNA 0417 1643 00", Iban.Format("nl91abna0417164300"));

    [Fact]
    public void Format_ThrowsForAnInvalidIban() => Assert.Throws<ArgumentException>(() => Iban.Format("NL91ABNA0417164301"));
}
