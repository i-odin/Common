using System;
using Common.Core.Extensions;
using Xunit;

namespace Common.Core.Tests.Extensions
{
    public class DateTimeExtensionTest
    {
        [Theory]
        [InlineData(2021, -1, 21)]
        [InlineData(2021, 0, 28)]
        [InlineData(2021, 1, 4)]
        [InlineData(2021, 4, 25)]
        [InlineData(2021, 52, 27)]
        [InlineData(2021, 53, 3)]
        public void FirstDateOfWeekIso8601_CalculateFirstDayOfWeekFromWeekAndYear_ReturnTrue(int year, int weekOfYear, int expected)
        {
            var result = DateTimeExtension.FirstDateOfWeekIso8601(year, weekOfYear);
            Assert.Equal(expected, result.Day);
        }

        [Theory]
        [InlineData(-2021, 0)]
        [InlineData(0, 0)]
        [InlineData(10000, 0)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(19999, 1)]
        public void FirstDateOfWeekIso8601_CalculateFirstDayOfWeekFromWeekAndYear_ReturnArgumentException(int year, int weekOfYear)
        {
            void Act() => DateTimeExtension.FirstDateOfWeekIso8601(year, weekOfYear);
            Assert.Throws<ArgumentException>(Act);
        }
    }
}