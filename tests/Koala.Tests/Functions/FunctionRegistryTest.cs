using Koala.Functions;

namespace Koala.Tests.Functions;

public class FunctionRegistryTest
{
    [Fact]
    public void CreateDefault_Test()
    {
        var registry = new FunctionRegistry();
        registry.Register(typeof(FunctionRegistry).Assembly);

        var function = registry.Find("lower");
        Assert.NotNull(function);
    }
}
