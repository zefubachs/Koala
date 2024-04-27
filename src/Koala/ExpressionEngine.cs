using Koala.Parsing;
using Koala.Tokenization;

namespace Koala;
public class ExpressionEngine(Lexer lexer, Parser parser)
{
    public async Task<ExecuteResult> ExecuteAsync(string expression, ExecutionContext context)
    {
        var tokens = lexer.Tokenize(expression);
        var node = parser.Parse(tokens);

        var result = await node.ExecuteAsync(context);
        return new ExecuteResult(result, node);
    }

    public static ExpressionEngine CreateDefault()
    {
        return new ExpressionEngine(Lexer.CreateDefault(), Parser.CreateDefault());
    }
}
