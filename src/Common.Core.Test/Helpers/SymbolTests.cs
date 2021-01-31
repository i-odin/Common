using Common.Core.Helpers;
using Xunit;

namespace Common.Core.Test.Helpers
{
    public class SymbolTests
    {
        [Fact]
        public void SymbolNineEqual()
        {
            Assert.Equal(expected: '9', actual: Symbol.Nine);
        }

        [Fact]
        public void SymbolSemicolonEqual()
        {
            Assert.Equal(expected: ';', actual: Symbol.Semicolon);
        }

        [Fact]
        public void SymbolZeroEqual()
        {
            Assert.Equal(expected: '0', actual: Symbol.Zero);
        }
    }
}
