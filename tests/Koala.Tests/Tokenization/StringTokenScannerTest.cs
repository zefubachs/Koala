using Koala.Tokenization;

namespace Koala.Tests.Tokenization;

public class StringTokenScannerTest
{
    [Fact]
    public void Valid()
    {
        var scanner = new StringTokenScanner();
        var input = "\"test input\"";

        var cursor = new StringCursor(input, 0, 0);
        var result = scanner.Scan(ref cursor);

        Assert.NotNull(result);
        Assert.Equal(TokenType.String, result.Value.Token.Type);
        Assert.Equal("test input", result.Value.Token.Value);
        Assert.Equal(input.Length, result.Value.Length);
    }

    [Fact]
    public void Missing_Closing_Quotes()
    {
        var scanner = new StringTokenScanner();
        var input = "\"test input";

        Assert.Throws<TokenizerException>(() =>
        {
            var cursor = new StringCursor(input, 0, 0);
            var result = scanner.Scan(ref cursor);
        });
    }

    [Fact]
    public void Not_A_String()
    {
        var scanner = new StringTokenScanner();
        var input = "function";

        var cursor = new StringCursor(input, 0, 0);
        var result = scanner.Scan(ref cursor);

        Assert.Null(result);
    }
}
