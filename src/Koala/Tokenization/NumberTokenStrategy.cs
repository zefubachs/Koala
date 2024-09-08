namespace Koala.Tokenization;
public class NumberTokenStrategy : ITokenStrategy
{
    public bool TryRead(ReadOnlySpan<char> text, out TokenInfo info)
    {
        if (char.IsDigit(text[0]))
        {
            var length = 1;
            int? decimalIndex = null;
            while (true)
            {
                if (length > text.Length - 1)
                    break;

                var currentCharacter = text[length];
                if (currentCharacter == '.')
                {
                    if (decimalIndex.HasValue)
                        break;

                    decimalIndex = length;
                }
                else if (!char.IsDigit(text[length]))
                    break;

                length++;
            }

            info = new TokenInfo
            {
                Type = decimalIndex.HasValue ? TokenType.Decimal : TokenType.Number,
                Text = text[0..length],
                Length = length,
            };
            return true;
        }

        info = default;
        return false;
    }
}