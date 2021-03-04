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
            void Act() => Throw.NotNull<string>(null, "testMethod");
            Assert.Throws<ArgumentNullException>(Act);
        }

        [Fact]
        public void NotEmpty_CheckException_ReturnArgumentException()
        {
            void ActCollection() => Throw.NotEmpty(Array.Empty<string>(), "testMethod");
            void ActString() => Throw.NotEmpty(null, "testMethod");
            Assert.Throws<ArgumentException>(ActString);
            Assert.Throws<ArgumentException>(ActCollection);
        }
    }
}
