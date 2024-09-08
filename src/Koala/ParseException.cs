using Koala.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala;
public class ParseException : Exception
{
    public Token Token { get; }

    public ParseException(Token token, string message)
        : base(message)
    {
        Token = token;
    }
}

public class ParseStructException : Exception
{
    public int Column { get; }

    public ParseStructException(string message, int column)
        : base(message)
    {
        Column = column;
    }

    public static ParseStructException UnexptectedToken(TokenStruct token)
        => new ParseStructException($"Unexpected token '{token.Text}' at {token.Column}.", token.Column);
}
