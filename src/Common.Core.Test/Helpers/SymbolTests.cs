using Common.Core.Helpers;
using Xunit;

namespace Common.Core.Test.Helpers
{
    public class SymbolTests
    {
        [Fact]
        public void SymbolNineEqual()
        {
            Assert.Equal('9', Symbol.Nine);
        }

        [Fact]
        public void SymbolSemicolonEqual()
        {
            Assert.Equal(';', Symbol.Semicolon);
        }

        [Fact]
        public void SymbolZeroEqual()
        {
            Assert.Equal('0', Symbol.Zero);
        }
    }
}
