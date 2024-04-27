using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Koala.Tokenization;
public class Lexer
{
    private static readonly SkipCharacter[] skipCharacters = new SkipCharacter[]
    {
        new SkipCharacter(" "),
        new SkipCharacter("\t"),
        new SkipCharacter("\n", true),
        new SkipCharacter("\r\n", true),
    };

    private readonly List<TokenDefinition> definitions;

    private readonly ITokenScanner[] scanners =
    {
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
    };

    public Lexer(IEnumerable<TokenDefinition> definitions)
    {
        this.definitions = definitions.ToList();
    }

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

        //var cursor = new StringCursor(input);
        //while (!cursor.Finished)
        //{
        //    var token = FindMatch(cursor);
        //    if (token is not null)
        //    {
        //        tokens.Add(token);
        //        continue;
        //    }

        //    var skip = skipCharacters.FirstOrDefault(x => cursor.Current.StartsWith(x.Value));
        //    if (skip is not null)
        //    {
        //        cursor.Forward(skip.Value.Length);
        //        if (skip.NewLine)
        //            cursor.NextLine();

        //        continue;
        //    }

        //    throw new TokenizerException(cursor.Current[0], cursor.Line, cursor.Column);
        //}

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

    private Token? FindMatch(StringCursor cursor)
    {
        foreach (var definition in definitions)
        {
            if (definition.Match(cursor, out var token))
                return token;
        }

        return null;
    }

    public static Lexer CreateDefault()
    {
        var definitions = new List<TokenDefinition>
        {
            new TokenDefinition(TokenType.Operator, "^and"),
            new TokenDefinition(TokenType.Operator, "^or"),
            new TokenDefinition(TokenType.Operator, "^\\+"),
            new TokenDefinition(TokenType.Operator, "^-"),
            new TokenDefinition(TokenType.Operator, "^\\*"),
            new TokenDefinition(TokenType.Operator, "^\\\\"),
            new TokenDefinition(TokenType.Operator, "^%"),
            new TokenDefinition(TokenType.OpenParanthesis, "^\\("),
            new TokenDefinition(TokenType.CloseParenthesis, "^\\)"),
            //new TokenDefinition(TokenType.Boolean, "^true"),
            //new TokenDefinition(TokenType.Boolean, "^false"),
            new TokenDefinition(TokenType.Comma, "^,"),
            new TokenDefinition(TokenType.Operator, "^=="),
            new TokenDefinition(TokenType.Operator, "^!="),
            new TokenDefinition(TokenType.Invert, "^!"),
            new TokenDefinition(TokenType.Operator, "^>="),
            new TokenDefinition(TokenType.Operator, "^>"),
            new TokenDefinition(TokenType.Operator, "^<="),
            new TokenDefinition(TokenType.Operator, "^<"),
            new TokenDefinition(TokenType.String, "^\"([^\"]+)\""),
            new TokenDefinition(TokenType.Decimal, "^\\d+,\\d+"),
            new TokenDefinition(TokenType.Number, "^\\d+"),
            new TokenDefinition(TokenType.Reference, "^[a-zA-Z]\\w*"),
            new TokenDefinition(TokenType.Parameter, "^@([\\w.]+)")
        };

        return new Lexer(definitions);
    }
}
