using Koala.Functions;

namespace Koala.Syntax;
public class FunctionNode : SyntaxNode
{
    public FunctionDescriptor Function { get; }
    public IReadOnlyList<SyntaxNode> Parameters { get; }

    public FunctionNode(FunctionDescriptor function, IEnumerable<SyntaxNode> parameters)
    {
        Function = function;
        Parameters = parameters.ToList();
    }

    public override async Task<object?> ExecuteAsync(ExecutionContext context)
    {
        var arguments = new object?[Parameters.Count];
        for (int i = 0; i < Parameters.Count; i++)
        {
            var argument = await Parameters[i].ExecuteAsync(context);
            arguments[i] = argument;
        }

        return Function.Invoke(arguments);
    }
}
