namespace Koala;
public class TokenizerException(string message, char invalidCharacter, int line, int column) : Exception(message)
{
    public char InvalidCharacter { get; } = invalidCharacter;
    public int Line { get; } = line;
    public int Column { get; } = column;

    public TokenizerException(char invalidCharacter, int line, int column)
        : this($"Invalid token '{invalidCharacter}' at {line}:{column}", invalidCharacter, line, column)
    { }
}
