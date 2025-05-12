using System.Text.RegularExpressions;

namespace Koala.Tokenization;
public static partial class TokenStrategies
{
    public static IReadOnlyList<ITokenStrategy> Default { get; } =
    [
        new RegexTokenStrategy(TokenType.True, TrueRegex),
        new RegexTokenStrategy(TokenType.False, FalseRegex),
        new FixedTokenStrategy(TokenType.OpenParanthesis, "("),
        new FixedTokenStrategy(TokenType.CloseParenthesis, ")"),
        new FixedTokenStrategy(TokenType.Operator, "+"),
        new FixedTokenStrategy(TokenType.Operator, "-"),
        new FixedTokenStrategy (TokenType.Operator, "*"),
        new FixedTokenStrategy(TokenType.Operator, "\\"),
        new FixedTokenStrategy(TokenType.Comma, ","),
        new FixedTokenStrategy(TokenType.Accessor, "."),
        new RegexTokenStrategy(TokenType.Parameter, ParameterRegex),
        new RegexTokenStrategy(TokenType.Number, NumberRegex),
        new RegexTokenStrategy(TokenType.String, StringRegex),
        new RegexTokenStrategy(TokenType.Operator, AndOrRegex),
        new RegexTokenStrategy(TokenType.Reference, ReferenceRegex),
    ];

    [GeneratedRegex("^\"[^\"]*\"")]
    private static partial Regex StringRegex { get; }
    [GeneratedRegex("^@[a-zA-Z]\\w*")]
    private static partial Regex ParameterRegex { get; }
    [GeneratedRegex("^\\d+(\\.\\d+){0,1}")]
    private static partial Regex NumberRegex { get; }
    [GeneratedRegex("^true(?![a-zA-Z_])", RegexOptions.IgnoreCase)]
    private static partial Regex TrueRegex { get; }
    [GeneratedRegex("^false(?![a-zA-Z_])", RegexOptions.IgnoreCase)]
    private static partial Regex FalseRegex { get; }
    [GeneratedRegex("^(and|or)(?![a-zA-Z_])", RegexOptions.IgnoreCase)]
    private static partial Regex AndOrRegex { get; }
    [GeneratedRegex("^[a-zA-Z]\\w*", RegexOptions.IgnoreCase)]
    private static partial Regex ReferenceRegex { get; }
}
