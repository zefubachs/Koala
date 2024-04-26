namespace Koala.Tokenization;
public ref struct StringCursor(ReadOnlySpan<char> value, int column, int line)
{
    public int Column { get; private set; } = column;
    public int Line { get; private set; } = line;
    public ReadOnlySpan<char> Value { get; } = value;
}
