namespace Koala.Parameters;
public interface IParameterSource
{
    Task<object?> GetValueAsync(string name);
}
