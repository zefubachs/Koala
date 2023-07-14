using ExpressionEngine.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEngine.Tests.Functions;

[TestClass]
public class FunctionDescriptorTest
{
    [TestMethod]
    public void Single_Parameter()
    {
        var method = typeof(FunctionClass).GetMethod(nameof(FunctionClass.Echo));
        var descriptor = new FunctionDescriptor(method!);

        var input = "test";
        var output = descriptor.Invoke(new[] { input });

        Assert.AreEqual("test", output);
    }

    public static class FunctionClass
    {
        [Function]
        public static string Echo(string input) => input;
    }
}
