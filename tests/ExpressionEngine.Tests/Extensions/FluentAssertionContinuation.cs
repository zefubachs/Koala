namespace ExpressionEngine.Tests.Extensions;

public class FluentAssertionContinuation
{
    public FluentAssertionBuilder And { get; }

    public FluentAssertionContinuation(FluentAssertionBuilder builder)
    {
        And = builder;
    }
}
