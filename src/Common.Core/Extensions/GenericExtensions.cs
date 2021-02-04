using System.Linq;

namespace Common.Core.Extensions
{
    public static class GenericExtensions
    {
        //TODO: Test
        public static bool In<T>(this T obj, params T[] values)
        {
            return values.Contains(obj);
        }
    }
}
