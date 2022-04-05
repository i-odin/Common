using Common.Core.Helpers;

namespace Common.Core.Extensions;
public static class StringExtension
{
    public static bool IsEmpty(this string str) => string.IsNullOrWhiteSpace(str);
    public static bool IsDigitsOnly(this string str) => str.All(c => c is >= Symbol.Zero and <= Symbol.Nine);
    public static T[] ToArray<T>(this string str, char separator = Symbol.Semicolon) where T : struct
    {
        if(str.IsEmpty())
            return Array.Empty<T>();

        var strArray = str.Split(separator);
        var result = new List<T>(strArray.Length);
        foreach (var item in strArray)
        {
            if (item.IsEmpty() || item.IsDigitsOnly() == false)
                continue;
            result.Add((T)Convert.ChangeType(item, typeof(T)));
        }
        return result.ToArray();
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
}