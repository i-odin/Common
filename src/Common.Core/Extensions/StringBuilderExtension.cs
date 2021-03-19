using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Common.Core.Structs;

namespace Common.Core.Extensions
{
    //TODO: Test
    public static class StringBuilderExtension
    {
        public static StringBuilder AppendJoin(
            [NotNull] this StringBuilder sb,
            [NotNull] in ReadOnlySpan<KeyValueString> values)
        {
            if (values.Length <= 0)
                return sb;

            foreach (var item in values)
                sb.Append(item.ToString());

            return sb;
        }
    }
}