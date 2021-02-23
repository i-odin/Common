using Common.Core.Helpers;
using Xunit;

namespace Common.Core.Test.Helpers
{
    public class SymbolHelperTests
    {
        [Fact]
        public void SymbolNineEqual()
        {
            Assert.Equal(expected: '9', actual: SymbolHelper.Nine);
        }

        [Fact]
        public void SymbolSemicolonEqual()
        {
            Assert.Equal(expected: ';', actual: SymbolHelper.Semicolon);
        }

        [Fact]
        public void SymbolZeroEqual()
        {
            Assert.Equal(expected: '0', actual: SymbolHelper.Zero);
        }
    }
}
