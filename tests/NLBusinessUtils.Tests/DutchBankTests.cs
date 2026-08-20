using NLBusinessUtils;

namespace NLBusinessUtils.Tests;

public class DutchBankTests
{
    [Fact]
    public void GetName_ReturnsTheBankNameForAKnownBankCode() =>
        Assert.Equal("ABN AMRO Bank N.V.", DutchBank.GetName("NL91ABNA0417164300"));

    [Fact]
    public void GetName_IsCaseAndWhitespaceInsensitive() =>
        Assert.Equal("ABN AMRO Bank N.V.", DutchBank.GetName("nl91 abna 0417 1643 00"));

    [Fact]
    public void GetName_ReturnsNullForAnInvalidIban() => Assert.Null(DutchBank.GetName("NL91ABNA0417164301"));

    [Fact]
    public void GetName_ReturnsNullForAValidIbanWhoseBankCodeIsNotInTheCuratedList()
    {
        // A synthetic (non-existent) but checksum-valid IBAN using bank
        // code "TEST", which is not a real Dutch bank identifier.
        Assert.Null(DutchBank.GetName("NL80TEST0000000001"));
    }
}
