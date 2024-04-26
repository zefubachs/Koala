using System.Globalization;

namespace Koala.Tokenization;
public class NummericTokenScanner() : ITokenScanner
{
    public (Token token, int positions)? Scan(ref StringCursor cursor)
    {
        var length = 0;
        while (true)
        {
            if (char.IsDigit(cursor.Value[length]))
            {
                length++;
                continue;
            }


        }
    }
}
