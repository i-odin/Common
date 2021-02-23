using System;
using System.Linq;
using Common.Core.Helpers;

namespace Common.Core.Extensions
{
    public static class StringExtension
    {
        public static bool IsDigitsOnly(this string str) =>
            str.All(c => c >= SymbolHelper.Zero && c <= SymbolHelper.Nine);

        public static T[] ToArray<T>(this string str, char separator = SymbolHelper.Semicolon) where T : struct
        {
            if (string.IsNullOrEmpty(str))
                return Array.Empty<T>();

            if (string.IsNullOrWhiteSpace(str))
                return Array.Empty<T>();

            var strArray = str.Split(separator);
            var result = new T[strArray.Length];

            for (var i = 0; i < strArray.Length; i++)
                result[i] = (T)Convert.ChangeType(strArray[i], typeof(T));

            return result;
        }

        public static DateTime UnixTimeToDateTime(this string str)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            if (long.TryParse(str, out long value))
            {
                // миллисекунды нужно убрать
                if (str.Length == 13)
                    value /= 1000;

                return origin.AddSeconds(value);
            }

            return origin;
        }

        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str) || str.Trim().Length == 0;
        }
    }
}
