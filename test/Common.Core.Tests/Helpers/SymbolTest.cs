using Common.Core.Helpers;

namespace Common.Core.Tests.Helpers;

public class SymbolTest
{
    [Theory]
    [InlineData(Symbol.Zero, '0')]
    [InlineData(Symbol.Semicolon, ';')]
    [InlineData(Symbol.Nine, '9')]
    [InlineData(Symbol.Equal, '=')]
    public void Symbol_String_ReturnTrue(char input, char expected)
    {
        Assert.Equal(expected, input);
    }
}