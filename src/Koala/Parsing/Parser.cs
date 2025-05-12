﻿using Koala.Syntax;
using Koala.Syntax;
using Koala.Functions;
using Koala.Tokenization;
using System.Xml.Linq;

namespace Koala.Parsing;
public class Parser
{
    public FunctionRegistry functions;

    public Parser(FunctionRegistry functions)
    {
        this.functions = functions;
    }

    public SyntaxNode Parse(IEnumerable<Token> tokens)
    {
        var context = new ParseContext(tokens);

        SyntaxNode? previousNode = null;
        while (context.NextToken())
        {
            switch (context.CurrentToken.Type)
            {
                case TokenType.Number:
                    previousNode = new ConstantNode(int.Parse(context.CurrentToken.Value!), typeof(int));
                    break;
                case TokenType.Operator:
                    if (previousNode is null)
                        throw new ParseException(context.CurrentToken, $"Unexpected operator '{context.CurrentToken.Value}'.");

                    switch (context.CurrentToken.Value)
                    {
                        case "+":

                            break;
                        case "-":

                            break;
                        case "*":

                            break;
                        case "\\":

                            break;
                        case "%":

                            break;
                        default:
                            break;
                    }

                    break;
                default:
                    throw new ParseException(context.CurrentToken, $"Unexpected token '{context.CurrentToken.Value}'.");
            }
        }

        SyntaxNode? root = null;
        while (context.NextToken())
        {
            var left = ReadNode(context);
            if (!context.NextToken())
            {
                root = left;
                break;
            }

            var op = context.CurrentToken;
            if (!context.NextToken())
                throw new ParseException(op, "Unexpected end of expression");

            var rightToken = context.CurrentToken;
            var right = ReadNode(context);
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
                default:
                    throw new ParseException(rightToken, $"Unexpected token '{rightToken.Value}'.");
            }
        }

        if (root is null)
            throw new NotSupportedException("test");

        return root;
    }

    private SyntaxNode ReadNode(ParseContext context)
    {
        switch (context.CurrentToken?.Type)
        {
            case TokenType.Number: return new ConstantNode(int.Parse(context.CurrentToken.Value!), typeof(int));
            case TokenType.Decimal: return new ConstantNode(decimal.Parse(context.CurrentToken.Value!), typeof(decimal));
            case TokenType.True: return new ConstantNode(true, typeof(bool));
            case TokenType.False: return new ConstantNode(false, typeof(bool));
            case TokenType.String: return new ConstantNode(context.CurrentToken.Value, typeof(string));
            case TokenType.Parameter: return new ParameterNode(context.CurrentToken.Value!);
            //case TokenType.OpenParanthesis:
            //    // New scope
            //    break;
            case TokenType.Reference: return ParseFunction(context.CurrentToken, context);
            default:
                throw new ParseException(context.CurrentToken, $"Unexpected token");
        }
    }

    private FunctionNode ParseFunction(Token token, ParseContext context)
    {
        var function = functions.Find(token.Value!);
        if (function is null)
            throw new ParseException(token, $"Unknown function '{token.Value}'.");

        if (!context.NextToken() || context.CurrentToken.Type != TokenType.OpenParanthesis)
            throw new ParseException(token, "Expected '('.");

        token = context.CurrentToken;
        var parameters = new List<SyntaxNode>();
        while (context.NextToken())
        {
            token = context.CurrentToken;
            if (context.CurrentToken.Type == TokenType.CloseParenthesis)
                return new FunctionNode(function, parameters);

            var node = ReadNode(context);
            parameters.Add(node);

            if (!context.NextToken() && context.CurrentToken.Type != TokenType.Comma && context.CurrentToken.Type != TokenType.CloseParenthesis)
                throw new ParseException(token, "Expected ')' or ','.");

            if (context.CurrentToken.Type == TokenType.CloseParenthesis)
                return new FunctionNode(function, parameters);
        }

        throw new ParseException(token, "Expected ')'.");
    }

    public static Parser CreateDefault()
    {
        var registry = new FunctionRegistry();
        registry.Register(typeof(Parser).Assembly);
        return new Parser(registry);
    }
}
