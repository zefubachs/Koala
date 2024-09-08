namespace Koala.Syntax;
public abstract class SyntaxNode
{
    public abstract Task<object?> ExecuteAsync(ExecutionContext context);
}
