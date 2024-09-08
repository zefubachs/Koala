using Koala.Syntax;

namespace Koala.Tests;
public class InterpreterTests
{
    [Fact]
    public void Parse_Plus_Test()
    {
        var interpreter = new Interpreter();
        var parameters = new ParameterProviderBuilder().Build();

        var result = interpreter.Parse("1 + 3");

        Assert.IsType<BinaryElement.Addition>(result);
        var plus = (BinaryElement.Addition)result;
        Assert.IsType<ConstantElement>(plus.Left);
        Assert.IsType<ConstantElement>(plus.Right);
    }

    [Theory]
    [InlineData("1", 1)]
    [InlineData("true", true)]
    [InlineData("\"hallo wereld\"", "hallo wereld")]
    public async Task Execute_Constant_Test(string expression, object expectedResult)
    {
        var interpreter = new Interpreter();
        var parameters = new ParameterProviderBuilder().Build();

        var result = await interpreter.Execute(expression, parameters);

        Assert.NotNull(result.Root);
        Assert.Equal(expectedResult, result.Result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(true)]
    [InlineData("hello world")]
    public async Task Execute_Variable_Test(object variable)
    {
        var interpreter = new Interpreter();
        var parameters = new ParameterProviderBuilder()
            .AddDictionary(new Dictionary<string, object>
            {
                { "var", variable },
            })
            .Build();

        var result = await interpreter.Execute("$var", parameters);

        Assert.NotNull(result.Result);
        Assert.Equal(variable, result.Result);
    }

    [Theory]
    [InlineData("1 + 2", 3)]
    [InlineData("1 + 2 + 3", 6)]
    public async Task Execute_Sum_Test(string expression, object expectedResult)
    {
        var interpreter = new Interpreter();
        var parameters = new ParameterProviderBuilder().Build();
        
        var result = await interpreter.Execute(expression, parameters);

        Assert.Equal(expectedResult, result.Result);
    }

    [Theory]
    [InlineData("3 - 1", 2)]
    [InlineData("8 - 2 - 1", 5)]
    public async Task Execute_Subtract_Test(string expression, object expectedResult)
    {
        var interpreter = new Interpreter();
        var parameters = new ParameterProviderBuilder().Build();

        var result = await interpreter.Execute(expression, parameters);

        Assert.Equal(expectedResult, result.Result);
    }
}
