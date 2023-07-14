using ExpressionEngine.Functions;

namespace ExpressionEngine.Tests.Functions;

[TestClass]
public class FunctionRegistryTest
{
    [TestMethod]
    public void CreateDefault_Test()
    {
        var registry = new FunctionRegistry();
        registry.Register(typeof(FunctionRegistry).Assembly);

        var function = registry.Find("lower");
        Assert.IsNotNull(function);
    }
}
