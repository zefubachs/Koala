using Koala.Parameters;

namespace Koala;
public class ParameterProvider
{
    private readonly IEnumerable<IParameterSource> sources;

    public ParameterProvider(IEnumerable<IParameterSource> sources)
    {
        this.sources = sources;
    }

    public async Task<object?> GetValueAsync(string name)
    {
        foreach (var source in sources)
        {
            var value = await source.GetValueAsync(name);
            if (value is not null)
                return value;
        }

        return null;
    }
}
