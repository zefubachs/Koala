using Koala.Tokenization;

namespace Koala.Tests.Tokenization;
public class FixedTokenScannerTest
{
    [Fact]
    public void Valid_Single()
    {
        var scanner = new FixedTokenScanner(TokenType.Operator, "+");
        var input = "+";

        var cursor = new StringCursor(input, 0, 0);
        var result = scanner.Scan(ref cursor);

        result.IsOfType(TokenType.Operator, "+", 1);
    }

    [Fact]
    public void Not_Found_Single()
    {
        var scanner = new FixedTokenScanner(TokenType.Operator, "+");
        var input = "-";

        var cursor = new StringCursor(input, 0, 0);
        var result = scanner.Scan(ref cursor);

        Assert.Null(result);
    }

    [Fact]
    public void Valid_Multiple()
    {
        var scanner = new FixedTokenScanner(TokenType.Operator, "==");
        var input = "==";

        var cursor = new StringCursor(input, 0, 0);
        var result = scanner.Scan(ref cursor);

        result.IsOfType(TokenType.Operator, "==", 2);
    }
}
