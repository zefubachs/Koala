using ExpressionEngine.Parsing;
using ExpressionEngine.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEngine;
public class ExpressionEngine
{
    private readonly Lexer lexer;
    private readonly Parser parser;

    public ExpressionEngine(Lexer lexer, Parser parser)
    {
        this.lexer = lexer;
        this.parser = parser;
    }

    public object? Execute(string expression, Dictionary<string, object?> parameters)
    {
        var tokens = lexer.Tokenize(expression);
        var result = parser.Parse(tokens);

        var context = new ExecutionContext(parameters);
        return result.Root.Execute(context);
    }

    public static ExpressionEngine CreateDefault()
    {
        return new ExpressionEngine(Lexer.CreateDefault(), Parser.CreateDefault());
    }
}
