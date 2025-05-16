using Koala.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala;

public class ParseException : Exception
{
    public int Column { get; }

    public ParseException(int column, string message)
        : base(message)
    {
        Column = column;
    }

    public static ParseException UnexptectedToken(Token token)
        => new ParseException(token.Column, $"Unexpected token '{token.Text}' at {token.Column}.");
}
