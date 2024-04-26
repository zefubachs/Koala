namespace Koala.Tests;

public class ExpressionEngineTest
{
    private readonly ExpressionEngine engine = ExpressionEngine.CreateDefault();

    [Fact]
    public async Task Logic_Boolean_Constant()
    {
        var expression = "true and true";
        var context = new ExecutionContext(new ParameterProviderBuilder().Build());

        var result = await engine.ExecuteAsync(expression, context);
        Assert.IsType<bool>(result.Result);
        Assert.Equal(true, result.Result);
    }

    [Fact]
    public async Task Sum_Integer_Constant()
    {
        var expression = "2 + 3";
        var context = new ExecutionContext(new ParameterProviderBuilder().Build());

        var result = await engine.ExecuteAsync(expression, context);
        Assert.IsType<int>(result.Result);
        Assert.Equal(5, result.Result);
    }

    [Fact]
    public async Task Sum_String_Constant()
    {
        var expression = "\"Hallo\" + \" wereld\"";
        var context = new ExecutionContext(new ParameterProviderBuilder().Build());

        var result = await engine.ExecuteAsync(expression, context);
        Assert.IsType<string>(result.Result);
        Assert.Equal("Hallo wereld", result.Result);
    }

    [Fact]
    public async Task Sum_Integer_Parameterized()
    {
        var expression = "@Param1 + @Param2";
        var context = new ExecutionContext(new ParameterProviderBuilder()
            .AddDictionary(new Dictionary<string, object>
            {
                ["Param1"] = 2,
                ["Param2"] = 3,
            }).Build());

        var result = await engine.ExecuteAsync(expression, context);

        Assert.IsType<int>(result.Result);
    }

    [Fact]
    public async Task Function_No_Parameters()
    {
        var expression = "LOWER(\"HALLO\")";
        var context = new ExecutionContext(new ParameterProviderBuilder().Build());

        var result = await engine.ExecuteAsync(expression, context);
        Assert.Equal("hallo", result.Result);
    }

    [Fact]
    public async Task Function_Multiple_Parameters()
    {
        var expression = "RegexMatch(@Pattern, @Input)";
        var context = new ExecutionContext(new ParameterProviderBuilder()
            .AddDictionary(new Dictionary<string, object>
            {
                ["Pattern"] = "test",
                ["Input"] = "Dit is een test",
            }).Build());

        var result = await engine.ExecuteAsync(expression, context);
        Assert.Equal(true, result.Result);
    }
}
