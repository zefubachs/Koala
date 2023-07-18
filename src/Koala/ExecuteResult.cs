using Koala.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala;
public class ExecuteResult
{
    public object? Result { get; }
    public AstNode Root { get; }

    public ExecuteResult(object? result, AstNode root)
    {
        Result = result;
        Root = root;
    }
}
