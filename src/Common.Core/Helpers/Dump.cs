using System.Text.RegularExpressions;

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
    /// <param name="s">"56 65 74 100 99 68 86 180 90"</param>
    /// <returns>"100 180 90 56 65 74 68 86 99"</returns>
    public static string orderWeight(string s)
    {
        return string.Join(" ", s.Split(' ')
            .OrderBy(n => n.ToCharArray()
            .Select(c => (int)char.GetNumericValue(c)).Sum())
            .ThenBy(n => n));
    }

    /// <summary>
    /// Get count smileys
    /// </summary>
    /// <param name="smileys">"[':)', ';(', ';}', ':-D']"</param>
    /// <returns>2</returns>
    public static int CountSmileys(string[] smileys) => 
        smileys.Count(s => Regex.IsMatch(s, @"^[:;]{1}[~-]{0,1}[\)D]{1}$"));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str">"the-stealth-warrior"</param>
    /// <returns>"theStealthWarrior"</returns>
    public static string ToCamelCase(string str) =>
        Regex.Replace(str, @"[_-](\w)", m => m.Groups[1].Value.ToUpper());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iterable">"AAAABBBCCDAABBB"</param>
    /// <returns>{'A', 'B', 'C', 'D', 'A', 'B'}</returns>
    public static IEnumerable<T> UniqueInOrder<T>(IEnumerable<T> iterable)
    {
            if (iterable == null) yield return (T)Enumerable.Empty<T>();

            T? previous = default;
            foreach (var current in iterable)
            {
                if (current?.Equals(previous) == false)
                {
                    previous = current;
                    yield return current;
                }
            }
        }
    }