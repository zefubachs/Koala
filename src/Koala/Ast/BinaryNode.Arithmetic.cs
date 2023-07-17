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

    public class AddNode : BinaryNode
    {
        public AddNode(AstNode left, AstNode right)
            : base(NodeType.Add, left, right)
        { }

        public override object? Execute(ExecutionContext context)
        {
            var leftArgument = Left.Execute(context);
            var rightArgument = Right.Execute(context);
            if (leftArgument is string leftString && rightArgument is string rightString)
                return leftString + rightString;

            if (leftArgument is int leftInteger && rightArgument is int rightInteger)
                return leftInteger + rightInteger;

            if (leftArgument is decimal leftDecimal && rightArgument is decimal rightDecimal)
                return leftDecimal + rightDecimal;

            throw new NotSupportedException();
        }
    }

    public class SubstractNode : BinaryNode
    {
        public SubstractNode(AstNode left, AstNode right)
            : base(NodeType.Subtract, left, right)
        { }

        public override object? Execute(ExecutionContext context)
        {
            var leftArgument = Left.Execute(context);
            var rightArgument = Right.Execute(context);
            if (leftArgument is int leftInteger && rightArgument is int rightInteger)
                return leftInteger - rightInteger;

            if (leftArgument is decimal leftDecimal && rightArgument is decimal rightDecimal)
                return leftDecimal - rightDecimal;

            throw new NotSupportedException();
        }
    }
}
