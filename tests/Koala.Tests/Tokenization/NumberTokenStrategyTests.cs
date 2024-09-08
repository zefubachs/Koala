using Koala.Tokenization;

namespace Koala.Tests.Tokenization;
public class NumberTokenStrategyTests
{
    private readonly NumberTokenStrategy strategy = new();

    [Fact]
    public void Decimal_Test()
    {
        var input = "12.3";
        var result = strategy.TryRead(input, out var info);

        Assert.True(result);
        Assert.Equal(TokenType.Decimal, info.Type);
        Assert.Equal("12.3", info.Text);
        Assert.Equal(4, info.Length);
    }
}
