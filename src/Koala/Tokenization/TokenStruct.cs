namespace Koala.Tokenization;
public readonly ref struct TokenStruct
{
    public required TokenType Type { get; init; }
    public required ReadOnlySpan<char> Text { get; init; }
    public int Column { get; init; }
}
