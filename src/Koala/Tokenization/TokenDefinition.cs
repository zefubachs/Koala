using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Koala.Tokenization;
public class TokenDefinition
{
    private readonly TokenType tokenType;
    private readonly Regex regex;

    public TokenDefinition(TokenType tokenType, Regex regex)
    {
        this.tokenType = tokenType;
        this.regex = regex;
    }

    public TokenDefinition(TokenType tokenType, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
        : this(tokenType, new Regex(pattern, RegexOptions.IgnoreCase))
    { }

    public bool Match(StringCursor cursor, [NotNullWhen(true)] out Token? token)
    {
        var match = regex.Match(cursor.Current);
        if (match.Success)
        {
            var value = match.Value;
            if (match.Groups.Count > 1)
                value = match.Groups[1].Value;

            token = new Token(tokenType, value, cursor.Line, cursor.Column);
            cursor.Forward(match.Length);
            return true;
        }

        token = null;
        return false;
    }
}
