using Koala.Tokenization;
using System.Text.RegularExpressions;

namespace Koala.Tests.Tokenization;
public class RegexTokenScannerTest
{
    [Fact]
    public void Valid()
    {
        var scanner = new RegexTokenScanner(TokenType.Operator, "^and", RegexOptions.IgnoreCase);
        var input = "and test";

        var cursor = new StringCursor(input, 0, 0);
        var result = scanner.Scan(ref cursor);

        Assert.NotNull(result);
        Assert.Equal(TokenType.Operator, result.Value.Token.Type);
        Assert.Equal("and", result.Value.Token.Value);
        Assert.Equal(3, result.Value.Length);
    }

    [Fact]
    public void NotFound()
    {
        var scanner = new RegexTokenScanner(TokenType.Operator, "^and", RegexOptions.IgnoreCase);
        var input = "or test";

        var cursor = new StringCursor(input, 0, 0);
        var result = scanner.Scan(ref cursor);

        Assert.Null(result);
    }
}
