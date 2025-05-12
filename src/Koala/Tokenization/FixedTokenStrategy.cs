namespace Koala.Tokenization;
public class FixedTokenStrategy(TokenType type, string value, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase) : ITokenStrategy
{
    public TokenInfo Evaluate(ReadOnlySpan<char> text)
    {
        if (text.StartsWith(value))
        {
            return new TokenInfo
            {
                Type = type,
                Text = text.Slice(0, value.Length),
                Length = value.Length,
            };
        }

        return default;
    }
}