namespace Koala;
internal static class NullTask
{
    public static Task<object?> Instance { get; } = Task.FromResult<object?>(null);
}