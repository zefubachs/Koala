using Koala;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Ast;
public abstract class AstNode
{
    public abstract object? Execute(ExecutionContext context);
}
