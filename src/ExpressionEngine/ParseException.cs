using ExpressionEngine.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEngine;
public class ParseException : Exception
{
    public Token Token { get; }

    public ParseException(Token token, string message)
        : base(message)
    {
        Token = token;
    }
}
