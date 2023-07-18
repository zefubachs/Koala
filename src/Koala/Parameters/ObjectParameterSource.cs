using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Parameters;
public class ObjectParameterSource : IParameterSource
{
    private readonly object target;
    private readonly string prefix;

    public ObjectParameterSource(object target, string prefix)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.prefix = prefix ?? throw new ArgumentNullException(nameof(target));
    }

    public Task<object?> GetValueAsync(string name)
    {
        var split = name.Split('.');
        if (split.Length <= 1)
            return NullTask.Instance;

        if (!string.Equals(split[0], prefix))
            return NullTask.Instance;

        // TODO: Implement deeper property paths
        // ex:
        // prefix.propert1.property2
        // prefix.collection[0].property
        var property = target.GetType().GetProperty(split[1]);
        if (property is null)
            return NullTask.Instance;

        return Task.FromResult(property.GetValue(target));
    }
}
