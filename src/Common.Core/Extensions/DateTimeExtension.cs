using System;
using System.Globalization;

namespace Common.Core.Extensions
{
    public static class DateTimeExtension
    {
        private const int _maxYear = 9999;

        public static DateTime FirstDateOfWeekIso8601(this DateTime datetime, int year = _maxYear, int weekOfYear = 0) => FirstDateOfWeekIso8601(year == _maxYear ? datetime.Year : year, weekOfYear);

        public static DateTime FirstDateOfWeekIso8601(int year, int weekOfYear)
        {
            //TODO: message
            if (year is < 1 or > _maxYear)
                throw new ArgumentException("the year should be in the range 1 to 9999");

            if ((year - 1) * 52 <= weekOfYear)
                throw new ArgumentException("Test");

            var jan = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan.DayOfWeek;

            DateTime firstThursday = jan.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            if (firstWeek == 1)
                weekOfYear -= 1;
            
            return firstThursday.AddDays(weekOfYear * 7 - 3);
        }
    }
}