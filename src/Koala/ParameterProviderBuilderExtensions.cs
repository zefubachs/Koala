using Koala.Parameters;

namespace Koala;
public static class ParameterProviderBuilderExtensions
{
    public static ParameterProviderBuilder AddDictionary(this ParameterProviderBuilder builder, IDictionary<string, object> entries)
    {
        builder.Sources.Add(new DictionaryParameterSource(entries));
        return builder;
    }

    public static ParameterProviderBuilder AddObject(this ParameterProviderBuilder builder, object target, string prefix)
    {
        builder.Sources.Add(new ObjectParameterSource(target, prefix));
        return builder;
    }
}
