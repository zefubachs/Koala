using System.Text;

namespace Koala.Ast;
public class ParameterNode(string name) : IAstNode
{
    public string Name { get; } = name;

    public ValueTask<object?> ExecuteAsync(ExpressionContext context, CancellationToken cancellationToken = default)
    {
        return context.Parameters.GetAsync(Name, cancellationToken);
    }

    public void Generate(StringBuilder builder)
    {
        builder.Append('@').Append(Name);
    }
}
