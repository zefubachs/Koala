namespace Koala.Syntax;
public class VariableElement(string name) : IElement
{
    public string Name { get; } = name;

    public async ValueTask<object?> ExecuteAsync(ExecutionContext context)
    {
        var value = await context.Parameters.GetValueAsync(Name);
        if (value is null)
            throw new InvalidOperationException($"Parameter '{Name}' is not provided");

        return value;
    }
}
