using Koala.Tokenization;

namespace Koala.Tests.Tokenization;

public class LexerTests
{
    private readonly Lexer lexer = Lexer.CreateDefault();


    [Fact]
    public void Valid_Single_Operator()
    {
        var input = "+";
        var tokens = lexer.Tokenize(input).ToList();

        //Assert.That.Instance(tokens).HaveLength(1)
        //    .And.WithItem(0, x => x.EqualsTo(TokenType.Operator).And.EqualsTo("+"));
        Assert.Single(tokens);
        Assert.Equal(TokenType.Operator, tokens[0].Type);
        Assert.Equal("+", tokens[0].Value);
    }

    [Fact]
    public void Valid_Comparison_Operator()
    {
        var input = "AND";
        var tokens = lexer.Tokenize(input).ToList();
        Assert.Single(tokens);
        Assert.Equal(TokenType.Operator, tokens[0].Type);
        Assert.Equal("AND", tokens[0].Value);
    }

    [Fact]
    public void Value_Single_Boolean_True()
    {
        var input = "TRUE";
        var tokens = lexer.Tokenize(input).ToList();
        Assert.Single(tokens);
        Assert.Equal(TokenType.Boolean, tokens[0].Type);
        Assert.Equal("TRUE", tokens[0].Value);
    }

    [Fact]
    public void Valid_Single_Integer()
    {
        var input = "123";
        var tokens = lexer.Tokenize(input).ToList();
        Assert.Single(tokens);
        Assert.Equal(TokenType.Number, tokens[0].Type);
        Assert.Equal("123", tokens[0].Value);
    }

    [Fact]
    public void Valid_Single_Decimal()
    {
        var input = "1,23";
        var tokens = lexer.Tokenize(input).ToList();
        Assert.Single(tokens);
        Assert.Equal(TokenType.Decimal, tokens[0].Type);
        Assert.Equal("1,23", tokens[0].Value);
    }

    [Fact]
    public void Valid_Single_Parameter()
    {
        var input = "@Param1";
        var tokens = lexer.Tokenize(input).ToList();
        Assert.Single(tokens);
        Assert.Equal(TokenType.Parameter, tokens[0].Type);
        Assert.Equal("Param1", tokens[0].Value);
    }

    [Fact]
    public void Simple_Addition()
    {
        var input = "10 + 32";
        var tokens = lexer.Tokenize(input).ToList();

        Assert.Equal(3, tokens.Count);
        Assert.Equal(TokenType.Number, tokens[0].Type);
        Assert.Equal("10", tokens[0].Value);
        Assert.Equal(TokenType.Operator, tokens[1].Type);
        Assert.Equal("+", tokens[1].Value);
        Assert.Equal(TokenType.Number, tokens[2].Type);
        Assert.Equal("32", tokens[2].Value);
    }

    [Fact]
    public void Function_With_Single_Parameter()
    {
        var input = "LOWER(@Param1)";
        var tokens = lexer.Tokenize(input).ToList();

        Assert.Equal(4, tokens.Count);
        Assert.Equal(TokenType.Function, tokens[0].Type);
        Assert.Equal("LOWER", tokens[0].Value);
        Assert.Equal(TokenType.OpenParanthesis, tokens[1].Type);

        Assert.Equal(TokenType.CloseParenthesis, tokens[3].Type);
    }

    [Fact]
    public void Function_With_Multiple_Parameters()
    {
        var input = """REGEX(@Param1, "^\d", true)""";
        var tokens = lexer.Tokenize(input).ToList();

        Assert.Equal(8, tokens.Count);
    }
}
