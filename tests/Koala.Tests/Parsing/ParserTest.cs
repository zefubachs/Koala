using Koala.Ast;
using Koala.Parsing;
using Koala.Tokenization;

namespace Koala.Tests.Parsing;

[TestClass]
public class ParserTest
{
    private readonly Parser parser;

    public ParserTest()
    {
        parser = Parser.CreateDefault();
    }

    [TestMethod]
    public void Simple_Parameter()
    {
        var tokens = new List<Token>
        {
            new Token(TokenType.Parameter, "Param1", 0, 0),
        };
        var result = parser.Parse(tokens);
        Assert.IsInstanceOfType(result, typeof(ParameterNode));
    }

    [TestMethod]
    public async Task Function_Single_Parameter()
    {
        var tokens = new List<Token>()
        {
            new Token(TokenType.Function, "LOWER", 0, 0),
            new Token(TokenType.OpenParanthesis, null, 0, 5),
            new Token(TokenType.String, "TEST", 0, 6),
            new Token(TokenType.CloseParenthesis, null, 0, 10),
        };
        var result = parser.Parse(tokens);
        var expression = result as FunctionNode;
        Assert.IsNotNull(expression);

        var context = new ExecutionContext(new ParameterProviderBuilder().Build());
        var returnValue = await expression.ExecuteAsync(context);
        Assert.AreEqual("test", returnValue);
    }

    [TestMethod]
    public void Function_Multiple_Parameters()
    {
        var tokens = new List<Token>
        {
            new Token(TokenType.Function, "Lower", 0, 0),
            new Token(TokenType.OpenParanthesis, null, 0, 7),
            new Token(TokenType.String, "a", 0, 8),
            new Token(TokenType.Comma, null, 0, 9),
            new Token(TokenType.Number, "4", 0, 10),
            new Token(TokenType.CloseParenthesis, null, 0, 11),
        };
        var result = parser.Parse(tokens);
        var expression = result as FunctionNode;
        Assert.IsNotNull(expression);
    }

    [TestMethod]
    public void Logical_Single_And()
    {
        var tokens = new List<Token>
        {
            new Token(TokenType.Boolean, bool.TrueString, 0, 0),
            new Token(TokenType.Operator, "and", 0, 5),
            new Token(TokenType.Boolean, bool.FalseString, 0, 7),
        };
        var result = parser.Parse(tokens);
        var node = result as BinaryNode.AndNode;
        Assert.IsNotNull(node);
    }
}
