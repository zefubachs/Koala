using ExpressionEngine.Ast;

namespace ExpressionEngine.Parsing;
public class ParseResult
{
    public AstNode Root { get; }

    public ParseResult(AstNode expression)
    {
        Root = expression;
    }
}
