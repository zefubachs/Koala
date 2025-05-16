namespace Koala.Tests;

public class ExpressionEngineTest(ExpressionEngineTest.EngineFixture fixture) : IClassFixture<ExpressionEngineTest.EngineFixture>
{
    private readonly ExpressionEngine engine = fixture.Engine;

    [Fact]
    public void Parse_Simple_Addition()
    {
        var input = "\"hallo\" + \" \" + \"wereld\"";

        var result = engine.Parse(input);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Logic_Boolean_Constant()
    {
        var expression = "true and true";
        var parameters = new DictionaryParameterProvider();

        var result = await engine.ExecuteAsync(expression, parameters, TestContext.Current.CancellationToken);
        Assert.IsType<bool>(result.Result);
        Assert.Equal(true, result.Result);
    }

    [Fact]
    public async Task Sum_Integer_Constant()
    {
        var expression = "2 + 3";
        var parameters = new DictionaryParameterProvider();

        var result = await engine.ExecuteAsync(expression, parameters, TestContext.Current.CancellationToken);
        Assert.NotStrictEqual(5, result.Result);
    }

    [Fact]
    public async Task Sum_String_Constant()
    {
        var expression = "\"Hallo\" + \" wereld\"";
        var parameters = new DictionaryParameterProvider();

        var result = await engine.ExecuteAsync(expression, parameters, TestContext.Current.CancellationToken);

        Assert.Equal("Hallo wereld", result.Result);
    }

    [Fact]
    public async Task Sum_Integer_Parameterized()
    {
        var expression = "@Param1 + @Param2";
        var parameters = new DictionaryParameterProvider
        {
            ["Param1"] = 2,
            ["Param2"] = 3,
        };

        var result = await engine.ExecuteAsync(expression, parameters, TestContext.Current.CancellationToken);

        Assert.NotStrictEqual(5, result.Result);
    }

    [Fact]
    public async Task Function_No_Parameters()
    {
        var expression = "LOWER(\"HALLO\")";
        var parameters = new DictionaryParameterProvider();

        var result = await engine.ExecuteAsync(expression, parameters, TestContext.Current.CancellationToken);
        Assert.Equal("hallo", result.Result);
    }

    [Fact]
    public async Task Function_Multiple_Parameters()
    {
        var expression = "RegexMatch(@Pattern, @Input)";
        var parameters = new DictionaryParameterProvider
        {
            ["Pattern"] = "test",
            ["Input"] = "Dit is een test",
        };

        var result = await engine.ExecuteAsync(expression, parameters, TestContext.Current.CancellationToken);
        Assert.Equal(true, result.Result);
    }

    public class EngineFixture
    {
        public ExpressionEngine Engine { get; } = ExpressionEngine.CreateDefault();
    }
}
