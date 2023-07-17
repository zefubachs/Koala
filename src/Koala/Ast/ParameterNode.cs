namespace Koala.Ast;
public class ParameterNode : AstNode
{
    public string ParameterName { get; }

    public ParameterNode(string parameterName)
    {
        ParameterName = parameterName;
    }

    public override object? Execute(ExecutionContext context)
    {
        if (context.Parameters.TryGetValue(ParameterName, out var parameter))
            return parameter;

        throw new InvalidOperationException($"Parameter '{ParameterName}' is not provided");
    }
}
