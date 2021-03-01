using Common.Core.Helpers;
using Xunit;

namespace Common.Core.Test.Helpers
{
    public class SymbolTests
    {
        [Theory]
        [InlineData(Symbol.Zero, '0')]
        [InlineData(Symbol.Semicolon, ';')]
        [InlineData(Symbol.Nine, '9')]
        public void Symbol_String_ReturnTrue(char input, char expected)
        {
            Assert.Equal(expected, input);
        }
    }
}
