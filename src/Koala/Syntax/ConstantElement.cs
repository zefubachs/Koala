namespace Koala.Syntax;
public class ConstantElement(object? value) : IElement
{
    public object? Value { get; } = value;

    public ValueTask<object?> ExecuteAsync(ExecutionContext context)
    {
        return ValueTask.FromResult(Value);
    }

    public override string ToString()
    {
        return $"[{Value?.GetType()}] {Value}";
    }
}
