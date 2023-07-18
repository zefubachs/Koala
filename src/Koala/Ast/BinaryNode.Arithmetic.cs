namespace Koala.Ast;
public partial class BinaryNode
{
    public class AddNode : BinaryNode
    {
        public AddNode(AstNode left, AstNode right)
            : base(NodeType.Add, left, right)
        { }

        public override async Task<object?> ExecuteAsync(ExecutionContext context)
        {
            var leftArgument = await Left.ExecuteAsync(context);
            var rightArgument = await Right.ExecuteAsync(context);
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

        public override async Task<object?> ExecuteAsync(ExecutionContext context)
        {
            var leftArgument = await Left.ExecuteAsync(context);
            var rightArgument = await Right.ExecuteAsync(context);
            if (leftArgument is int leftInteger && rightArgument is int rightInteger)
                return leftInteger - rightInteger;

            if (leftArgument is decimal leftDecimal && rightArgument is decimal rightDecimal)
                return leftDecimal - rightDecimal;

            throw new NotSupportedException();
        }
    }
}
