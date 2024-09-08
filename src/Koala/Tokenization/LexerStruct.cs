using System.Globalization;

namespace Koala.Tokenization;
public ref struct LexerStruct(ReadOnlySpan<char> expression, IEnumerable<ITokenStrategy> strategies)
{
    private readonly ReadOnlySpan<char> expression = expression;
    private readonly IEnumerable<ITokenStrategy> strategies = strategies;

    private ReadOnlySpan<char> text = expression;

    public LexerStruct GetEnumerator() => this;

    public TokenStruct Current { get; private set; }

    public bool MoveNext()
    {
        text = text.TrimStart();
        if (text.IsEmpty)
        {
            Current = default;
            return false;
        }

        foreach (var strategy in strategies)
        {
            if (strategy.TryRead(text, out var info))
            {
                Current = new TokenStruct
                {
                    Type = info.Type,
                    Text = info.Text,
                    Column = expression.Length - text.Length,
                };
                text = text[info.Length..];
                return true;
            }
        }

        return false;
    }

    public void Reset()
    {
        text = expression;
    }
}