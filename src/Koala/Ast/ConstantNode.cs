using System.Diagnostics;
using System.Text;

namespace Koala.Ast;

[DebuggerDisplay("Constant [{Type}]: {Value}")]
public class ConstantNode(Type type, object? value) : IAstNode
{
    public Type Type { get; } = type;
    public object? Value { get; } = value;

    public ValueTask<object?> ExecuteAsync(ExpressionContext context, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(Value);
    }

    public void Generate(StringBuilder builder)
    {
        switch (Value)
        {
            case string s:
                builder.Append('"').Append(s).Append('"');
                break;
            default:
                builder.Append(Value);
                break;
        }
    }
}
