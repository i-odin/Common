using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Common.Core.Helpers;

namespace Common.Core.Extensions
{
    //TODO: Test
    public static class StringBuilderExtension
    {
        public static StringBuilder AppendJoin(
            [NotNull] this StringBuilder sb,
            [NotNull] Dictionary<string, string> values,
            char separatorKeyValue = Symbol.Equal,
            char separator = Symbol.Semicolon,
            string format = "{0}{1}{2}{3}")
        {
            if (values.Count < 0)
                return sb;

            foreach (var item in values)
                sb.AppendFormat(format, item.Key, separatorKeyValue, item.Value, separator);

            return sb;
        }
    }
}
