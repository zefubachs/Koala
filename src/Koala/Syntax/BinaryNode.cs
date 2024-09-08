using Koala.Syntax;

namespace Koala.Syntax;
public abstract partial class BinaryNode : SyntaxNode
{
    public SyntaxNode Left { get; }
    public SyntaxNode Right { get; }
    public NodeType Type { get; }

    protected BinaryNode(NodeType type, SyntaxNode left, SyntaxNode right)
    {
        Type = type;
        Left = left;
        Right = right;
    }
}
