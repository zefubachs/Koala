namespace Koala.Ast;
public abstract class AstNode
{
    public abstract Task<object?> ExecuteAsync(ExecutionContext context);
}
