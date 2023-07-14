namespace ExpressionEngine.Ast;
public abstract partial class BinaryNode : AstNode
{
    public AstNode Left { get; }
    public AstNode Right { get; }

    protected BinaryNode(AstNode left, AstNode right)
    {
        Left = left;
        Right = right;
    }
}
