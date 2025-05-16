namespace Koala;
public interface IParameterProvider
{
    ValueTask<object?> GetAsync(string name, CancellationToken cancellationToken = default);
}
