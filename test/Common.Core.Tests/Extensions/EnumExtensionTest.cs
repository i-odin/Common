using Common.Core.Extensions;
using System;
using Xunit;

namespace Common.Core.Tests.Extensions
{
    public class EnumExtensionTest
    {
        [Theory]
        [InlineData(EnumTest.One, typeof(TestAttribute), true)]
        [InlineData(EnumTest.One, typeof(EnumExtensionTest), false)]
        [InlineData(EnumTest.One, null, false)]
        [InlineData(EnumTest.Two, typeof(TestAttribute), false)]
        public void CheckAttribute_MultipleEnums_ReturnExpected(EnumTest inputOne, Type inputTwo, bool expected)
        {
            var result = inputOne.CheckAttribute(inputTwo);
            Assert.Equal(expected, result);
        }

        public enum EnumTest
        {
            [Test]
            One,
            Two
        }

        public class TestAttribute : Attribute
        {
        }
    }
}
