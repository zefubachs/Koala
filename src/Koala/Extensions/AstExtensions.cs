using Koala.Ast;

namespace Koala.Ast;
public static class AstExtensions
{
    public static List<ParameterNode> GetParameters(this IAstNode node)
    {
        var parameters = new List<ParameterNode>();

        return parameters;

        void Recurse(IAstNode node)
        {
            if (node is ParameterNode param)
            {
                parameters.Add(param);
                return;
            }

            if(node is BinaryNode binary)
            {
                Recurse(binary.Left);
                Recurse(binary.Right);
            }

            if(node is ScopeNode scope)
            {
                Recurse(scope.Child);
            }
        }
    }
}
