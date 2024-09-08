using Koala.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala;
public class ExecuteResult
{
    public object? Result { get; }
    public SyntaxNode Root { get; }

    public ExecuteResult(object? result, SyntaxNode root)
    {
        Result = result;
        Root = root;
    }
}

public class ExecuteStructResult(object? result, IElement root)
{
    public object? Result { get; } = result;
    public IElement Root { get; } = root;
}
