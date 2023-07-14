namespace ExpressionEngine.Tokenization;
public class StringCursor
{
    private readonly string value;
    private int position = 0;

    public int Column { get; private set; } = 0;
    public int Line { get; private set; } = 0;
    public string Current { get; private set; }
    public bool Finished => string.IsNullOrWhiteSpace(Current);

    public StringCursor(string value)
    {
        this.value = value;
        Current = value;
    }

    public void Forward(int amount)
    {
        position = Math.Min(value.Length, position + amount);
        if (position < value.Length)
        {
            Current = value.Substring(position);
        }
        else
        {
            Current = string.Empty;
        }
    }

    public void NextLine()
    {
        Line++;
        Column = 0;
    }
}
