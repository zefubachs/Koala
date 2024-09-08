namespace Koala.Syntax;
public class ParameterNode : SyntaxNode
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
