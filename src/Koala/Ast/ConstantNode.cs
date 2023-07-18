namespace Koala.Ast;
public class ConstantNode : AstNode
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
