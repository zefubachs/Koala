namespace Koala.Ast;
public partial class BinaryNode
{
    public class EqualsNode : BinaryNode
    {
        public EqualsNode(AstNode left, AstNode right)
            : base(NodeType.Equals, left, right)
        { }

        public override async Task<object?> ExecuteAsync(ExecutionContext context)
        {
            var leftValue = await Left.ExecuteAsync(context);
            var rightValue = await Right.ExecuteAsync(context);
            return Equals(leftValue, rightValue);
        }
    }

    public class NotEqualsNode : BinaryNode
    {
        public NotEqualsNode(AstNode left, AstNode right)
            : base(NodeType.NotEquals, left, right)
        { }

        public override async Task<object?> ExecuteAsync(ExecutionContext context)
        {
            var leftValue = await Left.ExecuteAsync(context);
            var rightValue = await Right.ExecuteAsync(context);
            return !Equals(leftValue, rightValue);
        }
    }
}
