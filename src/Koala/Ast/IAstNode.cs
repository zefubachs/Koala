using System.Text;
using System.Text.Json.Serialization;

namespace Koala.Ast;

public interface IAstNode
{
    ValueTask<object?> ExecuteAsync(ExpressionContext context, CancellationToken cancellationToken = default);
}
