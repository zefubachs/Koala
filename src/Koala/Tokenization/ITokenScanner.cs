namespace Koala.Tokenization;
public interface ITokenScanner
{
    (Token token, int positions)? Scan(ref StringCursor cursor);
}
