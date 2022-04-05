using System;
using Common.Core.Extensions;
using Xunit;

namespace Common.Core.Tests.Extensions
{
    public class StringExtensionTest
    {
        [Theory]
        [InlineData("1119987465131511")]
        public void IsDigitsOnly_StringOfNumbers_ReturnTrue(string input)
        {
            var result = input.IsDigitsOnly();
            Assert.True(result);
        }

        [Theory]
        [InlineData("11199wqeqe8746513qweqweqwe1511")]
        public void IsDigitsOnly_StringMultipleCharts_ReturnFalse(string input)
        {
            var result = input.IsDigitsOnly();
            Assert.False(result);
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData(" ", 0)]
        [InlineData("1", 1)]
        [InlineData("1;", 1)]
        [InlineData(";1;", 1)]
        [InlineData("1;q;", 1)]
        [InlineData("1#q#", 0)]
        [InlineData("1;1;1;9;9", 5)]
        [InlineData("1;q;1;9;9", 4)]
        public void ToArray_StringOfNumbers_ReturnTrue(string input, int expected)
        {
            var result = input.ToArray<int>();
            Assert.Equal(expected, result.Length);
        }

        [Theory]
        [InlineData("1611151178", 2021, 01, 20, 13, 59, 38, 0, DateTimeKind.Utc)]
        [InlineData("1;1;1;9;9", 1970, 01, 1, 0, 0, 0, 0, DateTimeKind.Utc)]
        public void UnixTimeToDateTime_StringConvert_ReturnTrue(string input, int year, int month, int day, int hour, int minute, int second, int millisecond, DateTimeKind kind)
        {
            var result = input.UnixTimeToDateTime();
            Assert.Equal(expected: new DateTime(year, month, day, hour, minute, second, millisecond, kind), actual: result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("\t")]
        public void IsEmpty_StringCheck_ReturnTrue(string input)
        {
            var result = input.IsEmpty();
            Assert.True(result);
        }

        [Theory]
        [InlineData("1;1;1;9;9")]
        public void IsEmpty_StringCheck_ReturnFalse(string input)
        {
            var result = input.IsEmpty();
            Assert.False(result);
        }
    }
}
