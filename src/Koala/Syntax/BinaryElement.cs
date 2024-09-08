namespace Koala.Syntax;
public abstract class BinaryElement : IElement
{
    public IElement Left { get; }
    public IElement Right { get; }

    public BinaryElement(IElement left, IElement right)
    {
        Left = left;
        Right = right;
    }

    public abstract ValueTask<object?> ExecuteAsync(ExecutionContext context);

    public class Addition(IElement left, IElement right) : BinaryElement(left, right)
    {
        public override async ValueTask<object?> ExecuteAsync(ExecutionContext context)
        {
            var leftValue = await Left.ExecuteAsync(context);
            var rightValue = await Right.ExecuteAsync(context);

            if (leftValue is string || rightValue is string)
                return leftValue?.ToString() + rightValue?.ToString();

            if ((leftValue is int || leftValue is decimal) && (rightValue is int || rightValue is decimal))
            {
                if (leftValue is decimal || rightValue is decimal)
                    return (decimal)leftValue + (decimal)rightValue;

                return (int)leftValue + (int)rightValue;
            }

            throw new NotSupportedException();
        }
    }

    public class Subtract(IElement left, IElement right) : BinaryElement(left, right)
    {
        public override async ValueTask<object?> ExecuteAsync(ExecutionContext context)
        {
            var leftValue = await Left.ExecuteAsync(context);
            var rightValue = await Right.ExecuteAsync(context);

            if ((leftValue is int || leftValue is decimal) && (rightValue is int || rightValue is decimal))
            {
                if (leftValue is decimal || rightValue is decimal)
                    return (decimal)leftValue - (decimal)rightValue;

                return (int)leftValue - (int)rightValue;
            }

            throw new NotSupportedException();
        }
    }
}
