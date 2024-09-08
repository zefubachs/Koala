namespace Koala.Tokenization;
public interface ITokenStrategy
{
    bool TryRead(ReadOnlySpan<char> text, out TokenInfo info);
}
