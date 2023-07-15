namespace ExpressionEngine.Ast;
public abstract partial class BinaryNode : AstNode
{
    public AstNode Left { get; }
    public AstNode Right { get; }
    public NodeType Type { get; }

    protected BinaryNode(NodeType type, AstNode left, AstNode right)
    {
        Type = type;
        Left = left;
        Right = right;
    }
}
