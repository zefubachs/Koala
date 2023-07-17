namespace Koala.Tokenization;
public class SkipCharacter
{
    public string Value { get; }
    public bool NewLine { get; }

    public SkipCharacter(string value, bool newLine = false)
    {
        Value = value;
        NewLine = newLine;
    }
}
