using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Koala.Tokenization;
public class RegexTokenScanner(TokenType tokenType, Regex regex) : ITokenScanner
{
    public RegexTokenScanner(TokenType tokenType, [StringSyntax("regex")] string pattern)
        : this(tokenType, new Regex(pattern))
    { }

    public (Token token, int positions)? Scan(ref StringCursor cursor)
    {
        var matchIterator = regex.EnumerateMatches(cursor.Value);
        if (matchIterator.MoveNext())
        {
            var match = matchIterator.Current;            
            var value = cursor.Value.Slice(match.Index, match.Length).ToString();
            var token = new Token(tokenType, value, cursor.Line, cursor.Column);
            return (token, value.Length);
        }

        return null;
    }
}
