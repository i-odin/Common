namespace Common.Core.Helpers;

internal class Dump
{
    public static string CreatePhoneNumber(int[] numbers)
    {
        return long.Parse(string.Concat(numbers)).ToString("(000) 000-0000");
    }
}
