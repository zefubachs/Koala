namespace Koala.Ast;
public class ParameterNode : AstNode
{
    public string ParameterName { get; }

    public ParameterNode(string parameterName)
    {
        ParameterName = parameterName;
    }

    public override async Task<object?> ExecuteAsync(ExecutionContext context)
    {
        var value = await context.Parameters.GetValueAsync(ParameterName);
        if (value is null)
            throw new InvalidOperationException($"Parameter '{ParameterName}' is not provided");

        return value;
    }
}
