namespace ExpressionEngine.Ast;
public partial class BinaryNode
{
    public class AndNode : BinaryNode
    {
        public AndNode(AstNode left, AstNode right)
            : base(left, right)
        { }

        public override object? Execute(ExecutionContext context)
        {
            var leftValue = Left.Execute(context);
            var rightValue = Right.Execute(context);
            if (leftValue is bool leftBool && rightValue is bool rightBool)
                return leftBool && rightBool;

            throw new ArgumentException("Value is not a boolean");
        }
    }

    public class OrNode : BinaryNode
    {
        public OrNode(AstNode left, AstNode right)
            : base(left, right)
        { }

        public override object? Execute(ExecutionContext context)
        {
            var leftValue = Left.Execute(context);
            var rightValue = Right.Execute(context);
            if (leftValue is bool leftBool && rightValue is bool rightBool)
                return leftBool || rightBool;

            throw new ArgumentException("Value is not a boolean");
        }
    }
}
