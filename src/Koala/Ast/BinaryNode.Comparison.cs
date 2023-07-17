using Koala;
using Koala.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Ast;
public partial class BinaryNode
{
    public class EqualsNode : BinaryNode
    {
        public EqualsNode(AstNode left, AstNode right)
            : base(NodeType.Equals, left, right)
        { }

        public override object? Execute(ExecutionContext context)
        {
            var leftValue = Left.Execute(context);
            var rightValue = Right.Execute(context);
            return Equals(leftValue, rightValue);
        }
    }

    public class NotEqualsNode : BinaryNode
    {
        public NotEqualsNode(AstNode left, AstNode right)
            : base(NodeType.NotEquals, left, right)
        { }

        public override object? Execute(ExecutionContext context)
        {
            var leftValue = Left.Execute(context);
            var rightValue = Right.Execute(context);
            return !Equals(leftValue, rightValue);
        }
    }
}
