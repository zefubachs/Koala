using Koala.Extensions;

namespace Koala.Ast;
public abstract class BinaryNode(IAstNode left, IAstNode right) : IAstNode
{
    public IAstNode Left { get; } = left;
    public IAstNode Right { get; } = right;

    public abstract ValueTask<object?> ExecuteAsync(ExpressionContext context, CancellationToken cancellationToken = default);

    public class And(IAstNode left, IAstNode right) : BinaryNode(left, right)
    {
        public override async ValueTask<object?> ExecuteAsync(ExpressionContext context, CancellationToken cancellationToken = default)
        {
            var leftResult = await Left.ExecuteAsync(context, cancellationToken).ConfigureAwait(false);
            if (!leftResult.AsBoolean())
                return false;

            var rightResult = await Right.ExecuteAsync(context, cancellationToken).ConfigureAwait(false);
            return rightResult.AsBoolean();
        }
    }

    public class Or(IAstNode left, IAstNode right) : BinaryNode(left, right)
    {
        public override async ValueTask<object?> ExecuteAsync(ExpressionContext context, CancellationToken cancellationToken = default)
        {
            var leftResult = await Left.ExecuteAsync(context, cancellationToken).ConfigureAwait(false);
            if (leftResult.AsBoolean())
                return true;

            var rightResult = await Right.ExecuteAsync(context, cancellationToken).ConfigureAwait(false);
            return rightResult.AsBoolean();
        }
    }

    public class Addition(IAstNode left, IAstNode right) : BinaryNode(left, right)
    {
        public override async ValueTask<object?> ExecuteAsync(ExpressionContext context, CancellationToken cancellationToken = default)
        {
            var leftValue = await Left.ExecuteAsync(context);
            var rightValue = await Right.ExecuteAsync(context);

            if (leftValue is string || rightValue is string)
                return leftValue?.ToString() + rightValue?.ToString();

            if (leftValue.IsNumber(out var leftNumber) && rightValue.IsNumber(out var rightNumber))
                return leftNumber + rightNumber;

            throw new NotSupportedException();
        }
    }

    public class Subtraction(IAstNode left, IAstNode right) : BinaryNode(left, right)
    {
        public override async ValueTask<object?> ExecuteAsync(ExpressionContext context, CancellationToken cancellationToken = default)
        {
            var leftValue = await Left.ExecuteAsync(context);
            var rightValue = await Right.ExecuteAsync(context);

            if (leftValue.IsNumber(out var leftNumber) && rightValue.IsNumber(out var rightNumber))
                return leftNumber - rightNumber;

            throw new NotSupportedException();
        }
    }
}
