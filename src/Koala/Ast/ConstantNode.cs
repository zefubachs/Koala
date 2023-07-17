using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Ast;
public class ConstantNode : AstNode
{
    public object? Value { get; }
    public Type Type { get; }

    public ConstantNode(object? value, Type type)
    {
        Value = value;
        Type = type;
    }

    public override object? Execute(ExecutionContext context)
    {
        return Value;
    }
}
