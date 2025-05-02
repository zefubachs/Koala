using Koala.Syntax;
using Koala.Tokenization;

namespace Koala;
public class Interpreter
{
    private readonly IReadOnlyList<ITokenStrategy> strategies = TokenStrategies.Default;

    public Task<ExecuteStructResult> Execute(string expression, ParameterProvider parameters)
    {
        var root = Parse(expression);
        return Execute(root, parameters);
    }

    public async Task<ExecuteStructResult> Execute(IElement root, ParameterProvider parameters)
    {
        var ctx = new ExecutionContext(parameters);
        var result = await root.ExecuteAsync(ctx);
        return new ExecuteStructResult(result, root);
    }

    public IElement Parse(ReadOnlySpan<char> expression)
    {
        var lexer = new LexerStruct(expression, strategies);
        return Parse(ref lexer);
    }

    private static IElement Parse(ref LexerStruct lexer)
    {
        IElement? root = null;

        while (lexer.MoveNext())
        {
            if (root is null)
            {
                root = Grammar_Start(ref lexer);
                continue;
            }

            if (lexer.Current.Type == TokenType.Operator)
            {
                var opToken = lexer.Current;
                if (!lexer.MoveNext())
                    throw ParseStructException.UnexptectedToken(opToken);

                var right = Grammar_Start(ref lexer);
                switch (opToken.Text)
                {
                    case ['-']:
                        root = new BinaryElement.Subtract(root, right);
                        break;
                    case ['+']:
                        root = new BinaryElement.Addition(root, right);
                        break;
                    case ['*']:

                        break;
                    case ['/'] or ['\\']:

                        break;
                }
                continue;
            }
        }

        return root ?? new ConstantElement(0);
    }

    private static IElement Grammar_Start(ref LexerStruct lexer)
    {
        switch (lexer.Current.Type)
        {
            case TokenType.Number: return new ConstantElement(int.Parse(lexer.Current.Text));
            case TokenType.Decimal: return new ConstantElement(decimal.Parse(lexer.Current.Text, System.Globalization.CultureInfo.InvariantCulture));
            case TokenType.String: return new ConstantElement(lexer.Current.Text.ToString());
            case TokenType.False: return new ConstantElement(false);
            case TokenType.True: return new ConstantElement(true);
            case TokenType.Operator when lexer.Current.Text[0] == '-':
                var opToken = lexer.Current;
                if (!lexer.MoveNext())
                    throw ParseStructException.UnexptectedToken(opToken);

                if (lexer.Current.Type == TokenType.Number)
                    return new ConstantElement(-int.Parse(lexer.Current.Text));

                if (lexer.Current.Type == TokenType.Decimal)
                    return new ConstantElement(-decimal.Parse(lexer.Current.Text));

                throw ParseStructException.UnexptectedToken(lexer.Current);
            case TokenType.Variable: return new VariableElement(lexer.Current.Text.ToString());
            default:
                throw ParseStructException.UnexptectedToken(lexer.Current);
        }
    }
}
