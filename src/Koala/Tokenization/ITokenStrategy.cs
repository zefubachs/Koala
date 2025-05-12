namespace Koala.Tokenization;
public interface ITokenStrategy
{
    TokenInfo Evaluate(ReadOnlySpan<char> text);
}
