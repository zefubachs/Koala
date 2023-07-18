using Koala.Tests.Extensions;

namespace Koala.Tests;

[TestClass]
public class ExpressionEngineTest
{
    private readonly ExpressionEngine engine = ExpressionEngine.CreateDefault();

    [TestMethod]
    public async Task Logic_Boolean_Constant()
    {
        var expression = "true and true";
        var context = new ExecutionContext(new ParameterProviderBuilder().Build());

        var result = await engine.ExecuteAsync(expression, context);
        Assert.That.IsOfType<bool>(result.Result);
        Assert.AreEqual(true, result.Result);
    }

    [TestMethod]
    public async Task Sum_Integer_Constant()
    {
        var expression = "2 + 3";
        var context = new ExecutionContext(new ParameterProviderBuilder().Build());

        var result = await engine.ExecuteAsync(expression, context);
        Assert.That.IsOfType<int>(result.Result);
        Assert.AreEqual(5, result.Result);
    }

    [TestMethod]
    public async Task Sum_String_Constant()
    {
        var expression = "\"Hallo\" + \" wereld\"";
        var context = new ExecutionContext(new ParameterProviderBuilder().Build());

        var result = await engine.ExecuteAsync(expression, context);
        Assert.That.IsOfType<string>(result.Result);
        Assert.AreEqual("Hallo wereld", result.Result);
    }

    [TestMethod]
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

        Assert.That.Instance(result.Result).IsOfType<int>()
            .And.EqualsTo(5);
    }

    [TestMethod]
    public async Task Function_No_Parameters()
    {
        var expression = "LOWER(\"HALLO\")";
        var context = new ExecutionContext(new ParameterProviderBuilder().Build());

        var result = await engine.ExecuteAsync(expression, context);
        Assert.AreEqual("hallo", result.Result);
    }

    [TestMethod]
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
        Assert.AreEqual(true, result.Result);
    }
}
