using Koala.Parameters;

namespace Koala;
public class ParameterProviderBuilder
{
    public List<IParameterSource> Sources { get; } = new List<IParameterSource>();

    public ParameterProvider Build()
        => new ParameterProvider(Sources);
}
