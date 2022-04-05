using Common.Core.Helpers;

namespace Common.Core.Tests.Helpers;

public class MessageTest
{
    [Theory]
    [InlineData(Messages.Body, "Body")]
    [InlineData(Messages.Method, "Method")]
    [InlineData(Messages.TraceIdentifier, "Trace Identifier")]
    [InlineData(Messages.Url, "Url")]
    [InlineData(Messages.DateTimeYearRange1To9999, "The year should be in the range 1 to 9999")]
    [InlineData(Messages.DateTimeWeekNumberRange1To9999, "The week number must not shift the year beyond 1 year to 9999 years")]
    public void Messages_String_ReturnTrue(string input, string expected)
    {
        Assert.Equal(expected, input);
    }
}