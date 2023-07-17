using System.Text.RegularExpressions;

namespace Koala.Functions;
public class StringFunctions
{
    [Function]
    public static string Lower(string input) => input.ToLower();
    [Function]
    public static string Upper(string input) => input.ToUpper();

    [Function]
    public static bool RegexMatch(string pattern, string input)
        => Regex.IsMatch(input, pattern);
}
