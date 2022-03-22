using System;
using Common.Core.Utilities;
using Xunit;

namespace Common.Core.Tests.Utilities
{
    public class ThrowTest
    {
        [Fact]
        public void NotNull_CheckException_ReturnArgumentNullException()
        {
            string[]? array = null;
            void Act() => Throw.NotNull(array);
            Assert.Throws<ArgumentNullException>(Act);
        }

        [Fact]
        public void NotEmpty_CheckException_ReturnArgumentException()
        {
            string str = string.Empty;
            void ActCollection() => Throw.NotEmpty(Array.Empty<string>());
            void ActString() => Throw.NotEmpty(str);
            Assert.Throws<ArgumentException>(ActString);
            Assert.Throws<ArgumentException>(ActCollection);
        }
    }
}
