using Koala.Tokenization;

namespace Koala.Tests.Tokenization;
public class LexerStructTests
{
    private static LexerStruct CreateLexer(ReadOnlySpan<char> expression)
    {
        return new LexerStruct(expression, TokenStrategies.Default);
    }

    [Fact]
    public void Number_Single_Digit()
    {
        var lexer = CreateLexer("3");

        var result = lexer.MoveNext();
        Assert.True(result);
        Assert.Equal(TokenType.Number, lexer.Current.Type);
        Assert.Equal("3", lexer.Current.Text);

        result = lexer.MoveNext();
        Assert.False(result);
    }

    [Fact]
    public void Number_Double_Digit()
    {
        var lexer = CreateLexer("56");

        var result = lexer.MoveNext();
        Assert.True(result);
        Assert.Equal(TokenType.Number, lexer.Current.Type);
        Assert.Equal("56", lexer.Current.Text);

        result = lexer.MoveNext();
        Assert.False(result);
    }

    [Fact]
    public void String_Empty()
    {
        var lexer = CreateLexer("\"\"");

        var result = lexer.MoveNext();
        Assert.True(result);
        Assert.Equal(TokenType.String, lexer.Current.Type);
        Assert.Equal(string.Empty, lexer.Current.Text);

        result = lexer.MoveNext();
        Assert.False(result);
    }

    [Fact]
    public void String_Value()
    {
        var lexer = CreateLexer("\"hallo wereld\"");

        var result = lexer.MoveNext();
        Assert.True(result);
        Assert.Equal(TokenType.String, lexer.Current.Type);
        Assert.Equal("hallo wereld", lexer.Current.Text);

        result = lexer.MoveNext();
        Assert.False(result);
    }

    [Fact]
    public void Parameter_Empty()
    {
        var lexer = CreateLexer("$");

        var result = lexer.MoveNext();
        Assert.True(result);
        Assert.Equal(TokenType.Variable, lexer.Current.Type);
        Assert.Equal(string.Empty, lexer.Current.Text);

        result = lexer.MoveNext();
        Assert.False(result);
    }

    [Fact]
    public void Parameter_Simple()
    {
        var lexer = CreateLexer("$param1");

        var result = lexer.MoveNext();
        Assert.True(result);
        Assert.Equal(TokenType.Variable, lexer.Current.Type);
        Assert.Equal("param1", lexer.Current.Text);

        result = lexer.MoveNext();
        Assert.False(result);
    }

    [Fact]
    public void Simple_Addition()
    {
        var lexer = CreateLexer("1 + 3");

        var result = lexer.MoveNext();
        Assert.True(result);
        Assert.Equal(TokenType.Number, lexer.Current.Type);
        Assert.Equal("1", lexer.Current.Text);

        result = lexer.MoveNext();
        Assert.True(result);
        Assert.Equal(TokenType.Operator, lexer.Current.Type);
        Assert.Equal("+", lexer.Current.Text);

        result = lexer.MoveNext();
        Assert.True(result);
        Assert.Equal(TokenType.Number, lexer.Current.Type);
        Assert.Equal("3", lexer.Current.Text);

        result = lexer.MoveNext();
        Assert.False(result);
    }
}
