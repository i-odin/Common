using System.Linq;

namespace Common.Core.Extensions
{
    public static class GenericExtensions
    {
        public static bool In<T>(this T obj, params T[] values) =>
            values.Contains(obj);
    }
}
