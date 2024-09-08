namespace Koala.Tokenization;
public class StringTokenStrategy : ITokenStrategy
{
    public bool TryRead(ReadOnlySpan<char> text, out TokenInfo info)
    {
        if (text[0] == '"')
        {
            var next = text[1..].IndexOf('"');
            if (next >= 0)
            {
                info = new TokenInfo
                {
                    Type = TokenType.String,
                    Text = text[1..(next + 1)],
                    Length = next + 2,
                };
                return true;
            }
        }

        info = default;
        return false;
    }
}