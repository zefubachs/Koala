using Koala.Syntax;

namespace Koala.Parsing;
public class ParseResult
{
    public IElement Root { get; }

    public ParseResult(IElement expression)
    {
        Root = expression;
    }
}
