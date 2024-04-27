namespace Koala.Tokenization;
public interface ITokenScanner
{
    (Token Token, int Length)? Scan(ref StringCursor cursor);
}
