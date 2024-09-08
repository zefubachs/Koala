using System.Text.Json.Serialization;

namespace Koala.Syntax;

[JsonDerivedType(typeof(ConstantElement), typeDiscriminator: "constant")]
[JsonDerivedType(typeof(VariableElement), typeDiscriminator: "variable")]
public interface IElement
{
    ValueTask<object?> ExecuteAsync(ExecutionContext context);
}
