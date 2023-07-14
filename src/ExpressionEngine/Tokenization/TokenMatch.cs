using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEngine.Tokenization;
public class TokenMatch
{
    public bool Success { get; private set; }
    public Token Token { get; init; }

}
