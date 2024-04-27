using Koala.Tokenization;

namespace Koala.Tests.Tokenization;
public class NummericTokenScannerTest
{
    [Fact]
    public void Valid_Number()
    {
        var scanner = new NummericTokenScanner();
        var input = "123";

        var cursor = new StringCursor(input, 0, 0);
        var result = scanner.Scan(ref cursor);

        result.IsOfType(TokenType.Number, "123", 3);
    }

    [Fact]
    public void Valid_Decimal()
    {
        var scanner = new NummericTokenScanner();
        var input = "123.45";

        var cursor = new StringCursor(input, 0, 0);
        var result = scanner.Scan(ref cursor);

        result.IsOfType(TokenType.Decimal, "123.45", 6);
    }
}
