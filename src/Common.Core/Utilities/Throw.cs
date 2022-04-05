using System.Runtime.CompilerServices;
using Common.Core.Extensions;

namespace Common.Core.Utilities;

public static class Throw
{
    public static T NotNull<T>(T value, [CallerArgumentExpression("value")] string parameterName = "")
    {
        if (value is null)
        {
            NotEmpty(parameterName);
            throw new ArgumentNullException(parameterName);
        }

        return value;
    }

    public static string NotEmpty(string value, [CallerArgumentExpression("value")] string parameterName = "")
    {
        if (value.IsEmpty())
        {
            NotEmpty(parameterName);
            throw new ArgumentException(parameterName);
        }

        return value;
    }

    public static IReadOnlyCollection<T> NotEmpty<T>(IReadOnlyCollection<T> value, [CallerArgumentExpression("value")] string parameterName = "")
    {
        NotNull(value);
        if (value.Count == 0)
        {
            NotEmpty(parameterName);
            throw new ArgumentException(parameterName);
        }

        return value;
    }
}