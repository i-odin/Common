using Common.Core.Extensions;
using System;
using Xunit;

namespace Common.Core.Tests.Extensions
{
    public class EnumExtensionTest
    {
        [Theory]
        [InlineData(EnumTest.One, true)]
        [InlineData(EnumTest.Two, false)]
        public void HasAttribute_MultipleEnums_ReturnExpected(EnumTest input, bool expected)
        {
            var result = input.HasAttribute<TestAttribute>();
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(EnumTest.One, typeof(TestAttribute))]
        [InlineData(EnumTest.Two, null)]
        public void GetAttribute_MultipleEnums_ReturnExpected(EnumTest input, Type expected)
        {
            var result = input.GetAttribute<TestAttribute>();
            Assert.Equal(expected, result?.GetType());
        }

        public enum EnumTest 
        {
            [Test2] [Test]  One,
            [Test2] Two 
        }

        public class TestAttribute : Attribute
        { }

        public class Test2Attribute : Attribute
        { }
    }
}