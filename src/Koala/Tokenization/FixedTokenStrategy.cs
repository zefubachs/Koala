namespace Koala.Tokenization;
public class FixedTokenStrategy(TokenType type, string value, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase) : ITokenStrategy
{
    public bool TryRead(ReadOnlySpan<char> text, out TokenInfo info)
    {
        if (text.StartsWith(value, comparison))
        {
            info = new TokenInfo
            {
                Type = type,
                Text = text[0..value.Length],
                Length = value.Length,
            };
            return true;
        }

        info = default;
        return false;
    }
}