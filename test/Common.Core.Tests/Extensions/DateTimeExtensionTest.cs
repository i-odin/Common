using System;
using System.Collections.Generic;
using Common.Core.Extensions;
namespace Common.Core.Tests.Extensions;

public class DateTimeExtensionTest
{
    public static IEnumerable<object[]> DateTimes =>
        new List<object[]>
        {
            new object[] { DateTime.Parse("2022-02-09T19:15:37.9043446Z"), "2022-02-09T19:15:37.9043446Z" },
            new object[] { DateTime.Parse("2022-02-09 19:15:37"), "2022-02-09T19:15:37.0000000" }
        };

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

    [Theory]
    [MemberData(nameof(DateTimes))]
    public void ToStringIso8601_ConvertDateTimeToString_ReturnTrue(DateTime input, string expected)
    {
        var result = input.Kind == DateTimeKind.Unspecified ? input.ToStringIso8601() : input.ToUniversalTime().ToStringIso8601();

        Assert.Equal(expected, result);
    }
}