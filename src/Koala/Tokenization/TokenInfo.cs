namespace Koala.Tokenization;
public readonly ref struct TokenInfo
{
    public required TokenType Type { get; init; }
    public required ReadOnlySpan<char> Text { get; init; }
    public required int Length { get; init; }
}