namespace Koala.Syntax;
public class ConstantNode : SyntaxNode
{
    private readonly Task<object?> task;

    public object? Value => task.Result;
    public Type Type { get; }

    public ConstantNode(object? value, Type type)
    {
        task = Task.FromResult(value);
        Type = type;
    }

    public override Task<object?> ExecuteAsync(ExecutionContext context)
    {
        return task;
    }
}
