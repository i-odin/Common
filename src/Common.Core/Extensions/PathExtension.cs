namespace Common.Core.Extensions;
//TODO Test
public static class PathExtension
{
    public static ReadOnlySpan<char> GetFileName(string path)
    {
        if(path.IsEmpty())
            return ReadOnlySpan<char>.Empty;

        for (int i = path.Length; --i >= 0;)
        {
            var @char = path[i];
            if (IsDirectorySeparator(ref @char))
                return path.AsSpan(i + 1, path.Length - i - 1);
        }

        return ReadOnlySpan<char>.Empty;
    }

    public static bool IsDirectorySeparator(ref char @char) => @char == 92 || @char == 47;
}