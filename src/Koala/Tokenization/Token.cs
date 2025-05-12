using System.Diagnostics;

namespace Koala.Tokenization;

[DebuggerDisplay("[{Type}:{Column}] {Text}")]
public readonly ref struct Token
{
    public required TokenType Type { get; init; }
    public required ReadOnlySpan<char> Text { get; init; }
    public int Column { get; init; }
}
