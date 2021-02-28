using Common.Core.Helpers;
using Xunit;

namespace Common.Core.Test.Helpers
{
    public class SymbolHelperTests
    {
        [Theory]
        [InlineData(SymbolHelper.Zero, '0')]
        [InlineData(SymbolHelper.Semicolon, ';')]
        [InlineData(SymbolHelper.Nine, '9')]
        public void Symbol_String_ReturnTrue(char input, char expected)
        {
            Assert.Equal(expected, input);
        }
    }
}
