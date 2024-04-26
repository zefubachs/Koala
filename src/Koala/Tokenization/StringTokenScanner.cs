namespace Koala.Tokenization;
public class StringTokenScanner : ITokenScanner
{
    public (Token token, int positions)? Scan(ref StringCursor cursor)
    {
        if (cursor.Value[0] == '"')
        {
            var current = cursor.Value[1..];
            while (true)
            {
                if (current.IsEmpty)
                    throw new TokenizerException("Missing '\"' at end of string.", cursor.Value[^1], cursor.Line, cursor.Column);
                
                if (current[0] == '"')
                {
                    var end = cursor.Value.Length - current.Length;
                    var value = cursor.Value[1..end].ToString();
                    var token = new Token(TokenType.String, value, cursor.Line, cursor.Column);
                    return (token, value.Length + 2);
                }

                current = current[1..];
            }
        }

        return null;
    }
}
