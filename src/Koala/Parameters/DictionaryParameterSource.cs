namespace Koala.Parameters;
internal class DictionaryParameterSource : IParameterSource
{
    private readonly IDictionary<string, object> entries;

    public DictionaryParameterSource(IDictionary<string, object> entries)
    {
        this.entries = entries;
    }

    public Task<object?> GetValueAsync(string name)
    {
        return Task.FromResult<object?>(entries[name]);
    }
}
