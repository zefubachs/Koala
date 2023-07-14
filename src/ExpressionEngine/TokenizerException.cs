namespace ExpressionEngine;
public class TokenizerException : Exception
{
    public char InvalidCharacter { get; }
    public int Line { get; }
    public int Column { get; }

    public TokenizerException(char invalidCharacter, int line, int column)
        : base($"Invalid token '{invalidCharacter}' at {line}:{column}")
    {
        InvalidCharacter = invalidCharacter;
        Line = line;
        Column = column;
    }
}
