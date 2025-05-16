namespace Koala.Ast;
public class FunctionNode(string name, IEnumerable<IAstNode> parameters) : IAstNode
{
    public string Name { get; } = name;
    public IReadOnlyList<IAstNode> Parameters { get; } = parameters.ToList();

    public async ValueTask<object?> ExecuteAsync(ExpressionContext context, CancellationToken cancellationToken = default)
    {
        var descriptor = context.Functions.Find(Name);
        if (descriptor is null)
            throw new InvalidOperationException($"Function '{Name}' not found in function registry.");

        var arguments = new object?[Parameters.Count];
        for (int i = 0; i < Parameters.Count; i++)
        {
            var value = await Parameters[i].ExecuteAsync(context, cancellationToken).ConfigureAwait(false);
            arguments[i] = value;
        }

        var result = descriptor.Invoke(arguments);
        return result switch
        {
            ValueTask<object?> valueTask => await valueTask,
            Task<object?> task => await task,
            _ => result
        };
    }
}
