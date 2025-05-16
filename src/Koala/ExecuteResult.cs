using Koala.Ast;

namespace Koala;
public class ExecuteResult(object? result, IAstNode root)
{
    public object? Result { get; } = result;
    public IAstNode Root { get; } = root;
}
