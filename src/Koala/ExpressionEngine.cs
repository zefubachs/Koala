using Koala.Functions;
using Koala.Ast;
using Koala.Tokenization;

namespace Koala;
public class ExpressionEngine(FunctionRegistry functions)
{
    private readonly IReadOnlyList<ITokenStrategy> strategies = TokenStrategies.Default;
    private readonly FunctionRegistry functions = functions;

    public IAstNode Parse(string expression)
    {
        var lexer = new Lexer(expression, strategies);
        while (lexer.MoveNext())
        {
            var root = ParseStart(ref lexer);
            if (root is not null)
            {
                root = ParseRight(ref lexer, root);
                return root;
            }
        }

        return new ConstantNode(typeof(object), null);

        static IAstNode ParseRight(ref Lexer lexer, IAstNode left)
        {
            if (!lexer.MoveNext())
                return left;

            var op = lexer.Current;
            if (op.Type == TokenType.CloseParenthesis)
                return left;

            if (op.Type is not TokenType.Operator and not TokenType.And and not TokenType.Or)
                throw ParseException.UnexptectedToken(op);

            if (!lexer.MoveNext())
                throw ParseException.UnexptectedToken(op);

            IAstNode operantNode;
            var right = ParseStart(ref lexer);
            if (op.Type == TokenType.And)
            {
                operantNode = new BinaryNode.And(left, right);
            }
            else if (op.Type == TokenType.Or)
            {
                operantNode = new BinaryNode.Or(left, right);
            }
            else
            {
                operantNode = op.Text switch
                {
                    ['+'] => new BinaryNode.Addition(left, right),
                    ['-'] => new BinaryNode.Subtraction(left, right),
                    _ => throw new ParseException(op.Column, $"Unknown operant '{op.Text}' on position {op.Column}."),
                };
            }

            return ParseRight(ref lexer, operantNode);
        }

        static IAstNode ParseStart(ref Lexer lexer)
        {
            var current = lexer.Current;
            switch (current.Type)
            {
                //case TokenType.OpenParanthesis:

                //    break;
                case TokenType.Parameter:
                    return new ParameterNode(current.Text[1..].ToString());
                case TokenType.String:
                    return new ConstantNode(typeof(string), current.Text[1..^1].ToString());
                case TokenType.Number:
                    return new ConstantNode(typeof(double), double.Parse(current.Text));
                case TokenType.True:
                    return new ConstantNode(typeof(bool), true);
                case TokenType.False:
                    return new ConstantNode(typeof(bool), false);
                case TokenType.Reference:
                    return ParseFunction(ref lexer);
                default:
                    throw ParseException.UnexptectedToken(current);
            }
        }

        // Function format
        // <FUNCTION>(<PARAM 1>,<PARAM 2>, ...)
        static IAstNode ParseFunction(ref Lexer lexer)
        {
            var functionToken = lexer.Current;

            if (!lexer.MoveNext())
                throw ParseException.UnexptectedToken(functionToken);

            if (lexer.Current.Type is not TokenType.OpenParanthesis)
                throw ParseException.UnexptectedToken(functionToken);

            var currentToken = lexer.Current;
            var arguments = new List<IAstNode>();
            while (lexer.MoveNext())
            {
                currentToken = lexer.Current;
                if (currentToken.Type is TokenType.CloseParenthesis)
                    return new FunctionNode(functionToken.Text.ToString(), arguments);

                if (arguments.Count > 0)
                {
                    if (currentToken.Type != TokenType.Comma)
                        throw new ParseException(currentToken.Column, "Missing ',' for argument.");

                    if (!lexer.MoveNext())
                        throw ParseException.UnexptectedToken(currentToken);

                    currentToken = lexer.Current;
                }

                var argument = ParseStart(ref lexer);
                arguments.Add(argument);
            }

            throw new ParseException(currentToken.Column, $"Missing ')' for function '{functionToken.Text}'.");
        }
    }

    public Task<ExecuteResult> ExecuteAsync(string expression, IParameterProvider parameters, CancellationToken cancellationToken = default)
    {
        var root = Parse(expression);
        return ExecuteAsync(root, parameters, cancellationToken);
    }

    public async Task<ExecuteResult> ExecuteAsync(IAstNode node, IParameterProvider parameters, CancellationToken cancellationToken = default)
    {
        var ctx = new ExpressionContext(parameters, functions);
        var result = await node.ExecuteAsync(ctx, cancellationToken).ConfigureAwait(false);
        return new ExecuteResult(result, node);
    }

    public static ExpressionEngine CreateDefault()
    {
        var functions = new FunctionRegistry();
        functions.Register(typeof(ExpressionEngine).Assembly);
        return new ExpressionEngine(functions);
    }
}
