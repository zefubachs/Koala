﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEngine.Ast;
public partial class BinaryNode
{
    public class EqualsNode : BinaryNode
    {
        public EqualsNode(AstNode left, AstNode right)
            : base(left, right)
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
            : base(left, right)
        { }

        public override object? Execute(ExecutionContext context)
        {
            var leftValue = Left.Execute(context);
            var rightValue = Right.Execute(context);
            return !Equals(leftValue, rightValue);
        }
    }
}
