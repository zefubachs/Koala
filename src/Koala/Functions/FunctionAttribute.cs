namespace Koala.Functions;

/// <summary>
/// Marks this method as a invokable function by the function registry.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class FunctionAttribute : Attribute
{
    public string? Name { get; set; }
}
