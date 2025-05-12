namespace Koala.Tokenization;
public ref struct Lexer(ReadOnlySpan<char> expression, IEnumerable<ITokenStrategy> strategies)
{
    private readonly IEnumerable<ITokenStrategy> strategies = strategies;

    private ReadOnlySpan<char> text = expression;
    private int position = 0;

    public readonly Lexer GetEnumerator() => this;
    public Token Current { get; private set; }

    public bool MoveNext()
    {
        SkipTrivia();
        if (text.IsEmpty)
        {
            Current = default;
            return false;
        }

        var info = GetTokenInfo();
        Current = new Token
        {
            Type = info.Type,
            Text = info.Text,
            Column = position,
        };
        position += info.Length;
        text = text[info.Length..];
        return true;
    }

    private void SkipTrivia()
    {
        while (text.StartsWith(' '))
        {
            text = text[1..];
            position++;
        }
    }

    private TokenInfo GetTokenInfo()
    {
        foreach (var strategy in strategies)
        {
            var info = strategy.Evaluate(text);
            if (info.Length > 0)
                return info;
        }

        return new TokenInfo
        {
            Type = TokenType.Unknown,
            Text = text[..1],
            Length = 1,
        };
    }
}