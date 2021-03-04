using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Common.Core.Extensions;

namespace Common.Core.Utilities
{
    public static class Throw
    {
        public static T NotNull<T>(T value, [NotNull] string parameterName)
        {
            if (value is null)
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static string NotEmpty(string value, [NotNull] string parameterName)
        {
            if (value.IsEmpty())
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentException(parameterName);
            }

            return value;
        }

        public static IReadOnlyCollection<T> NotEmpty<T>(IReadOnlyCollection<T> value, [NotNull] string parameterName)
        {
            NotNull(value, parameterName);
            if (value.Count == 0)
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentException(parameterName);
            }

            return value;
        }
    }
}
