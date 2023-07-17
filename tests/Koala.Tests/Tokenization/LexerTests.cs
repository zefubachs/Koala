using Koala.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Tests.Tokenization;

[TestClass]
public class LexerTests
{
    private readonly Lexer lexer = Lexer.CreateDefault();


    [TestMethod]
    public void Valid_Single_Operator()
    {
        var input = "+";
        var tokens = lexer.Tokenize(input).ToList();

        //Assert.That.Instance(tokens).HaveLength(1)
        //    .And.WithItem(0, x => x.EqualsTo(TokenType.Operator).And.EqualsTo("+"));
        Assert.AreEqual(1, tokens.Count);
        Assert.AreEqual(TokenType.Operator, tokens[0].Type);
        Assert.AreEqual("+", tokens[0].Value);
    }

    [TestMethod]
    public void Valid_Comparison_Operator()
    {
        var input = "AND";
        var tokens = lexer.Tokenize(input).ToList();
        Assert.AreEqual(1, tokens.Count);
        Assert.AreEqual(TokenType.Boolean, tokens[0].Type);
        Assert.AreEqual("AND", tokens[0].Value);
    }

    [TestMethod]
    public void Value_Single_Boolean_True()
    {
        var input = "TRUE";
        var tokens = lexer.Tokenize(input).ToList();
        Assert.AreEqual(1, tokens.Count);
        Assert.AreEqual(TokenType.Boolean, tokens[0].Type);
        Assert.AreEqual("TRUE", tokens[0].Value);
    }

    [TestMethod]
    public void Valid_Single_Integer()
    {
        var input = "123";
        var tokens = lexer.Tokenize(input).ToList();
        Assert.AreEqual(1, tokens.Count);
        Assert.AreEqual(TokenType.Number, tokens[0].Type);
        Assert.AreEqual("123", tokens[0].Value);
    }

    [TestMethod]
    public void Valid_Single_Decimal()
    {
        var input = "1,23";
        var tokens = lexer.Tokenize(input).ToList();
        Assert.AreEqual(1, tokens.Count);
        Assert.AreEqual(TokenType.Decimal, tokens[0].Type);
        Assert.AreEqual("1,23", tokens[0].Value);
    }

    [TestMethod]
    public void Valid_Single_Parameter()
    {
        var input = "@Param1";
        var tokens = lexer.Tokenize(input).ToList();
        Assert.AreEqual(1, tokens.Count);
        Assert.AreEqual(TokenType.Parameter, tokens[0].Type);
        Assert.AreEqual("Param1", tokens[0].Value);
    }

    [TestMethod]
    public void Simple_Addition()
    {
        var input = "10 + 32";
        var tokens = lexer.Tokenize(input).ToList();

        Assert.AreEqual(3, tokens.Count);
        Assert.AreEqual(TokenType.Number, tokens[0].Type);
        Assert.AreEqual("10", tokens[0].Value);
        Assert.AreEqual(TokenType.Operator, tokens[1].Type);
        Assert.AreEqual("+", tokens[1].Value);
        Assert.AreEqual(TokenType.Number, tokens[2].Type);
        Assert.AreEqual("32", tokens[2].Value);
    }

    [TestMethod]
    public void Function_With_Single_Parameter()
    {
        var input = "LOWER(@Param1)";
        var tokens = lexer.Tokenize(input).ToList();

        Assert.AreEqual(4, tokens.Count);
        Assert.AreEqual(TokenType.Function, tokens[0].Type);
        Assert.AreEqual("LOWER", tokens[0].Value);
        Assert.AreEqual(TokenType.OpenParanthesis, tokens[1].Type);

        Assert.AreEqual(TokenType.CloseParenthesis, tokens[3].Type);
    }

    [TestMethod]
    public void Function_With_Multiple_Parameters()
    {
        var input = """REGEX(@Param1, "^\d", true)""";
        var tokens = lexer.Tokenize(input).ToList();

        Assert.AreEqual(8, tokens.Count);
    }
}
