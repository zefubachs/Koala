using System.Text;

namespace Koala.Ast;
public class ScopeNode(IAstNode child) : IAstNode
{
    public IAstNode Child { get; } = child;

    public ValueTask<object?> ExecuteAsync(ExpressionContext context, CancellationToken cancellationToken = default)
    {
        return Child.ExecuteAsync(context, cancellationToken);
    }
}
