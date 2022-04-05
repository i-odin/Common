namespace Common.Core.Extensions;
public static class GenericExtension
{
    public static bool In<T>(this T obj, params T[] values) =>
        values.Contains(obj);
}