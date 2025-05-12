using System.Text.RegularExpressions;

namespace Koala.Tokenization;
public class RegexTokenStrategy(TokenType type, Regex regex) : ITokenStrategy
{
    public TokenInfo Evaluate(ReadOnlySpan<char> text)
    {
        var matches = regex.EnumerateMatches(text);
        if (matches.MoveNext())
        {
            var match = matches.Current;
            return new TokenInfo
            {
                Length = match.Length,
                Text = text.Slice(match.Index, match.Length),
                Type = type,
            };
        }

        return default;
    }

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
