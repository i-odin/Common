namespace Common.Core.Helpers;

internal class Dump
{
    public static string CreatePhoneNumber(int[] numbers)
    {
        return long.Parse(string.Concat(numbers)).ToString("(000) 000-0000");
    }

    /// <summary>
    /// Ordered by numbers weights
    /// </summary>
    /// <param s="56 65 74 100 99 68 86 180 90"></param>
    /// <returns>"100 180 90 56 65 74 68 86 99"</returns>
    public static string orderWeight(string s)
    {
        return string.Join(" ", s.Split(' ')
            .OrderBy(n => n.ToCharArray()
            .Select(c => (int)char.GetNumericValue(c)).Sum())
            .ThenBy(n => n));
    }
}