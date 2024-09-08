namespace Koala.Tokenization;
public class VariableTokenStrategy : ITokenStrategy
{
    public bool TryRead(ReadOnlySpan<char> text, out TokenInfo info)
    {
        if (text[0] == '$')
        {
            int length = 1;
            while (true)
            {
                if (length > text.Length - 1)
                    break;

                if (!char.IsLetter(text[length]) && !char.IsDigit(text[length])
                    && text[length] != '_')
                    break;

                length++;
            }

            info = new TokenInfo
            {
                Type = TokenType.Variable,
                Text = text[1..length],
                Length = length,
            };
            return true;
        }

        info = default;
        return false;
    }
}
