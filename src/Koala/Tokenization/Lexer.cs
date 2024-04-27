using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Koala.Tokenization;
public class Lexer(IReadOnlyCollection<ITokenScanner> scanners)
{
    private static IReadOnlyCollection<ITokenScanner> DefaultScanners { get; } =
    [
        new FixedTokenScanner(TokenType.OpenParanthesis, "("),
        new FixedTokenScanner(TokenType.CloseParenthesis, ")"),
        new FixedTokenScanner(TokenType.Operator, "+"),
        new FixedTokenScanner(TokenType.Operator, "-"),
        new FixedTokenScanner(TokenType.Operator, "\\"),
        new FixedTokenScanner(TokenType.Operator, "*"),
        new FixedTokenScanner(TokenType.Operator, "%"),
        new FixedTokenScanner(TokenType.Operator, "=="),
        new FixedTokenScanner(TokenType.Operator, "!="),
        new FixedTokenScanner(TokenType.Operator, ">"),
        new FixedTokenScanner(TokenType.Operator, ">="),
        new FixedTokenScanner(TokenType.Operator, "<"),
        new FixedTokenScanner(TokenType.Operator, "<="),
        new FixedTokenScanner(TokenType.Comma, ","),
        new RegexTokenScanner(TokenType.True, @"^true(?![a-zA-Z_])", RegexOptions.IgnoreCase),
        new RegexTokenScanner(TokenType.False, @"^false(?![a-zA-Z_])", RegexOptions.IgnoreCase),
        new RegexTokenScanner(TokenType.Operator, @"^(and|or)(?![a-zA-Z_])", RegexOptions.IgnoreCase),
        new RegexTokenScanner(TokenType.Reference, @"^[a-zA-Z]\w*"),
        new ParameterTokenScanner(),
        new StringTokenScanner(),
        new NummericTokenScanner(),
        new FixedTokenScanner(TokenType.Accessor, "."),
    ];

    public IReadOnlyCollection<Token> Tokenize(ReadOnlySpan<char> input)
    {
        var tokens = new List<Token>();
        var position = 0;
        var column = 0;
        var line = 0;
        while (position < input.Length)
        {
            if (input[position] == ' ')
            {
                column++;
                position++;
                continue;
            }

            if (input[position] == '\t')
            {
                column += 3;
                position++;
                continue;
            }

            if (input[position] == '\r')
            {
                column++;
                position++;
                continue;
            }

            if (input[position] == '\n')
            {
                column = 0;
                line++;
                position++;
                continue;
            }

            var cursor = new StringCursor(input[position..], column, line);
            if (!TryScan(ref cursor, out var token, out var skip))
                throw new TokenizerException(input[position], line, column);


            position += skip;
            tokens.Add(token);
        }

        return tokens;
    }

    private bool TryScan(ref StringCursor cursor, [NotNullWhen(true)] out Token? token, out int skip)
    {
        foreach (var scanner in scanners)
        {
            var result = scanner.Scan(ref cursor);
            if (result is not null)
            {
                token = result.Value.Token;
                skip = result.Value.Length;
                return true;
            }
        }

        token = null;
        skip = 0;
        return false;
    }

    public static Lexer CreateDefault() => new Lexer(DefaultScanners);
}
