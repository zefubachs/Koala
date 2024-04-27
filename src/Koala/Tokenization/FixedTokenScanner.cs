namespace Koala.Tokenization;
public class FixedTokenScanner(TokenType tokenType, string key, StringComparison comparison) : ITokenScanner
{
    public FixedTokenScanner(TokenType tokenType, string key)
        : this(tokenType, key, StringComparison.InvariantCultureIgnoreCase)
    { }

    public (Token Token, int Length)? Scan(ref StringCursor cursor)
    {
        if (cursor.Value.StartsWith(key, comparison))
        {
            var token = new Token(tokenType, key, cursor.Line, cursor.Column);
            return (token, key.Length);
        }

        return null;
    }
}
