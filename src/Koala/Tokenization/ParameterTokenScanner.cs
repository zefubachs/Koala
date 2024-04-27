namespace Koala.Tokenization;
public class ParameterTokenScanner : ITokenScanner
{
    public (Token Token, int Length)? Scan(ref StringCursor cursor)
    {
        if (cursor.Value[0] != '@')
            return null;

        var length = 1;
        while (length < cursor.Value.Length)
        {
            char current = cursor.Value[length];
            if (!char.IsDigit(current) && !char.IsLetter(current) && current != '_')
                break;

            length++;
        }

        var value = cursor.Value.Slice(1, length - 1).ToString();
        var token = new Token(TokenType.Parameter, value, cursor.Line, cursor.Column);
        return (token, length);
    }
}
