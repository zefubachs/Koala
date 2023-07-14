using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEngine.Ast;
public partial class BinaryNode
{

    public class AddNode : BinaryNode
    {
        public AddNode(AstNode left, AstNode right)
            : base(left, right)
        { }

        public override object? Execute(ExecutionContext context)
        {
            var leftArgument = Left.Execute(context);
            var rightArgument = Right.Execute(context);
            if (leftArgument is string leftString && rightArgument is string rightString)
                return leftString + rightString;

            if (leftArgument is int leftInteger && rightArgument is int rightInteger)
                return leftInteger + rightInteger;

            throw new NotImplementedException();
        }
    }

    public class SubstractNode : BinaryNode
    {
        public SubstractNode(AstNode left, AstNode right)
            : base(left, right)
        { }

        public override object? Execute(ExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
