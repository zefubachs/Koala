using Koala.Tokenization;

namespace Koala.Tests.Parsing;
public class LexerTests
{
    [Fact]
    public void Parse_String()
    {
        var input = "\"hello\"";
        var lexer = new Lexer(input, TokenStrategies.Default);

        var hasTokens = lexer.MoveNext();
        Assert.True(hasTokens);

        Assert.Equal(TokenType.String, lexer.Current.Type);
        Assert.Equal(input, lexer.Current.Text);
    }
}
