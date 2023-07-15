using ExpressionEngine.Tests.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEngine.Tests;
public static class FluentAssertionExtensions
{
    public static FluentAssertionContinuation IsOfType<T>(this FluentAssertionBuilder builder)
    {
        if (builder.Instance is not T)
            throw new AssertFailedException($"Instance '{builder.Instance?.GetType()}' is not instance of type '{typeof(T)}'.");

        return new FluentAssertionContinuation(builder);
    }

    public static FluentAssertionContinuation EqualsTo(this FluentAssertionBuilder builder, object expected)
    {
        if (!Equals(builder.Instance, expected))
            throw new AssertFailedException($"Instance '{builder.Instance}' is not equal to expected '{expected}'");

        return new FluentAssertionContinuation(builder);
    }

    public static FluentAssertionContinuation HaveLength(this FluentAssertionBuilder builder, int length)
    {
        if (builder.Instance is not IEnumerable enumerable)
            throw new AssertFailedException($"Instance '{builder.Instance}' is not an Enumerable");

        var count = enumerable.Cast<object>().Count();
        if (count != length)
            throw new AssertFailedException($"Instance has {count} items, expected {length}");

        return new FluentAssertionContinuation(builder);
    }

    public static FluentAssertionContinuation WithItem(this FluentAssertionBuilder builder, int index, Action<FluentAssertionBuilder> configureItemAssertion)
    {
        FluentAssertionBuilder itemBuilder;
        if (builder.Instance is IList list)
        {
            var item = list[index];
            itemBuilder = new FluentAssertionBuilder(item);
        }
        else if (builder.Instance is IEnumerable enumerable)
        {
            var item = enumerable.Cast<object>().ElementAt(index);
            itemBuilder = new FluentAssertionBuilder(item);
        }
        else
            throw new AssertFailedException($"Instance {builder.Instance} is not a collection");

        configureItemAssertion(itemBuilder);
        return new FluentAssertionContinuation(builder);
    }
}
