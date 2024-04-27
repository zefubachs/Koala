using Koala.Ast;
using Koala.Parsing;
using Koala.Tokenization;

namespace Koala.Tests.Parsing;

public class ParserTest
{
    private readonly Parser parser;

    public ParserTest()
    {
        parser = Parser.CreateDefault();
    }

    [Fact]
    public void Simple_Parameter()
    {
        var tokens = new List<Token>
        {
            new Token(TokenType.Parameter, "Param1", 0, 0),
        };
        var result = parser.Parse(tokens);
        Assert.IsType<ParameterNode>(result);
    }

    [Fact]
    public async Task Function_Single_Parameter()
    {
        var tokens = new List<Token>()
        {
            new Token(TokenType.Reference, "LOWER", 0, 0),
            new Token(TokenType.OpenParanthesis, null, 0, 5),
            new Token(TokenType.String, "TEST", 0, 6),
            new Token(TokenType.CloseParenthesis, null, 0, 10),
        };
        var result = parser.Parse(tokens);
        var expression = result as FunctionNode;
        Assert.NotNull(expression);

        var context = new ExecutionContext(new ParameterProviderBuilder().Build());
        var returnValue = await expression.ExecuteAsync(context);
        Assert.Equal("test", returnValue);
    }

    [Fact]
    public void Function_Multiple_Parameters()
    {
        var tokens = new List<Token>
        {
            new Token(TokenType.Reference, "Lower", 0, 0),
            new Token(TokenType.OpenParanthesis, null, 0, 7),
            new Token(TokenType.String, "a", 0, 8),
            new Token(TokenType.Comma, null, 0, 9),
            new Token(TokenType.Number, "4", 0, 10),
            new Token(TokenType.CloseParenthesis, null, 0, 11),
        };
        var result = parser.Parse(tokens);
        var expression = result as FunctionNode;
        Assert.NotNull(expression);
    }

    [Fact]
    public void Logical_Single_And()
    {
        var tokens = new List<Token>
        {
            new Token(TokenType.True, bool.TrueString, 0, 0),
            new Token(TokenType.Operator, "and", 0, 5),
            new Token(TokenType.False, bool.FalseString, 0, 7),
        };
        var result = parser.Parse(tokens);
        var node = result as BinaryNode.AndNode;
        Assert.NotNull(node);
    }
}
