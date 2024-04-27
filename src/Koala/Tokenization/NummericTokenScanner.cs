namespace Koala.Tokenization;
public class NummericTokenScanner() : ITokenScanner
{
    public (Token Token, int Length)? Scan(ref StringCursor cursor)
    {
        if (cursor.Value[0] != '.' && !char.IsDigit(cursor.Value[0]))
            return null;

        var length = 0;
        var hasDecimal = false;
        while (cursor.Value.Length > length)
        {
            if (char.IsDigit(cursor.Value[length]))
            {
                length++;
                continue;
            }

            if (cursor.Value[length] == '.')
            {
                if (hasDecimal)
                    throw new TokenizerException('.', cursor.Line, cursor.Column + length);

                hasDecimal = true;
                length++;
                continue;
            }

            if (char.IsLetter(cursor.Value[0]))
                throw new TokenizerException(cursor.Value[length], cursor.Line, cursor.Column + length);

            break;
        }

        var value = cursor.Value.Slice(0, length).ToString();
        var token = new Token(hasDecimal ? TokenType.Decimal : TokenType.Number, value, cursor.Line, cursor.Column);
        return (token, length);
    }
}
