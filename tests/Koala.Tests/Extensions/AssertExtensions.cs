using Koala.Tokenization;

namespace Koala.Tests;
public static class AssertExtensions
{
    public static void IsOfType(this (Token Token, int Length)? result, TokenType tokenType, string? value, int length)
    {
        Assert.NotNull(result);
        Assert.Equal(tokenType, result.Value.Token.Type);
        Assert.Equal(value, result.Value.Token.Value);
        Assert.Equal(length, result.Value.Length);
    }

    public static void HasTokens(this IReadOnlyCollection<Token> tokens, params (TokenType type, string? value)[] asserts)
    {
        Assert.Equal(asserts.Length, tokens.Count);
        Assert.Collection(tokens, asserts.Select(x => new Action<Token>((t) =>
        {
            Assert.Equal(x.type, t.Type);
            Assert.Equal(x.value, t.Value);
        })).ToArray());
    }
}
