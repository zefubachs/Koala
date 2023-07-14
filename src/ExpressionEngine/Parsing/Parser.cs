using ExpressionEngine.Ast;
using ExpressionEngine.Functions;
using ExpressionEngine.Tokenization;
using System.Xml.Linq;

namespace ExpressionEngine.Parsing;
public class Parser
{
    public FunctionRegistry functions;

    public Parser(FunctionRegistry functions)
    {
        this.functions = functions;
    }

    public ParseResult Parse(IEnumerable<Token> tokens)
    {
        var context = new ParseContext(tokens);
        AstNode? root = null;
        while (context.NextToken())
        {
            var left = ReadNode(context);
            if (!context.NextToken())
            {
                root = left;
                break;
            }

            var op = context.CurrentToken;
            context.NextToken();

            var right = ReadNode(context);
            context.NextToken();


            switch (op.Value?.ToLower())
            {
                case "and":
                    root = new BinaryNode.AndNode(left, right);
                    break;
                case "or":
                    root = new BinaryNode.OrNode(left, right);
                    break;
                case "==":
                    root = new BinaryNode.EqualsNode(left, right);
                    break;
                case "!=":
                    root = new BinaryNode.NotEqualsNode(left, right);
                    break;
                case "+":
                    root = new BinaryNode.AddNode(left, right);
                    break;
            }
            break;
        }

        if (root is null)
            throw new NotSupportedException("test");

        return new ParseResult(root);
    }

    private AstNode ReadNode(ParseContext context)
    {
        switch (context.CurrentToken?.Type)
        {
            case TokenType.Number: return new ConstantNode(int.Parse(context.CurrentToken.Value!), typeof(int));
            case TokenType.Decimal: return new ConstantNode(decimal.Parse(context.CurrentToken.Value!), typeof(decimal));
            case TokenType.Boolean: return new ConstantNode(bool.Parse(context.CurrentToken.Value!), typeof(bool));
            case TokenType.String: return new ConstantNode(context.CurrentToken.Value, typeof(string));
            case TokenType.Parameter: return new ParameterNode(context.CurrentToken.Value!);
            //case TokenType.OpenParanthesis:
            //    // New scope
            //    break;
            //case TokenType.Function:

            //    break;
            default:
                throw new ParseException(context.CurrentToken, $"Unexpected token");
        }
    }


    public static Parser CreateDefault()
    {
        var registry = new FunctionRegistry();
        registry.Register(typeof(Parser).Assembly);
        return new Parser(registry);
    }
}
