using System.Text.RegularExpressions;

namespace Koala.Tokenization;
public class RegexTokenStrategy(TokenType type, Regex regex) : ITokenStrategy
{
    public bool TryRead(ReadOnlySpan<char> text, out TokenInfo info)
    {
        var matches = regex.EnumerateMatches(text);
        if (matches.MoveNext())
        {
            var match = matches.Current;
            info = new TokenInfo
            {
                Type = type,
                Text = text[..match.Length],
                Length = match.Length,
            };
            return true;
        }

        info = default;
        return false;
    }
}
