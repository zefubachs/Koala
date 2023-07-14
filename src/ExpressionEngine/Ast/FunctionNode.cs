using ExpressionEngine.Functions;

namespace ExpressionEngine.Ast;
public class FunctionNode : AstNode
{
    public FunctionDescriptor Function { get; }
    public IReadOnlyList<AstNode> Parameters { get; }

    public FunctionNode(FunctionDescriptor function, IEnumerable<AstNode> parameters)
    {
        Function = function;
        Parameters = parameters.ToList();
    }

    public override object? Execute(ExecutionContext context)
    {
        var arguments = new object?[Parameters.Count];
        for (int i = 0; i < Parameters.Count; i++)
        {
            var argument = Parameters[i].Execute(context);
            arguments[i] = argument;
        }

        return Function.Invoke(arguments);
    }
}
