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

    public Lexer(IEnumerable<TokenDefinition> definitions)
    {
        this.definitions = definitions.ToList();
    }

    public IReadOnlyCollection<Token> Tokenize(string input)
    {
        var tokens = new List<Token>();
        var cursor = new StringCursor(input);
        while (!cursor.Finished)
        {
            var token = FindMatch(cursor);
            if (token is not null)
            {
                tokens.Add(token);
                continue;
            }

            var skip = skipCharacters.FirstOrDefault(x => cursor.Current.StartsWith(x.Value));
            if (skip is not null)
            {
                cursor.Forward(skip.Value.Length);
                if (skip.NewLine)
                    cursor.NextLine();

                continue;
            }

            throw new TokenizerException(cursor.Current[0], cursor.Line, cursor.Column);
        }

        return tokens;
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
            new TokenDefinition(TokenType.Boolean, "^true"),
            new TokenDefinition(TokenType.Boolean, "^false"),
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
            new TokenDefinition(TokenType.Function, "^[a-zA-Z]\\w*"),
            new TokenDefinition(TokenType.Parameter, "^@(\\w+)")
        };

        return new Lexer(definitions);
    }
}
