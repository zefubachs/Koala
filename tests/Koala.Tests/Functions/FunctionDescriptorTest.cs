using Koala.Functions;

namespace Koala.Tests.Functions;

public class FunctionDescriptorTest
{
    [Fact]
    public void Single_Parameter()
    {
        var method = typeof(FunctionClass).GetMethod(nameof(FunctionClass.Echo));
        var descriptor = new FunctionDescriptor(method!);

        var input = "test";
        var output = descriptor.Invoke(new[] { input });

        Assert.Equal("test", output);
    }

    public static class FunctionClass
    {
        [Function]
        public static string Echo(string input) => input;
    }
}
