using Common.Core.Extensions;
using Xunit;

namespace Common.Core.Test.Extensions
{
    public class GenericExtensionTest
    {
        [Fact]
        public void InTrue()
        {
            const EnumTest enumTest = EnumTest.One;

            var result = enumTest.In(EnumTest.One, EnumTest.Two);

            Assert.True(result);
        }

        [Fact]
        public void InFalse()
        {
            const EnumTest enumTest = EnumTest.One;

            var result = enumTest.In(EnumTest.Two, EnumTest.Three);

            Assert.False(result);
        }

        private enum EnumTest : byte
        {
            One,
            Two,
            Three
        }
    }
}
