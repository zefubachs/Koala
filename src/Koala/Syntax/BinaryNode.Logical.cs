namespace Koala.Syntax;
public partial class BinaryNode
{
    public class AndNode : BinaryNode
    {
        public AndNode(SyntaxNode left, SyntaxNode right)
            : base(NodeType.And, left, right)
        { }

        public override async Task<object?> ExecuteAsync(ExecutionContext context)
        {
            var leftValue = await Left.ExecuteAsync(context);
            var rightValue = await Right.ExecuteAsync(context);
            if (leftValue is bool leftBool && rightValue is bool rightBool)
                return leftBool && rightBool;

            throw new ArgumentException("Value is not a boolean");
        }
    }

    public class OrNode : BinaryNode
    {
        public OrNode(SyntaxNode left, SyntaxNode right)
            : base(NodeType.Or, left, right)
        { }

        public override async Task<object?> ExecuteAsync(ExecutionContext context)
        {
            var leftValue = await Left.ExecuteAsync(context);
            var rightValue = await Right.ExecuteAsync(context);
            if (leftValue is bool leftBool && rightValue is bool rightBool)
                return leftBool || rightBool;

            throw new ArgumentException("Value is not a boolean");
        }
    }
}
