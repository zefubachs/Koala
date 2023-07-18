namespace Koala;
public class ExecutionContext
{
    public ParameterProvider Parameters { get; }

    public ExecutionContext(ParameterProvider parameters)
    {
        Parameters = parameters;
    }

}
