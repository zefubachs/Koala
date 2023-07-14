using ExpressionEngine.Ast;
using ExpressionEngine.Functions;
using ExpressionEngine.Parsing;
using ExpressionEngine.Tokenization;

namespace ExpressionEngine.Tests.Parsing;

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
        Assert.IsInstanceOfType(result.Root, typeof(ParameterNode));
    }

    [TestMethod]
    public void Function_Single_Parameter()
    {
        var tokens = new List<Token>()
        {
            new Token(TokenType.Function, "LOWER", 0, 0),
            new Token(TokenType.OpenParanthesis, null, 0, 5),
            new Token(TokenType.String, "TEST", 0, 6),
            new Token(TokenType.CloseParenthesis, null, 0, 10),
        };
        var result = parser.Parse(tokens);
        var expression = result.Root as FunctionNode;
        Assert.IsNotNull(expression);

        var context = new ExecutionContext(new Dictionary<string, object>());
        var returnValue = expression.Execute(context);
        Assert.AreEqual("test", returnValue);
    }

    [TestMethod]
    public void Function_Multiple_Parameters()
    {
        var tokens = new List<Token>
        {
            new Token(TokenType.Function, "PadLeft", 0, 0),
            new Token(TokenType.OpenParanthesis, null, 0, 7),
            new Token(TokenType.String, "a", 0, 8),
            new Token(TokenType.Comma, null, 0, 9),
            new Token(TokenType.Number, "4", 0, 10),
            new Token(TokenType.CloseParenthesis, null, 0, 11),
        };
        var result = parser.Parse(tokens);
        var expression = result.Root as FunctionNode;
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
        var node = result.Root as BinaryNode.AndNode;
        Assert.IsNotNull(node);
    }
}
