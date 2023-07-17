using Koala.Ast;

namespace Koala.Parsing;
public class ParseResult
{
    public AstNode Root { get; }

    public ParseResult(AstNode expression)
    {
        Root = expression;
    }
}
