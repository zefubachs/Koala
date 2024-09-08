using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Koala.Tokenization;
public static partial class TokenStrategies
{
    public static IReadOnlyList<ITokenStrategy> Default { get; } = new List<ITokenStrategy>
    {
        new RegexTokenStrategy(TokenType.True, TrueRegex()),
        new RegexTokenStrategy(TokenType.False, FalseRegex()),
        new FixedTokenStrategy(TokenType.OpenParanthesis, "("),
        new FixedTokenStrategy(TokenType.CloseParenthesis, ")"),
        new FixedTokenStrategy(TokenType.Operator, "+"),
        new FixedTokenStrategy(TokenType.Operator, "-"),
        new FixedTokenStrategy (TokenType.Operator, "*"),
        new FixedTokenStrategy(TokenType.Operator, "\\"),
        new FixedTokenStrategy(TokenType.Comma, ","),
        new FixedTokenStrategy(TokenType.Accessor, "."),
        new VariableTokenStrategy(),
        new NumberTokenStrategy(),
        new StringTokenStrategy(),
        new RegexTokenStrategy(TokenType.Operator, AndOrRegex()),
        new RegexTokenStrategy(TokenType.Reference, ReferenceRegex()),
    };

    [GeneratedRegex("^true(?![a-zA-Z_])", RegexOptions.IgnoreCase)]
    private static partial Regex TrueRegex();

    [GeneratedRegex("^false(?![a-zA-Z_])", RegexOptions.IgnoreCase)]
    private static partial Regex FalseRegex();

    [GeneratedRegex("^(and|or)(?![a-zA-Z_])", RegexOptions.IgnoreCase)]
    private static partial Regex AndOrRegex();

    [GeneratedRegex("^[a-zA-Z]\\w*", RegexOptions.IgnoreCase)]
    private static partial Regex ReferenceRegex();
}
