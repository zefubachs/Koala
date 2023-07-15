using ExpressionEngine.Tests.Extensions;

namespace ExpressionEngine.Tests;
public static class AssertExtensions
{
    public static void IsOfType<T>(this Assert assert, object? type)
    {
        if (type is not T)
            throw new AssertFailedException($"Is not of type {typeof(T)}, actual: {type?.GetType()}");
    }

    public static FluentAssertionBuilder Instance(this Assert assert, object? instance)
        => new FluentAssertionBuilder(instance);
}
