using Koala.Functions;

namespace Koala.Ast;
public class ExpressionContext(IParameterProvider parameters, FunctionRegistry functions)
{
    public IParameterProvider Parameters { get; } = parameters;
    public FunctionRegistry Functions { get; } = functions;
}
