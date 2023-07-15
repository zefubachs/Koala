using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEngine.Tests;

[TestClass]
public class ExpressionEngineTest
{
    private readonly ExpressionEngine engine = ExpressionEngine.CreateDefault();

    [TestMethod]
    public void Logic_Boolean_Constant()
    {
        var expression = "true and true";
        var result = engine.Execute(expression, new Dictionary<string, object?>());
        Assert.That.IsOfType<bool>(result);
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void Sum_Integer_Constant()
    {
        var expression = "2 + 3";
        var result = engine.Execute(expression, new Dictionary<string, object?>());
        Assert.That.IsOfType<int>(result);
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void Sum_String_Constant()
    {
        var expression = "\"Hallo\" + \" wereld\"";
        var result = engine.Execute(expression, new Dictionary<string, object?>());
        Assert.That.IsOfType<string>(result);
        Assert.AreEqual("Hallo wereld", result);
    }

    [TestMethod]
    public void Sum_Integer_Parameterized()
    {
        var expression = "@Param1 + @Param2";
        var parameters = new Dictionary<string, object?>
        {
            ["Param1"] = 2,
            ["Param2"] = 3,
        };
        var result = engine.Execute(expression, parameters);

        Assert.That.Instance(result).IsOfType<int>()
            .And.EqualsTo(5);
    }

    [TestMethod]
    public void Function_No_Parameters()
    {
        var expression = "LOWER(\"HALLO\")";
        var result = engine.Execute(expression, new Dictionary<string, object?>());

        Assert.AreEqual("hallo", result);
    }

    [TestMethod]
    public void Function_Multiple_Parameters()
    {
        var expression = "RegexMatch(@Pattern, @Input)";
        var parameters = new Dictionary<string, object?>
        {
            ["Pattern"] = "test",
            ["Input"] = "Dit is een test",
        };
        var result = engine.Execute(expression, parameters);
        Assert.AreEqual(true, result);
    }
}
