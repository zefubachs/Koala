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
        Assert.That.IsOfType<int>(result);
        Assert.AreEqual(5, result);
    }
}
