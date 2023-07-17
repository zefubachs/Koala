using Koala.Tokenization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Parsing;
public class ParseContext
{
    private readonly Queue<Token> queue;
    public Token? CurrentToken { get; private set; }
    public bool Empty => queue.Count == 0;

    public ParseContext(IEnumerable<Token> tokens)
    {
        queue = new Queue<Token>(tokens);
    }

    [MemberNotNullWhen(true, nameof(CurrentToken))]
    public bool NextToken()
    {
        if (Empty)
        {
            CurrentToken = null;
            return false;
        }

        CurrentToken = queue.Dequeue();
        return true;
    }
}
