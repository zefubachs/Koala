namespace Koala;
public class DictionaryParameterProvider : IParameterProvider
{
    private readonly Dictionary<string, object?> entries = [];

    public bool ThrowIfMissing { get; set; } = true;
    public object? this[string name]
    {
        get => entries[name];
        set => entries[name] = value;
    }

    public ValueTask<object?> GetAsync(string name, CancellationToken cancellationToken = default)
    {
        if (entries.TryGetValue(name, out var value))
            return ValueTask.FromResult(value);

        if (ThrowIfMissing)
            throw new ArgumentException($"Requested parameter '{name}' is missing.");

        return ValueTask.FromResult<object?>(null);
    }

    public void Add(string name, object? value)
    {
        entries[name] = value;
    }
}
